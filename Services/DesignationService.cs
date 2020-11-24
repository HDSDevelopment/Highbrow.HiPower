using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Highbrow.HiPower.Models;
using Highbrow.HiPower.Data;

namespace Highbrow.HiPower.Services
{
    public class DesignationService : IDesignationService
    {
        HiPowerContext _context;
        public DesignationService(HiPowerContext context)
        {
            _context = context;
        }

        public async Task<Designation> Details(int? id)
        {
            Designation designation = null;
            
            if(id != null)
                designation = await _context.Designations.AsNoTracking()
                                                .FirstOrDefaultAsync(n => n.Id == id);
                
                return designation;
        }

        public async Task<ServiceResult> Add(Designation designation)
        {
            ServiceResult result = ServiceResult.Failure;

            await _context.AddAsync(designation);
            int recordsAffected = await _context.SaveChangesAsync();

            if (recordsAffected != 0)
                result = ServiceResult.Success;

            return result;
        }

        public async Task<ServiceResult> Update(Designation designation)
        {
            ServiceResult result = ServiceResult.Failure;
            int recordsAffected = 0;

            if (await Exists(designation.Id))
            {
                _context.Update(designation);
                recordsAffected = await _context.SaveChangesAsync();
            }

            if (recordsAffected != 0)
                result = ServiceResult.Success;

            return result;
        }

        public async Task<ServiceResult> Delete(int id)
        {
            ServiceResult result = ServiceResult.Failure;
            int recordsAffected = 0;

            if (await Exists(id))
            {
                Designation designation = await _context.Designations.FindAsync(id);
                _context.Remove(designation);
                recordsAffected = await _context.SaveChangesAsync();
            }

            if (recordsAffected != 0)
                result = ServiceResult.Success;

            return result;
        }

        public async Task<List<Designation>> List()
        {
            List<Designation> designations = null;

            if (await _context.Designations.AnyAsync())
                designations = await _context.Designations.AsNoTracking().ToListAsync();

            return designations;
        }

        public async Task<bool> Exists(int id)
        {
            Designation designation = await _context.Designations.AsNoTracking().FirstOrDefaultAsync(n => n.Id == id);
            return designation != null ? true : false;
        }

        private async Task<List<Designation>> ListByIsActive(bool isActive)
        {
            return await (from designation in _context.Designations.AsNoTracking()
                          where designation.IsActive == isActive
                          select designation)
                        .ToListAsync();
        }

        public async Task<List<Designation>> ListActiveDesignations()
        {
            return await ListByIsActive(true);
        }

        public async Task<int> GetActiveCount()
        {
            List<Designation> activeDesignations = await ListActiveDesignations();

            if (activeDesignations != null)
                return activeDesignations.Count;
            return 0;
        }

        public async Task<List<Designation>> ListInactiveDesignations()
        {
            return await ListByIsActive(false);
        }

        public async Task<int> GetInactiveCount()
        {
            List<Designation> inactiveDesignations = await ListInactiveDesignations();
            
            if (inactiveDesignations != null)
                return inactiveDesignations.Count;
            return 0;
        }
    }
}