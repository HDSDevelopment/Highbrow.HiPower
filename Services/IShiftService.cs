using System.Threading.Tasks;
using System.Collections.Generic;
using Highbrow.HiPower.Models;

namespace Highbrow.HiPower.Services
{
    public interface IShiftService
    {
        Task<Shift> Details(int? id);

        Task<ServiceResult> Add(Shift shift);

        Task<ServiceResult> Update(Shift shift);

        Task<ServiceResult> Delete(int id);

        Task<List<Shift>> List();

        Task<List<Shift>> ListActiveShifts();

        Task<List<Shift>> ListInactiveShifts();

        Task<int> GetActiveCount();

        Task<int> GetInactiveCount();
    }
}