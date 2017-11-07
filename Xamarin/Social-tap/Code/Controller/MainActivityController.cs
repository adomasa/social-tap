using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Android.App;
using Android.Widget;
using RestSharp;
using Socialtap.Code.Model;

namespace Socialtap.Code.Controller
{
    public static class MainActivityController
    {
        private const string Url = "http://10.0.2.2:5000";
        public static List<BarData> BarsData;

        public static async Task AddBarReview(BarReview barReview)
        {
            
            var client = new RestClient(Url);
//            var request = new RestRequest($"{barReview.BarName}/{barReview.BeverageVolume}/" +
//                                          $"{barReview.Comment}/{barReview.Rating}", Method.POST);
            var request = new RestRequest($"/api/values/names/{barReview.BarName}/{barReview.Comment}",Method.POST);
            var cancellationTokenSource = new CancellationTokenSource();
            
            //Todo: duomenys per JsonBody

            var response = await client.ExecuteTaskAsync(request, cancellationTokenSource.Token);
            var requestStatus = response.ResponseStatus;

            var content = response.Content;

            if (requestStatus.Equals(ResponseStatus.Completed)) 
            {
                Toast.MakeText(Application.Context, "Išsaugota", ToastLength.Short).Show();
            }
            else
            {
                Toast.MakeText(Application.Context, "Klaida išsaugojant", ToastLength.Short).Show();
            }
        }

        public static bool FetchBarsData()
        {
            //var client = new RestClient(Url);
            //var request = new RestRequest("Kelias", Method.GET);    
            
            //var response = client.Execute(request);
            //var content = response.Content; 
            
            // Todo: parse JSON, update BarsData
            BarsData = new List<BarData> {new BarData("a", 10, "b", 4, 5), new BarData("a", 10, "b", 4, 5)};
            var status = true;
            return status;
        }
    }
}
