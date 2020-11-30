using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Highbrow.HiPower.Models;
using Highbrow.HiPower.Data;

namespace Highbrow.HiPower.Services
{
    public class HolidayService : IHolidayService
    {
        HiPowerContext _context;

        public HolidayService(HiPowerContext context)
        {
            _context = context;
        }

        public async Task<Holiday> Details(int? id)
        {
            Holiday holiday = null;

            if (id != null)
                holiday = await _context.Holidays.AsNoTracking()
                                                .FirstOrDefaultAsync(n => n.Id == id);

            return holiday;
        }

        public async Task<ServiceResult> Add(Holiday holiday)
        {
            ServiceResult result = ServiceResult.Failure;

            holiday.CreatedAt = DateTime.Now;
            holiday.UpdatedAt = holiday.CreatedAt;

            await _context.AddAsync(holiday);
            int recordsAffected = await _context.SaveChangesAsync();

            if (recordsAffected != 0)
                result = ServiceResult.Success;

            return result;
        }

        public async Task<ServiceResult> Update(Holiday holiday)
        {
            ServiceResult result = ServiceResult.Failure;
            int recordsAffected = 0;

            if (await Exists(holiday.Id))
            {
                holiday.UpdatedAt = DateTime.Now;

                _context.Update(holiday);
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
                Holiday holiday = await _context.Holidays.FindAsync(id);
                _context.Holidays.Remove(holiday);
                recordsAffected = await _context.SaveChangesAsync();
            }

            if (recordsAffected != 0)
                result = ServiceResult.Success;

            return result;
        }

        public async Task<List<Holiday>> List()
        {
            List<Holiday> holidays = null;

            if (await _context.Holidays.AnyAsync())
                holidays = await _context.Holidays.AsNoTracking().ToListAsync();

            return holidays;
        }

        public async Task<bool> Exists(int id)
        {
            Holiday holiday = await _context.Holidays.AsNoTracking().FirstOrDefaultAsync(n => n.Id == id);
            return holiday != null ? true : false;
        }
    }
}