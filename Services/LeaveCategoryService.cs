using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Highbrow.HiPower.Models;
using Highbrow.HiPower.Data;

namespace Highbrow.HiPower.Services
{
    public class LeaveCategoryService : ILeaveCategoryService
    {
        HiPowerContext _context;
        public LeaveCategoryService(HiPowerContext context)
        {
            _context = context;
        }

        public async Task<LeaveCategory> Details(int? id)
        {
            LeaveCategory leaveCategory = null;

            if (id != null)
                leaveCategory = await _context.LeaveCategories
                                                .Include(n => n.LeaveCategoryTypes)
                                                .ThenInclude(n => n.LeaveType)
                                                .FirstOrDefaultAsync(n => n.Id == id);

            return leaveCategory;
        }

        public async Task<ServiceResult> Update(LeaveCategory leaveCategory, List<int> leaveTypeIds)
        {
            ServiceResult result = ServiceResult.Failure;
            int recordsAffected = 0;

            LeaveCategory leaveCategoryToUpdate = await Details(leaveCategory.Id);

            if (leaveCategoryToUpdate != null)                                                                                                                                                          
            {                
                leaveCategoryToUpdate.LeaveCategoryTypes.Clear();
                await _context.SaveChangesAsync();

                leaveCategory.UpdatedAt = DateTime.Now;
                List<LeaveCategoryType> leaveCategoryTypes = new List<LeaveCategoryType>();
                LeaveCategoryType leaveCategoryType = new LeaveCategoryType();
                foreach (int leaveTypeId in leaveTypeIds)
				{
                    leaveCategoryType = new LeaveCategoryType
                    {
                        LeaveTypeId = leaveTypeId,
                        LeaveCategoryId = leaveCategoryToUpdate.Id
                    };
                    leaveCategoryTypes.Add(leaveCategoryType);

                }


                    await _context.LeaveCategoryTypes.AddRangeAsync(leaveCategoryTypes);

                recordsAffected = await _context.SaveChangesAsync();
            }

            if (recordsAffected != 0)
                result = ServiceResult.Success;

            return result;
        }

        private async Task<List<LeaveCategory>> ListByIsActive(bool isActive)
        {
            return await _context.LeaveCategories
                                .Where(n => n.IsActive == isActive)
                                .Include(n => n.LeaveCategoryTypes)
                                .ThenInclude(n => n.LeaveType)
                                .ToListAsync();
        }

        public async Task<List<LeaveCategory>> ListActiveLeaveCategories()
        {
            return await ListByIsActive(true);
        }

        public async Task<int> GetActiveCount()
        {
            List<LeaveCategory> activeLeaveCategories = await ListActiveLeaveCategories();

            if (activeLeaveCategories != null)
                return activeLeaveCategories.Count;
            return 0;
        }

        public async Task<List<LeaveCategory>> ListInactiveLeaveCategories()
        {
            return await ListByIsActive(false);
        }

        public async Task<int> GetInactiveCount()
        {
            List<LeaveCategory> inactiveLeaveCategories = await ListInactiveLeaveCategories();

            if (inactiveLeaveCategories != null)
                return inactiveLeaveCategories.Count;
            return 0;
        }

        public async Task<bool> Exists(int id)
        {
            LeaveCategory leaveCategory = await _context.LeaveCategories.AsNoTracking().FirstOrDefaultAsync(n => n.Id == id);
            return leaveCategory != null ? true : false;
        }
    }
}