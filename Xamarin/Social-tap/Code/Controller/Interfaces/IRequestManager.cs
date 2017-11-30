using System.Threading.Tasks;
using Socialtap.Code.Model;

namespace Socialtap.Code.Controller.Interfaces
{
    public interface IRequestManager
    {
        Task<string> PostBarReview(IBarReview review);
        Task<string> GetBarsData();
        Task<string> GetStatData();
        void AbortRequest();
    }
}
