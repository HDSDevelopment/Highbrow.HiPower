using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Highbrow.HiPower.Models;
using Highbrow.HiPower.Data;

namespace Highbrow.HiPower.Services
{
    public class ShiftService : IShiftService
    {
        HiPowerContext _context;
        public ShiftService(HiPowerContext context)
        {
            _context = context;
        }

        public async Task<Shift> Details(int? id)
        {
            Shift shift = null;

            if (id != null)
                shift = await _context.Shifts.AsNoTracking()
                                                .FirstOrDefaultAsync(n => n.Id == id);

            return shift;
        }

        
        public async Task<ServiceResult> Add(Shift shift)
        {
            ServiceResult result = ServiceResult.Failure;

            shift.CreatedAt = DateTime.Now;
            shift.UpdatedAt = shift.CreatedAt;

            await _context.AddAsync(shift);
            int recordsAffected = await _context.SaveChangesAsync();

            if (recordsAffected != 0)
                result = ServiceResult.Success;

            return result;
        }

        public async Task<ServiceResult> Update(Shift shift)
        {
            ServiceResult result = ServiceResult.Failure;
            int recordsAffected = 0;

            if (await Exists(shift.Id))
            {
                shift.UpdatedAt = DateTime.Now;

                _context.Update(shift);
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
                Shift shift = await _context.Shifts.FindAsync(id);
                _context.Remove(shift);
                recordsAffected = await _context.SaveChangesAsync();
            }

            if (recordsAffected != 0)
                result = ServiceResult.Success;

            return result;
        }

        public async Task<List<Shift>> List()
        {
            List<Shift> shifts = null;

            if (await _context.Shifts.AnyAsync())
                shifts = await _context.Shifts.AsNoTracking().ToListAsync();

            return shifts;
        }

        private async Task<List<Shift>> ListByIsActive(bool isActive)
        {
            return await (from shift in _context.Shifts.AsNoTracking()
                          where shift.IsActive == isActive
                          select shift)
                        .ToListAsync();
        }

        public async Task<List<Shift>> ListActiveShifts()
        {
            return await ListByIsActive(true);
        }

        public async Task<int> GetActiveCount()
        {
            List<Shift> activeShifts = await ListActiveShifts();

            if (activeShifts != null)
                return activeShifts.Count;
            return 0;
        }

        public async Task<List<Shift>> ListInactiveShifts()
        {
            return await ListByIsActive(false);
        }

        public async Task<int> GetInactiveCount()
        {
            List<Shift> inactiveShifts = await ListInactiveShifts();

            if (inactiveShifts != null)
                return inactiveShifts.Count;
            return 0;
        }

        public async Task<bool> Exists(int id)
        {
            Shift shift = await _context.Shifts.AsNoTracking().FirstOrDefaultAsync(n => n.Id == id);
            return shift != null ? true : false;
        }
    }
}