using System.Collections.Generic;

namespace Socialtap.Code.Model
{
    public class BarData : IBarData
    {
        public string BarName { get; }
        public int AverageBeverageVolume { get; }
        public int AverageRating { get; }
        public List<string> CommentList { get; }
        public int ReviewCount { get; }

        public BarData(string barName, int rating, string comment,
                       int beverageVolume, int reviewCount)
        {
            BarName = barName;
            AverageRating = rating;
            (CommentList = new List<string>()).Add(comment);
            AverageBeverageVolume += beverageVolume;
            ReviewCount = reviewCount;
        }

        public override string ToString() =>
            $"[BarData: barName={BarName}, totalBeverageVolume={AverageBeverageVolume}, " +
            $"totalRating={AverageRating}, commentList={CommentList}, reviewCount={ReviewCount}]";
    }
}
