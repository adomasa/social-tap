using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;


namespace SocialtapAPI
{

    public class BarData : IBarData
    {
        public int id { get; set; }
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

    /* public class BarDataContext : DbContext
     {
         public DbSet<Dictionary<string, IBarData>> BarDataSet { get; set; }
         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
         {
             optionsBuilder.UseSqlite("Data Source=bardata.db");
         } 
     } */
}