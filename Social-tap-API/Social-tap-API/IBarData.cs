using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialtapAPI
{
    public interface IBarData
    {
        string BarName { get; }
        List<string> HashTags { get; }

        double RateAvg { get; }

        string Comparison { get; }

        double BeverageAvg { get; }

        int BarUses { get; }
    }
}
