using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Content;
using Android.Util;
using Newtonsoft.Json;
using Socialtap.Code.Controller.Interfaces;
using Socialtap.Code.Model;

namespace Socialtap.Code.Controller
{
    public class MainController : IMainController
    {
        private const string Tag = "MainController";

        public static Dictionary<string, BarData> barsData;
        public static IStatistics stats;

        private IRequestManager _requestManager;
        private IPropertiesHandler _propertiesHandler;
        private Context _context;
        private MainActivity _activity;

        private static IMainController instance;


        private MainController(Context context)
        {
            _context = context;
            _activity = (MainActivity) context;
            _propertiesHandler = PropertiesHandler.GetInstance(context);
            _requestManager = RequestManager.GetInstance(context, _propertiesHandler);
        }

        public String GetConfigProperty(string key) 
        {
            return _propertiesHandler.GetConfigValue(key);
        }

        public static IMainController GetInstance(Context context)
        {
            return instance = instance ?? new MainController(context);
        }

        public void AbortRequest() 
        {
            _requestManager.AbortRequest();
        }

        /// <summary>
        /// Adds the bar review via http request to web API
        /// </summary>
        /// <param name="barReview">Parsed user entry</param>
        public async void AddBarReview(IBarReview barReview)
        {
            Log.Debug(Tag, "AddBarReview called.");
            string content = await _requestManager.PostBarReview(barReview);

            if (content == null || content.Equals(String.Empty))
            {
                _activity.ReportAddBarReviewState(false);  
            }
            else
            {
                Log.Debug(Tag, "AddBarReview deserialization in progress.");
                try
                {
                    var processingStatus = JsonConvert.DeserializeObject<bool>(content);
                    _activity.ReportAddBarReviewState(processingStatus);
                }
                catch (Exception e)
                {
                    Log.Error(Tag, $"Exception while handling json response. Message: {e.Message}");
                    _activity.ReportAddBarReviewState(false);
                }
            }
        }

        /// <summary>
        /// Fetchs bars data from web API
        /// </summary>
        /// <returns>Collection of BarData</returns>
        public async Task<bool> FetchBarsData()
        {
            Log.Debug(Tag, "FetchBarsData called.");
            string content = await _requestManager.GetBarsData();

            var status = content != null;
            if (status)
            {
                Log.Debug(Tag, "FetchBarsData deserialization in progress.");
                barsData = JsonConvert.DeserializeObject<Dictionary<string, BarData>>(content);
            }
            return status;
        }


        /// <summary>
        /// Fetches statistics from web API
        /// </summary>
        /// <returns>Statistics object</returns>
        public async Task<bool> FetchStatistics()
        {
            Log.Debug(Tag, "FetchStatistics called.");

            string content = await _requestManager.GetStatData();

            var status = content != null;
            if (status)
            {
                Log.Debug(Tag, "FetchStatistics deserialization in progress.");
                stats = JsonConvert.DeserializeObject<Statistics>(content);
            }
            return status;
        }
    }
}
