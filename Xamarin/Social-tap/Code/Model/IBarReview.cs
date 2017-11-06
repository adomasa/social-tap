namespace Socialtap.Code.Model
{
    public interface IBarReview
    {
        string BarName { get; }
        int BeverageVolume { get; }
        int Rating { get; }
        string Comment { get; }
    }
}