using System.Threading.Tasks;
using System.Collections.Generic;
using Highbrow.HiPower.Models;

namespace Highbrow.HiPower.Services
{
    public interface ILeaveCategoryService
    {
        Task<LeaveCategory> Details(int? id);

    Task<ServiceResult> Update(LeaveCategory leaveCategory, List<int> leaveTypeIds);

        Task<List<LeaveCategory>> ListActiveLeaveCategories();

        Task<List<LeaveCategory>> ListInactiveLeaveCategories();

        Task<int> GetActiveCount();

       Task<int> GetInactiveCount();

        Task<bool> Exists(int id);
        }
}
