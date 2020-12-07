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

        public async Task<ServiceResult> Update(LeaveCategory leaveCategory,
                                                List<int> leaveTypeIds)
        {
            ServiceResult result = ServiceResult.Failure;
            int recordsAffected = 0;

            LeaveCategory categoryToUpdate = await _context.LeaveCategories
                                                .Where(n => n.Id == leaveCategory.Id)
                                                .Include(n => n.LeaveCategoryTypes)
                                                .SingleOrDefaultAsync();

            List<LeaveCategoryType> categoryTypes = new List<LeaveCategoryType>();
            LeaveCategoryType categoryType = new LeaveCategoryType();

            if (await Exists(leaveCategory.Id))
            {
                if (leaveTypeIds != null)
                    foreach (int typeId in leaveTypeIds)
                        AddToLeaveCategoryTypes(categoryTypes,
                                                typeId,
                                                categoryToUpdate.Id);
                else
                    AddToLeaveCategoryTypes(categoryTypes,
                                                null,
                                                categoryToUpdate.Id);

                categoryToUpdate.UpdatedAt = DateTime.Now;
                categoryToUpdate.IsActive = leaveCategory.IsActive;
                categoryToUpdate.LeaveCategoryTypes = categoryTypes;

                recordsAffected = await _context.SaveChangesAsync();
            }

            if (recordsAffected != 0)
                result = ServiceResult.Success;

            return result;
        }

        void AddToLeaveCategoryTypes(List<LeaveCategoryType> categoryTypes,
                                     int? typeId,
                                     int categoryId)
        {
            LeaveCategoryType leaveCategoryType = GetLeaveCategoryType(typeId, categoryId);
            categoryTypes.Add(leaveCategoryType);
        }

        LeaveCategoryType GetLeaveCategoryType(int? leaveTypeId, int leaveCategoryId)
        {
            return new LeaveCategoryType
            {
                LeaveTypeId = (int)leaveTypeId,
                LeaveCategoryId = leaveCategoryId
            };
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