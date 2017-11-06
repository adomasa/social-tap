using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialtapAPI
{
    public class BarData : IBarData
    {
        public string BarName { get; }
        public List<string> HashTags { get; }

        public double RateAvg { get; }

        public string Comparison { get; }   //ar geriau įpylė visų barų palyginime

        public double BeverageAvg { get; } //baro vidurkis

        public int BarUses { get; }  

        public BarData() {
            HashTags = new List<string>();
            this.BarName = BarName;
            BarUses++;
        }

    }
}
