namespace Socialtap.Code.Model
{
    public class BarReview : IBarReview
    {
        public string BarName { get; }
        public int BeverageVolume { get; }
        public int Rating { get; }
        public string Comment { get; }
        
        public BarReview(int beverageVolume, int rating, string barName,
            string comment = "Komentaro nėra")
        {
            BarName = barName;
            BeverageVolume = beverageVolume;
            Rating = rating;
            Comment = comment;
        }
    }
}