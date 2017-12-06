using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialtapAPI
{

    public class BarData : IBarData
    {
        public int Id { get; set; }
        //saugomi hashtagai
        [NotMapped]
        public List<string> Tags { get; set; }
        //žvaigždučių vidurkis
        [NotMapped]
        public double RateAvg { get; set; }
        //ar geriau įpylė visų barų palyginime
        [NotMapped]
        public bool Comparison { get; set; }
        //baro vidurkis
        [NotMapped]
        public double BeverageAvg { get; set; }
        //kiek kartų buvo pasinaudota programele konkrečiame bare 
        [NotMapped]
        public int BarUses { get; set; }
        //kiek iš viso buvo įpilta bare
        [NotMapped]
        public int BeverageSum { get; set; }

        public BarData()
        {
            Tags = new List<string>();
        }
    }
}