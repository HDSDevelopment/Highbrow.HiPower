using System.Threading.Tasks;
using System.Collections.Generic;
using Highbrow.HiPower.Models;
using Highbrow.HiPower.DTO;

namespace Highbrow.HiPower.Services
{
public interface IDepartmentService
{
    Task<Department> Details(int? id);

    Task<ServiceResult> Add(Department department);

    Task<ServiceResult> Update(Department department);

    Task<ServiceResult> Delete(int id);

    Task<List<Department>> List();

    Task<List<Department>> ListActiveDepartments();

    Task<List<Department>> ListInactiveDepartments();

    Task<List<DepartmentNameResponse>> ListActiveDepartmentNames();

    Task<int> GetActiveCount();

    Task<int> GetInactiveCount();
}
}