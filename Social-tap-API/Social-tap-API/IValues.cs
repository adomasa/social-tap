using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialtapAPI
{
    interface IValues
    {
        Dictionary<string, BarData> AdBarReview(string barName, string comment, int rate, int beverage);
        Boolean Average(int beverageLevel);
        List<string> HashtagsFinder(string comment);
        Dictionary<string, List<string>> CountBars(string barName, string comment);
        IDictionary<string, BarData> GetBarData();
        double BarRateAverage(string barName, int rate);

    }
}
