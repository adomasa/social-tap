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
        static readonly string Tag = typeof(RequestManager).Name;

        private static IRequestManager _instance;
        private Context _context;

        private readonly string ApiUrl;
        private readonly int GetRequestTimeout;
        private readonly int PostRequestTimeout;


        public static CancellationTokenSource cancellationTokenSource;

        private RequestManager(Context context, IPropertiesHandler propertiesHandler) 
        {
            this._context = context;
            string savedApiUrlKey = context.GetString(Resource.String.saved_api_url);
            ApiUrl = propertiesHandler.GetConfigValue(savedApiUrlKey);

            string savedPostRequestTimeoutKey = context.GetString(Resource.String.saved_post_request_timeout);
            GetRequestTimeout = int.Parse(propertiesHandler.GetConfigValue(savedPostRequestTimeoutKey));

            string savedGetRequestTimeoutKey = context.GetString(Resource.String.saved_get_request_timeout);
            PostRequestTimeout = int.Parse(propertiesHandler.GetConfigValue(savedGetRequestTimeoutKey));
        }

        public static IRequestManager GetInstance (Context context, IPropertiesHandler propertiesHandler)
        {
            _instance = _instance ?? new RequestManager(context, propertiesHandler);
            return _instance;
        }

        public async Task<string> PostBarReview(IBarReview review) 
        {
            var requestPathFormat = _context.GetString(Resource.String.post_bar_review_path);
            var requestPath = String.Format(requestPathFormat, review.BarName, review.Comment, review.Rating, review.BeverageVolume);
            // returns request result raw content
            var sth = await BaseRequest(requestPath, Method.POST);
            return sth;
        }

        public async Task<string> GetBarsData()
        {
            var requestPath = _context.GetString(Resource.String.get_bar_data_path);

            // returns request result raw content
            return await BaseRequest(requestPath, Method.GET);
        }

        public async Task<string> GetStatData()
        {
            var requestPath = _context.GetString(Resource.String.get_stats_path);

            // returns request result raw content
            return await BaseRequest(requestPath, Method.GET);
        }

        private async Task<string> BaseRequest(string requestPath, Method method) 
        {
            var client = new RestClient(ApiUrl);
            var request = new RestRequest(requestPath, method)
            {
                Timeout = PostRequestTimeout
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
