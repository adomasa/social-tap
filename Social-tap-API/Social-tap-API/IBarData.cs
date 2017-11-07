using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialtapAPI
{
    public interface IBarData
    {

        double RateAvg { get; }

        bool Comparison { get; }

        double BeverageAvg { get; }

        int BarUses { get; }
    }
}
