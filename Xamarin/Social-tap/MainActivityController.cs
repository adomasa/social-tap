using System;
using System.Collections.Generic;
using Android.OS;
using Socialtap.Model;

namespace Socialtap
{
    public static class MainActivityController
    {
        public static List<BarData> BarsData;

        public static void ExtractBarsDataFromMemory(Bundle data)
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

        public static Boolean AddBarReview(String barName = "Nenurodyta", int beverageLevel = 0,
                                    String comment = "Nėra", int rating = 0)
        {
            // pereiti per listą ieškant, ar nėra jau tokio baro
            // yra:
            // ...
            // nėra:
            // ...
            return false;
        }

        public static Boolean SaveBarsDataToMemory(Bundle data) 
        {
            // ..
            return false;
        }
    }
}
