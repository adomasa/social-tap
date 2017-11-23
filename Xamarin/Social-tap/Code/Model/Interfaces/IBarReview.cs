namespace Socialtap.Code.Model
{
    public interface IBarReview
    {
        string BarName { get; set; }
        int BeverageVolume { get; set; }
        int Rating { get; set; }
        string Comment { get; set; }
    }
}