using Social_Tap_Api;

namespace SocialtapAPI
{
    public interface IReview 
    {
        int Id { get; }
        int Rate { get; set; }
        string Comment { get; set; }
        Bar Bar { get; set; }
    }
}
