using System.Collections.Generic;

namespace SocialtapAPI
{
    public interface IBarData
    {
        List<string> Tags{ get; set; }
        double RateAvg { get; set; }
        bool Comparison { get; set; }
        int BarUses { get; set; }
        double BeverageAvg { get; set; }
        int BeverageSum{ get; set; }
    }
}
