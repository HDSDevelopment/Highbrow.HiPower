using System.Threading.Tasks;
using System.Collections.Generic;
using Highbrow.HiPower.Models;

namespace Highbrow.HiPower.Services
{
    public interface IDepartmentService
    {
        Task<Department> Details(int? id);

        Task<ServiceResult> Add(Department department);

        Task<ServiceResult> Update(int id, Department department);

        Task<ServiceResult> Delete(int id);

        Task<List<Department>> List();

        List<Department> ListByIsActive(List<Department> departments, bool isActive);
    }
}