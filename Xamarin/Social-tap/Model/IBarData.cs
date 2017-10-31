using System;
using System.Collections.Generic;

namespace Socialtap.Model
{
    public interface IBarData
    {
        String barName { get; }
        int totalBeverageVolume { get; }
        int totalRating { get; }
        List<string> commentList { get; }
        int reviewCount { get; }

        void AddReview(String comment, int rating, int beverageVolume);
    }
}
