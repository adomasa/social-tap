using SocialtapAPI;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Social_tap_API
{
    public class Statistics : IStatistics
    {
        [Key]
        public string TopBarName { get; set; }
        public double TopBarRate { get; set; }
        public double TopBarAvgBeverageVolume { get; set; }
        public double TotalAvgBeverageVolume { get; set; }
        public int BarCount { get; set; }
        public int ReviewCount { get; set; }

        public Statistics()
        {
            TopBarName = "Nežinomas";
        }
    }

    public class StatisticsContext : DbContext
    {
        public DbSet<Statistics> StatsSet { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=bardata.db");
        }

    }
}