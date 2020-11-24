using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Highbrow.HiPower.Models;
using Highbrow.HiPower.Data;

namespace Highbrow.HiPower.Services
{
    public class WFHService : IWFHService
    {
        HiPowerContext _context;
        public WFHService(HiPowerContext context)
        {
            _context = context;
        }

        public async Task<WFH> Details(int? id)
        {
            WFH wfh = null;

            if (id != null)
                wfh = await _context.WFHs.AsNoTracking()
                                                .FirstOrDefaultAsync(n => n.Id == id);

            return wfh;
        }

        public async Task<ServiceResult> Add(WFH wfh)
        {
            ServiceResult result = ServiceResult.Failure;

            wfh.CreatedAt = DateTime.Now;
            wfh.UpdatedAt = wfh.CreatedAt;

            await _context.AddAsync(wfh);
            int recordsAffected = await _context.SaveChangesAsync();

            if (recordsAffected != 0)
                result = ServiceResult.Success;

            return result;
        }

        public async Task<ServiceResult> Update(WFH wfh)
        {
            ServiceResult result = ServiceResult.Failure;
            int recordsAffected = 0;

            if (await Exists(wfh.Id))
            {
                wfh.UpdatedAt = DateTime.Now;

                _context.Update(wfh);
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
                WFH wfh = await _context.WFHs.FindAsync(id);
                _context.WFHs.Remove(wfh);
                recordsAffected = await _context.SaveChangesAsync();
            }

            if (recordsAffected != 0)
                result = ServiceResult.Success;

            return result;
        }

        public async Task<List<WFH>> List()
        {
            List<WFH> wfhs = null;

            if(await _context.WFHs.AnyAsync())
                wfhs = await _context.WFHs.AsNoTracking().ToListAsync();

                return wfhs;
        }

        public async Task<List<WFH>> ListByIsActive(bool isActive)
        {
            return await (from wfh in _context.WFHs.AsNoTracking()
                          where wfh.IsActive == isActive
                          select wfh)
                        .ToListAsync();
        }

        public async Task<bool> Exists(int id)
        {
            WFH wfh = await _context.WFHs.AsNoTracking().FirstOrDefaultAsync(n => n.Id == id);
            return wfh != null ? true : false;
        }

        public async Task<List<WFH>> ListActiveWFHs()
        {
            return await ListByIsActive(true);
        }

        public async Task<int> GetActiveCount()
        {
            List<WFH> activeWFHs = await ListActiveWFHs();

            if (activeWFHs != null)
                return activeWFHs.Count;
            return 0;
        }

        public async Task<List<WFH>> ListInactiveWFHs()
        {
            return await ListByIsActive(false);
        }

        public async Task<int> GetInactiveCount()
        {
            List<WFH> inactiveWFHs = await ListInactiveWFHs();

            if (inactiveWFHs != null)
                return inactiveWFHs.Count;
            return 0;
        }
    }
}