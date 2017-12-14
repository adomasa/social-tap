using SocialtapAPI;

namespace Social_Tap_Api
{
    public class Review: IReview 
    {
        public int Id { get; set; }
        public int Rate { get; set; }
        public string Comment { get; set; }
        public Bar Bar  { get; set; }
        public int Beverage { get; set; }

        public Review(int rate, string barname, int bev)
        {
            Rate = rate;
            Bar = new Bar(barname);
            Beverage = bev;
        }

        public Review() {}
    }
}
