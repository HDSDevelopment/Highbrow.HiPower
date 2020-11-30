using System.Threading.Tasks;
using System.Collections.Generic;
using Highbrow.HiPower.Models;

namespace Highbrow.HiPower.Services
{
    public interface IHolidayService
    {
            Task<Holiday> Details(int? id);

            Task<ServiceResult> Add(Holiday department);

            Task<ServiceResult> Update(Holiday department);

            Task<ServiceResult> Delete(int id);

            Task<List<Holiday>> List();
    }
}