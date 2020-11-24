using Highbrow.HiPower.Models;
using System.Threading.Tasks;

namespace Highbrow.HiPower.Services
{
public interface ICompanyProfileService
{
    Task<CompanyProfile> Details(int? id);

    Task<ServiceResult> Add(CompanyProfile companyProfile);

    Task<ServiceResult> Update(CompanyProfile companyProfile);

    Task<ServiceResult> Delete(int id);
}
}