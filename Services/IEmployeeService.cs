using System.Threading.Tasks;
using System.Collections.Generic;
using Highbrow.HiPower.Models;
using Highbrow.HiPower.DTO;

namespace Highbrow.HiPower.Services
{
    public interface IEmployeeService
    {
        Task<Employee> Details(int? id);

        Task<ServiceResponse<Employee>> Add(Employee employee);

        Task<ServiceResponse<Employee>> Update(Employee employee);

        Task<List<EmployeeListResponse>> ListActiveEmployees();

        Task<int> GetActiveCount();

        Task<List<EmployeeListResponse>> ListInactiveEmployees();

        Task<int> GetInactiveCount();

        Task<List<EmployeeListResponse>> ListActiveEmployees(EmployeeSearchCriteria criteria);

        Task<List<EmployeeNameResponse>> ListActiveEmployeeNames();

        Task<List<EmployeeListResponse>> ListInactiveEmployees(EmployeeSearchCriteria criteria);

        Task<bool> Exists(long id);

        Task<bool> Exists(string officeId);

        Task<string> GetOfficeID(long id);
    }
}