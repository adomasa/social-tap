using System.Collections.Generic;

namespace Socialtap.Code.Model
{
    public interface IBarData
    {
        string BarName { get; }
        int AverageBeverageVolume { get; }
        int AverageRating { get; }
        List<string> CommentList { get; }
        int ReviewCount { get; }

   
    }
}
