using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Highbrow.HiPower.Models;
using Highbrow.HiPower.Data;

namespace Highbrow.HiPower.Services
{
    public class LeaveTypeService : ILeaveTypeService
    {
        HiPowerContext _context;
        public LeaveTypeService(HiPowerContext context)
        {
            _context = context;
        }

        public async Task<LeaveType> Details(int? id)
        {
            LeaveType leaveType = null;

            if (id != null)
                leaveType = await _context.LeaveTypes.AsNoTracking()
                                                .FirstOrDefaultAsync(n => n.Id == id);

            return leaveType;
        }

        public async Task<ServiceResult> Add(LeaveType leaveType)
        {
            ServiceResult result = ServiceResult.Failure;

            leaveType.CreatedAt = DateTime.Now;
            leaveType.UpdatedAt = leaveType.CreatedAt;

            await _context.AddAsync(leaveType);
            int recordsAffected = await _context.SaveChangesAsync();

            if (recordsAffected != 0)
                result = ServiceResult.Success;

            return result;
        }

        public async Task<ServiceResult> Update(LeaveType leaveType)
        {
            ServiceResult result = ServiceResult.Failure;
            int recordsAffected = 0;

            if (await Exists(leaveType.Id))
            {
                leaveType.UpdatedAt = DateTime.Now;

                _context.Update(leaveType);
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
                LeaveType leaveType = await _context.LeaveTypes.FindAsync(id);
                _context.LeaveTypes.Remove(leaveType);
                   recordsAffected = await _context.SaveChangesAsync();
            }

            if (recordsAffected != 0)
                result = ServiceResult.Success;

            return result;
        }

        public async Task<List<LeaveType>> List()
        {
            List<LeaveType> leaveTypes = null;

            if (await _context.LeaveTypes.AnyAsync())
                leaveTypes = await _context.LeaveTypes.AsNoTracking().ToListAsync();

            return leaveTypes;
        }

        private async Task<List<LeaveType>> ListByIsActive(bool isActive)
        {
            return await (from leaveType in _context.LeaveTypes.AsNoTracking()
                          where leaveType.IsActive == isActive
                          select leaveType)
                        .ToListAsync();
        }

        public async Task<List<LeaveType>> ListActiveLeaveTypes()
        {
            return await ListByIsActive(true);
        }

        public async Task<int> GetActiveCount()
        {
            List<LeaveType> activeLeaveTypes = await ListActiveLeaveTypes();

            if (activeLeaveTypes != null)
                return activeLeaveTypes.Count;
            return 0;
        }

       

        public async Task<List<LeaveType>> ListInactiveLeaveTypes()
        {
            return await ListByIsActive(false);
        }

        public async Task<int> GetInactiveCount()
        {
            List<LeaveType> inactiveLeaveTypes = await ListInactiveLeaveTypes();

            if (inactiveLeaveTypes != null)
                return inactiveLeaveTypes.Count;
            return 0;
        }

        public async Task<List<LeaveTypeNameInfo>> ListLeaveTypeNames()
        {
            return await (from leaveType in _context.LeaveTypes.AsNoTracking()
                          where leaveType.IsActive == true
                          select new LeaveTypeNameInfo
                          {
                              Id = leaveType.Id,
                              LeaveTypeName = leaveType.LeaveTypeName
                          })
                            .ToListAsync();
        }

        public async Task<bool> Exists(int id)
        {
            LeaveType leaveType = await _context.LeaveTypes.AsNoTracking().FirstOrDefaultAsync(n => n.Id == id);
            return leaveType != null ? true : false;
        }
    }
}