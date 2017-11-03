using System.Collections.Generic;

namespace Socialtap.Model
{
    public class BarData : IBarData
    {
        public string barName { get; }
        public int totalBeverageVolume { get; private set; }
        public int totalRating { get; private set; }
        public List<string> commentList { get; }
        public int reviewCount { get; private set; }

        public int AvgBeverageVolume => totalBeverageVolume / reviewCount;
        public int AvgRating => totalRating / reviewCount;

        public BarData(string barName, int rating, string comment,
                       int beverageVolume)
        {
            this.barName = barName;
            totalRating = rating;
            (commentList = new List<string>()).Add(comment);
            totalBeverageVolume += beverageVolume;
            reviewCount = 1;
        }

        public void AddReview(string comment, int rating, int beverageVolume)
        {
            commentList.Add(comment);
            totalRating += rating;
            totalBeverageVolume += beverageVolume;
            reviewCount++;
        }

        public override string ToString() =>
            $"[BarData: barName={barName}, totalBeverageVolume={totalBeverageVolume}, " +
            $"totalRating={totalRating}, commentList={commentList}, reviewCount={reviewCount}]";
    }
}
