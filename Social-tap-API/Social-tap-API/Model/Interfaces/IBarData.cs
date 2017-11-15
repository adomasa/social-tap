using System.Collections.Generic;

namespace SocialtapAPI
{
    public interface IBarData
    {
        List<string> Tags{ get; }
        double RateAvg { get; }
        bool Comparison { get; }
        int BarUses { get; }
        double BeverageAvg { get; }
        int BeverageSum{ get; }
    }
}
