using System;
using System.Threading.Tasks;
using Android.Content;
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
