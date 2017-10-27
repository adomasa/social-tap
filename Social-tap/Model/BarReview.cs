using System;
namespace Socialtap
{
    public class BarReview
    {
        private String barName;
        private int beverageVolume;
        private String barComment;
        private int barRating;

        public BarReview(string barName, int beverageVolume, string barComment, 
                         int barRating)
        {
            this.barName = barName;
            this.beverageVolume = beverageVolume;
            this.barComment = barComment;
            this.barRating = barRating;
        }

        public override string ToString() => string.Format(
            "[BarReview: barName={0}, beverageVolume={1}, barComment={2}, " +
            "barRating={3}]", barName, beverageVolume, barComment, barRating);

    }
}
