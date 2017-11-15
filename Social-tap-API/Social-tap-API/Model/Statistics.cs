using SocialtapAPI;

namespace Social_tap_API
{
    public class Statistics : IStatistics
    {
        public string TopBarName { get; set; }
        public double TopBarRate { get; set; }
        public double TopBarAvgBeverageVolume { get; set; }
        public double TotalAvgBeverageVolume { get; set; }
        public int BarCount { get; set; }
        public int ReviewCount { get; set; }
    }
}