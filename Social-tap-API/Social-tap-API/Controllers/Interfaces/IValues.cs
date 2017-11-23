using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Social_tap_API;

namespace SocialtapAPI
{
    interface IValues
    {
        bool AddBarReview(string barName, string comment, int rate, int beverage);
        IDictionary<string, IBarData> GetBarData();
        Statistics GetStats();
    }
}
