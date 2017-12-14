using System.Collections.Generic;

namespace SocialtapAPI
{
    interface IValuesController
    {
        IStatistics GetStats();
        IDictionary<string, IBarData> GetBarData();
        bool AddBarReview(string barName, string comment, int rate, int beverage);
    }
}
