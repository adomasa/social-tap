using System.Collections.Generic;
using Social_tap_API;

namespace SocialtapAPI
{
    interface ICalculations
    {
        string BarNameAdaptation(string barName);
        double BarRateAverage(string barName);
        bool Average(int beverageLevel);
        Dictionary<string, IBarData> AddBarInfo(string barName, int beverage, int rate, string comment);
        Dictionary<string, IBarData> GetBarData();
        double BarAverage(string barName, int index);
        bool AddReview(string barName, int rate, int beverage);
        IStatistics Stats();
        string BestBarRate();
        bool IsBarNew(string barName);
        bool AddBar(string barName);
        bool Validation(int rate, int beverage, string barName);
    }
}
