using System.Threading.Tasks;
using System.Collections.Generic;
using Highbrow.HiPower.Models;

namespace Highbrow.HiPower.Services
{
    public interface IWFHService
    {
        Task<WFH> Details(int? id);

        Task<ServiceResult> Add(WFH wfh);

        Task<ServiceResult> Update(WFH wfh);

        Task<ServiceResult> Delete(int id);

        Task<List<WFH>> List();

        Task<List<WFH>> ListActiveWFHs();

        Task<List<WFH>> ListInactiveWFHs();

        Task<int> GetActiveCount();

        Task<int> GetInactiveCount();
    }
}