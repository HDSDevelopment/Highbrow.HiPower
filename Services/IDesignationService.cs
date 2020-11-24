using System.Threading.Tasks;
using System.Collections.Generic;
using Highbrow.HiPower.Models;

namespace Highbrow.HiPower.Services
{
public interface IDesignationService
{
    Task<Designation> Details(int? id);

    Task<ServiceResult> Add(Designation designation);

    Task<ServiceResult> Update(Designation designation);

    Task<ServiceResult> Delete(int id);

    Task<List<Designation>> List();

    Task<List<Designation>> ListActiveDesignations();

    Task<List<Designation>> ListInactiveDesignations();

    Task<int> GetActiveCount();

    Task<int> GetInactiveCount();
}
}