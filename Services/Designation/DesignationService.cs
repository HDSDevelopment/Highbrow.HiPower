using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Highbrow.HiPower.Models;
using Highbrow.HiPower.Data;
using System;

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

            designation.CreatedAt = DateTime.Now;
            designation.UpdatedAt = designation.CreatedAt;
            await _context.AddAsync(designation);
            int recordsAffected = await _context.SaveChangesAsync();

            if (recordsAffected != 0)
                result = ServiceResult.Success;

            return result;
        }

        public async Task<ServiceResult> Update(int id, Designation designation)
        {
            ServiceResult result = ServiceResult.Failure;
            int recordsAffected = 0;

            if (id == designation.Id && await Exists(id))
            {
                designation.UpdatedAt = DateTime.Now;
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

        public List<Designation> ListByIsActive(List<Designation> designations, bool isActive)
        {
            List<Designation> designationsByIsActive = null;

            if (designations != null)
                designationsByIsActive = designations.Where(n => n.IsActive == isActive)
                                                    .ToList();
            return designationsByIsActive;
        }

        public async Task<bool> Exists(int id)
        {
            Designation designation = await _context.Designations.AsNoTracking().FirstOrDefaultAsync(n => n.Id == id);
            return designation != null ? true : false;
        }

        public int GetCount(List<Designation> designations, bool isActive)
        {
            int count = 0;

            if (designations != null)
                count = (from designation in designations
                         where designation.IsActive = isActive
                         select designation)
                        .Count();

            return count;
        }
    }
}