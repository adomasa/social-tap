using System.Collections.Generic;

namespace Socialtap.Code.Model
{
    namespace SocialtapAPI
    {
        public interface IBarData
        {
            double RateAvg { get; }

            bool Comparison { get; }

            int BarUses { get; }

            double BeverageAvg { get; }
        }
    }

}
