using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Social_tap_API;

namespace SocialtapAPI
{
    interface IValues
    {
        Boolean AddBarReview(string barName, string comment, int rate, int beverage);
        IDictionary<string, BarData> GetBarData();
        Statistics GetStats();
    }
}
