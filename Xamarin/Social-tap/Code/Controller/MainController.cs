using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Android.Content;
using Android.Util;
using Android.Widget;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using Socialtap.Code.Model;

namespace Socialtap.Code.Controller
{
    public static class MainController
    {
        private const string Tag = "MainController";
        private const string Url = "http://10.0.2.2:5000";
        public static Dictionary<string, BarData> BarsData;
        public static Statistics stats;
        public static CancellationTokenSource cancellationTokenSource;

        private const int PostRequestTimeout = 3000;
        private const int GetRequestTimeout = 5000;
        private const bool status_failed = false;

        /// <summary>
        /// Adds the bar review via http request to web API
        /// </summary>
        /// <param name="barReview">Bar review, collection of target data</param>
        /// <param name="mainActivity">Reference to access view methods</param>
        public static async void AddBarReview(BarReview barReview, MainActivity mainActivity)
        {
            Log.Debug(Tag, "AddBarReview called.");
            var client = new RestClient(Url);
            var request = new RestRequest($"/api/values/AddBarReview/{barReview.BarName}/{barReview.Comment}/" +
                                          $"{barReview.Rating}/{barReview.BeverageVolume}", Method.POST)
            {
                Timeout = PostRequestTimeout
            };

            cancellationTokenSource = new CancellationTokenSource();

            var response = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token);

            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Dispose();
            }

            var requestStatus = response.ResponseStatus;
            if (requestStatus.Equals(ResponseStatus.None) || requestStatus.Equals(ResponseStatus.TimedOut) 
                || requestStatus.Equals(ResponseStatus.Error))
                mainActivity.ReportAddBarReviewState(status_failed);
            else
            {
                Log.Debug(Tag, "AddBarReview deserialization in progress.");
                try 
                {
                    var processingStatus = JsonConvert.DeserializeObject<bool>(response.Content);
                    mainActivity.ReportAddBarReviewState(requestStatus.Equals(ResponseStatus.Completed) && processingStatus);
                }
                catch (Exception e) {
                    Log.Error(Tag, $"Exception while handling json response. Message: {e.Message}");
                    mainActivity.ReportAddBarReviewState(requestStatus.Equals(ResponseStatus.Completed) && status_failed);
                }
            }
        }

        /// <summary>
        /// Fetchs bars data from web API
        /// </summary>
        /// <returns>Collection of BarData</returns>
        public static async Task<bool> FetchBarsData()
        {
            //Todo: halt async request when leaving unloaded fragment
            Log.Debug(Tag, "FetchBarsData called.");
            var client = new RestClient(Url);
            var request = new RestRequest("/api/values/GetBarData/", Method.GET) {Timeout = GetRequestTimeout};

            cancellationTokenSource = new CancellationTokenSource();

            var response = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token);

            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Dispose();
            }

            var requestStatus = response.ResponseStatus;

            if (requestStatus.Equals(ResponseStatus.None) || requestStatus.Equals(ResponseStatus.TimedOut)
                || requestStatus.Equals(ResponseStatus.Error))
            {
                return false;
            }

            var content = response.Content;
            Log.Debug(Tag, "FetchBarsData deserialization in progress.");
            BarsData = JsonConvert.DeserializeObject<Dictionary<string, BarData>>(content);
            return requestStatus.Equals(ResponseStatus.Completed);
        }
        /// <summary>
        /// Fetches statistics from web API
        /// </summary>
        /// <returns>Statistics object</returns>
        public static async Task<bool> FetchStatistics()
        {
            //Todo: halt async request when leaving unloaded fragment
            Log.Debug(Tag, "FetchStatistics called.");

            BarsData = new Dictionary<string, BarData>();
            var client = new RestClient(Url);
            var request = new RestRequest("/api/values/Stats/", Method.GET) { Timeout = GetRequestTimeout };

            cancellationTokenSource = new CancellationTokenSource();

            var response = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token);

            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Dispose();
            }

            var requestStatus = response.ResponseStatus;

            if (requestStatus.Equals(ResponseStatus.None) || requestStatus.Equals(ResponseStatus.TimedOut)
                || requestStatus.Equals(ResponseStatus.Error))
            {
                return status_failed;
            }

            var content = response.Content;

            Log.Debug(Tag, "FetchStatistics deserialization in progress.");
            stats = JsonConvert.DeserializeObject<Statistics>(content);

            return requestStatus.Equals(ResponseStatus.Completed);
        }
    }
}
