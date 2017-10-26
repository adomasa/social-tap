using System;
using System.Collections.Generic;
using Android.OS;
using Socialtap.Model;

namespace Socialtap
{
    public class MainActivityController
    {
        public List<BarData> barsData;
        private static MainActivityController instance;

        private MainActivityController() { }

        public static MainActivityController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MainActivityController();
                }
                return instance;
            }
        }

        public void ExtractBarsDataFromMemory(Bundle data)
        {
            if (null != data) 
            {
                data.GetSerializable("BARS_DATA");
            }
            else 
            {
                barsData = new List<BarData>();
            }
        }

        public void addBarReview(BarReview barReview) 
        {
            // pereiti per listą ieškant, ar nėra jau tokio baro
            // yra:
            // ...
            // nėra:
            // ...
        } 
    }
}