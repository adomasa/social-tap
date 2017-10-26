using System;
using System.Collections.Generic;

namespace Socialtap.Model
{
    public class BarData : IBarData
    {
        public String barName { get; private set; }
        public int totalBeverageVolume { get; private set; }
        public int avgBeverageVolume
        {
            get
            {
                return totalBeverageVolume / reviewCount;
            }
        }
        public int avgRating
        {
            get
            {
                return totalRating / reviewCount;
            }
        }
        public int totalRating { get; private set; }
        public List<string> commentList { get; private set; }
        public int reviewCount { get; private set; }

        public BarData(string barName, int rating, string comment,
                       int beverageVolume)
        {
            this.barName = barName;
            this.totalRating = rating;
            (commentList = new List<string>()).Add(comment);
            this.totalBeverageVolume += beverageVolume;
        }

        public void AddReview(String comment, int rating, int beverageVolume)
        {
            commentList.Add(comment);
            totalRating += rating;
            totalBeverageVolume += beverageVolume;
        }
    }
}
