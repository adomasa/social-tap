using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialtapAPI
{
    interface IValues
    {
        Boolean AddBarReview(string barName, string comment, int rate, int beverage);
        IDictionary<string, BarData> GetBarData();

    }
}
