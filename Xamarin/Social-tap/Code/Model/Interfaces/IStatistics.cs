using System;
namespace Socialtap.Code.Model
{
    public interface IStatistics
    {
        string TopBarName { get; set; }
        double TopBarRate { get; set; }
        double TopBarAvgBeverageVolume { get; set; }
        double TotalAvgBeverageVolume { get; set; }
        int BarCount { get; set; }
        int ReviewCount { get; set; }
    }
}
