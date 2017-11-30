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
        private const string Url = "http://10.0.2.2:5000";

        public static Dictionary<string, IBarData> BarsData;
        public static IStatistics stats;

        private IRequestManager _requestManager;
        private Context _context;
        private MainActivity _activity;

        private static MainController instance;

        private MainController(Context context)
        {
            _context = context;
            _requestManager = RequestManager.GetInstance(context);
            _activity = (MainActivity) context;

            //load cfg settings
            var propertiesHandler = PropertiesHandler.GetInstance(context);

            var url = propertiesHandler.GetConfigValue("api_url");
        }

        public static MainController GetInstance(Context context)
        {
            return instance ?? new MainController(context);
        }

        public void AbortRequest() 
        {
            _requestManager.AbortRequest();
        }

        //Todo: DI, lazy,

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
                BarsData = JsonConvert.DeserializeObject<Dictionary<string, IBarData>>(content);
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
