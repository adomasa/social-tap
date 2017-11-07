using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialtapAPI
{
    public class BarData : IBarData
    {
        public List<string> Tags { get; set; }
        public double RateAvg { get; set; } 

        public bool Comparison { get; set; }   //ar geriau įpylė visų barų palyginime

        public double BeverageAvg { get; set; } //baro vidurkis

        public  int BarUses { get; set; }
        public int BeverageSum { get; set; }

        public BarData() {
            Tags = new List<string>();
            //HashTags = new Dictionary<string, List<string>>();
        }

    }
}
