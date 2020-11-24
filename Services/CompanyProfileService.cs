using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Highbrow.HiPower.Models;
using Highbrow.HiPower.Data;

namespace Highbrow.HiPower.Services
{
    public class CompanyProfileService : ICompanyProfileService
    {
        HiPowerContext _context;
        public CompanyProfileService(HiPowerContext context)
        {
            _context = context;
        }
        public async Task<CompanyProfile> Details(int? id)
        {
            CompanyProfile companyProfile = null;
            
            if(id != null)
                companyProfile = await _context.CompanyProfiles.AsNoTracking()
                                                .FirstOrDefaultAsync(n => n.Id == id);
                
                return companyProfile;            
        }

        public async Task<ServiceResult> Add(CompanyProfile companyProfile)
        {
            ServiceResult result = ServiceResult.Failure;
            companyProfile.CreatedAt = DateTime.Now;
            companyProfile.UpdatedAt = companyProfile.CreatedAt;

            await _context.AddAsync(companyProfile);
            int recordsAffected = await _context.SaveChangesAsync();
            
            if(recordsAffected != 0)
                result = ServiceResult.Success;
                    
                    return result;
        }

        public async Task<ServiceResult> Update(CompanyProfile companyProfile)
        {
            ServiceResult result = ServiceResult.Failure;
            int recordsAffected = 0;
            
            if(await Exists(companyProfile.Id))
            {
                companyProfile.UpdatedAt = DateTime.Now;
                _context.CompanyProfiles.Update(companyProfile);
                recordsAffected = await _context.SaveChangesAsync();
            }

            if(recordsAffected != 0)
                result = ServiceResult.Success;

                    return result;
        }

        public async Task<ServiceResult> Delete(int id)
        {
            ServiceResult result = ServiceResult.Failure;
            int recordsAffected = 0;

            if(await Exists(id))
            {
                CompanyProfile companyProfile = await _context.CompanyProfiles.FindAsync(id);
                _context.CompanyProfiles.Remove(companyProfile);
                recordsAffected = await _context.SaveChangesAsync();                
            }
            
            if(recordsAffected != 0)
                result = ServiceResult.Success;

                    return result;
        }

        public async Task<bool> Exists(int id)
        {
            CompanyProfile companyProfile = await _context.CompanyProfiles.AsNoTracking().FirstOrDefaultAsync(n => n.Id == id);
            return companyProfile != null ? true : false;
        } 
    }
}