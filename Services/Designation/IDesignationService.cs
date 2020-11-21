using System.Threading.Tasks;
using System.Collections.Generic;
using Highbrow.HiPower.Models;

namespace Highbrow.HiPower.Services
{
public interface IDesignationService
{
    Task<Designation> Details(int? id);

    Task<ServiceResult> Add(Designation designation);

    Task<ServiceResult> Update(int id, Designation designation);

    Task<ServiceResult> Delete(int id);

    Task<List<Designation>> List();

    List<Designation> ListByIsActive(List<Designation> designations, bool isActive);
    
    int GetCount(List<Designation> designations, bool isActive);
}
}