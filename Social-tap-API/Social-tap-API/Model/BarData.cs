using System.Collections.Generic;

namespace SocialtapAPI
{

    public class BarData : IBarData
    {
        public int Id { get; set; }
        //saugomi hashtagai
        public List<string> Tags { get; set; }
        //žvaigždučių vidurkis

        public double RateAvg { get; set; }
        //ar geriau įpylė visų barų palyginime

        public bool Comparison { get; set; }
        //baro vidurkis

        public double BeverageAvg { get; set; }
        //kiek kartų buvo pasinaudota programele konkrečiame bare 

        public int BarUses { get; set; }
        //kiek iš viso buvo įpilta bare

        public int BeverageSum { get; set; }

        public BarData()
        {
            Tags = new List<string>();
        }
    }
}