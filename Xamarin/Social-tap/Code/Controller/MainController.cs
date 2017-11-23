using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Android.Content;
using Android.Util;
using Newtonsoft.Json;
using RestSharp;
using Socialtap.Code.Controller.Interfaces;
using Socialtap.Code.Model;

namespace Socialtap.Code.Controller
{
    public class MainController : IMainController
    {
        private const string Tag = "MainController";
        private const string Url = "http://10.0.2.2:5000";

        public static Dictionary<string, BarData> BarsData;
        public static IStatistics stats;

        private IRequestManager requestManager;
        Context context;
        MainActivity activity;

        private static MainController instance;

        private MainController(Context context)
        {
            this.context = context;
            requestManager = RequestManager.GetInstance(context);
            activity = (MainActivity) context;
        }

        public static MainController GetInstance(Context context)
        {
            instance = instance ?? new MainController(context);
            return instance;
        }

        public void AbortRequest() 
        {
            requestManager.AbortRequest();
        }

        //Todo: DI, lazy,
        //Todo: Kai paleistas async taskas, negali kitas būt leidžiamas

        /// <summary>
        /// Adds the bar review via http request to web API
        /// </summary>
        /// <param name="barReview">Parsed user entry</param>
        public async void AddBarReview(IBarReview barReview)
        {
            Log.Debug(Tag, "AddBarReview called.");
            string content = await requestManager.PostBarReview(barReview);

            if (content == null || content.Equals(String.Empty))
            {
                activity.ReportAddBarReviewState(false);  
            }
            else
            {
                Log.Debug(Tag, "AddBarReview deserialization in progress.");
                try
                {
                    var processingStatus = JsonConvert.DeserializeObject<bool>(content);
                    activity.ReportAddBarReviewState(processingStatus);
                }
                catch (Exception e)
                {
                    Log.Error(Tag, $"Exception while handling json response. Message: {e.Message}");
                    activity.ReportAddBarReviewState(false);
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
            string content = await requestManager.GetBarsData();

            var status = content != null;
            if (status)
            {
                Log.Debug(Tag, "FetchBarsData deserialization in progress.");
                BarsData = JsonConvert.DeserializeObject<Dictionary<string, BarData>>(content);
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

            string content = await requestManager.GetStatData();

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
