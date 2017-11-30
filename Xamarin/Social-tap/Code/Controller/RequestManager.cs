using System;
using System.Threading;
using System.Threading.Tasks;
using Android.Content;
using Android.Util;
using RestSharp;
using Socialtap.Code.Controller.Interfaces;
using Socialtap.Code.Model;

namespace Socialtap.Code.Controller
{
    public class RequestManager : IRequestManager
    {
        public static string Tag = "RequestManager";

        private Context context;
        private string apiUrl;
        private int getRequestTimeout;
        private int postRequestTimeout;
        private static RequestManager instance;

        public static CancellationTokenSource cancellationTokenSource;

        private RequestManager(Context context, IPropertiesHandler propertiesHandler) 
        {
            this.context = context;
            apiUrl = propertiesHandler.GetConfigValue("api_url");
            int.TryParse(propertiesHandler.GetConfigValue("post_request_timeout"), out getRequestTimeout);
            int.TryParse(propertiesHandler.GetConfigValue("get_request_timeout"), out postRequestTimeout);

        }

        public static RequestManager GetInstance (Context context, IPropertiesHandler propertiesHandler)
        {
            instance = instance ?? new RequestManager(context, propertiesHandler);
            return instance;
        }

        public async Task<string> PostBarReview(IBarReview review) 
        {
            var requestPathFormat = context.GetString(Resource.String.post_bar_review_path);
            var requestPath = String.Format(requestPathFormat, review.BarName, review.Comment, review.Rating, review.BeverageVolume);
            // returns request result raw content
            var sth = await BaseRequest(requestPath, Method.POST);
            return sth;
        }

        public async Task<string> GetBarsData()
        {
            var requestPath = context.GetString(Resource.String.get_bar_data_path);

            // returns request result raw content
            return await BaseRequest(requestPath, Method.GET);
        }

        public async Task<string> GetStatData()
        {
            var requestPath = context.GetString(Resource.String.get_stats_path);

            // returns request result raw content
            return await BaseRequest(requestPath, Method.GET);
        }

        private async Task<string> BaseRequest(string requestPath, Method method) 
        {
            var client = new RestClient(apiUrl);
            var request = new RestRequest(requestPath, method)
            {
                Timeout = postRequestTimeout
            };

            IRestResponse response;

            cancellationTokenSource = new CancellationTokenSource();
            Log.Debug(Tag, "CancellationTokenSource initialised");

            try
            {
                response = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token);
                if (cancellationTokenSource != null)
                {
                    cancellationTokenSource.Dispose();
                    cancellationTokenSource = null;
                    Log.Debug(Tag, "CancellationTokenSource disposed");
                }
                    
                    var requestStatus = response.ResponseStatus;
                    return requestStatus.Equals(ResponseStatus.Completed) ? response.Content : null;
                
            }
            catch (TaskCanceledException) 
            {
                Log.Debug(Tag, "Task cancelled");
                return null;
            }
        }

        public void AbortRequest()
        {
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
                Log.Debug(Tag, "CancellationToken activated");
            }
            else 
            {
                Log.Debug(Tag, "There is no CancellationToken");    
            }
        }
    }
}
