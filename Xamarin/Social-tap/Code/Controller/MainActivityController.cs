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

        public static Boolean AddBarReview(string barName = "Nenurodyta", int beverageVolume = 0,
                                    string comment = "Nėra", int rating = 0)
        {
            BarsData.Add(new BarData(barName, rating, comment, beverageVolume));
//            var client = new RestClient("http://localhost:5050");
//            var request = new RestRequest("resource/{id}", Method.POST);
//
//            request.AddJsonBody(new BarData(barName, beverageVolume, comment, rating));
//
//            // execute the request
//            var response = client.Execute(request);
//            var content = response.Content; // raw content as string
//            Toast.MakeText(Application.Context,
//                           content, ToastLength.Short).Show();
            var status = true;
            return status;
        }
    }
}
