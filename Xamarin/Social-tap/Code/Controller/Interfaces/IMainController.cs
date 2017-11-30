using System.Threading.Tasks;
using Socialtap.Code.Model;

namespace Socialtap.Code.Controller.Interfaces
{
    public interface IMainController
    {
        void AbortRequest();
        void AddBarReview(IBarReview barReview);
        Task<bool> FetchBarsData();
        Task<bool> FetchStatistics();
    }
}
