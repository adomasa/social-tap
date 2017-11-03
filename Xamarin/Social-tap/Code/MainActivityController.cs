using System;
using System.Collections.Generic;
using Android.App;
using Android.OS;
using Android.Widget;
using RestSharp;
using Socialtap.Model;

namespace Socialtap.Code
{
    public static class MainActivityController
    {
        public static List<BarData> BarsData;

        public static Boolean AddBarReview(string barName = "Nenurodyta", int beverageLevel = 0,
                                    String comment = "Nėra", int rating = 0)
        {
            var client = new RestClient("http://localhost:5050");
            var request = new RestRequest("resource/{id}", Method.POST);

            request.AddJsonBody(new BarData(barName, beverageLevel, comment, rating));

            // execute the request
            var response = client.Execute(request);
            var content = response.Content; // raw content as string
            Toast.MakeText(Application.Context,
                           content, ToastLength.Short).Show();
            return false;
        }
    }
}
