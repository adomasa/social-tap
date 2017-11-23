namespace Socialtap.Code.Model
{
    public class BarReview : IBarReview
    {
        public string BarName { get; set; }
        public int BeverageVolume { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        
        public BarReview(int beverageVolume, int rating, string barName,
            string comment = "Komentaro nėra")
        {
            BarName = barName;
            BeverageVolume = beverageVolume;
            Rating = rating;
            Comment = comment.Equals(string.Empty) ? "Komentaro nėra" : comment;
        }
    }
}