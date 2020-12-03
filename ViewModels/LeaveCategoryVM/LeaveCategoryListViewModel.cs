using System.Collections.Generic;
using Highbrow.HiPower.Models;
using System.Linq;

namespace Highbrow.HiPower.ViewModels.LeaveCategoryVM
{
    public class LeaveCategoryListViewModel
    {
        public List<LeaveCategoryViewModel> ActiveLeaveCategories { get; set; }

        public List<LeaveCategoryViewModel> InactiveLeaveCategories { get; set; }

        public int ActiveCount { get; set; }

        public int InactiveCount { get; set; }

        public LeaveCategoryListViewModel()
        {
            ActiveLeaveCategories = new List<LeaveCategoryViewModel>();
            InactiveLeaveCategories = new List<LeaveCategoryViewModel>();
        }

        void SetLeaveCategories(List<LeaveCategory> fromCategory, List<LeaveCategoryViewModel> toCategory)
        {
            if (fromCategory.Count != 0)
            {                
                LeaveCategoryViewModel item;

                foreach (LeaveCategory leaveCategory in fromCategory)
                {
                    item = new LeaveCategoryViewModel
                    {
                        Id = leaveCategory.Id,
                        CategoryName = leaveCategory.CategoryName,
                        IsActive = leaveCategory.IsActive
                    };
                        
                        item.LeaveTypes = leaveCategory.LeaveCategoryTypes
                        .Select(n => new LeaveTypeNameInfo 
                        { Id = n.LeaveType.Id, 
                        LeaveTypeName = n.LeaveType.LeaveTypeName
                         }).ToList();
                    
                    toCategory.Add(item);
                }
            }
        }

        public void SetActiveLeaveCategories(List<LeaveCategory> leaveCategories)
        {
            SetLeaveCategories(leaveCategories, ActiveLeaveCategories);
        }

        public void SetInactiveLeaveCategories(List<LeaveCategory> leaveCategories)
        {
            SetLeaveCategories(leaveCategories, InactiveLeaveCategories);
        }
    }
}