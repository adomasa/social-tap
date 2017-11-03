using System;
using System.Collections.Generic;

namespace Socialtap.Model
{
    public interface IBarData
    {
        string barName { get; }
        int totalBeverageVolume { get; }
        int totalRating { get; }
        List<string> commentList { get; }
        int reviewCount { get; }

        void AddReview(string comment, int rating, int beverageVolume);
    }
}
