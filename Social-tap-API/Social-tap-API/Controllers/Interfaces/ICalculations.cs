using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Social_tap_API;

namespace SocialtapAPI
{
    interface ICalculations
    {
        string BarNameAdaptation(string barName);
        double BarRateAverage(string barName, int rate);
        bool Average(int beverageLevel);
        List<string> HashtagsFinder(string comment);
        bool Validation(int rate, int beverage, string barName);
        bool IsBarNew(string barName);
        Dictionary<string, IBarData> AddBarInfo(string barName, int beverage, int rate, string comment);
        string BestBarRate();
        Statistics Stats();
    }
}
