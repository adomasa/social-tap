using System;
using System.Collections.Generic;
using Android.OS;
using Socialtap.Model;

namespace Socialtap
{
    public class MainActivityController
    {
        public List<BarData> BarsData;
        private static MainActivityController _instance;

        private MainActivityController() { }

        public static MainActivityController Instance => _instance ?? (_instance = new MainActivityController());

        public void ExtractBarsDataFromMemory(Bundle data)
        {
            if (null != data) 
            {
                data.GetSerializable("BARS_DATA");
            }
            else 
            {
                BarsData = new List<BarData>();
            }
        }

        public Boolean addBarReview(BarReview barReview) 
        {
            // pereiti per listą ieškant, ar nėra jau tokio baro
            // yra:
            // ...
            // nėra:
            // ...
            return false;
        }

        public Boolean saveBarsDataToMemory(Bundle data) 
        {
            // ..
            return false;
        }
    }
}
