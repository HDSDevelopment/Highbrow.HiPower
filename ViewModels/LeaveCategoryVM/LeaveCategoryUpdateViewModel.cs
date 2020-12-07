using System;
using System.Linq;
using System.Collections.Generic;
using Highbrow.HiPower.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Highbrow.HiPower.ViewModels.LeaveCategoryVM
{
    public class LeaveCategoryUpdateViewModel
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public bool IsActive { get; set; }

        public List<SelectListItem> allLeaveTypesSelectList { get; set; }

        public List<int> SelectedLeaveTypes { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public void SetViewModel(LeaveCategory leaveCategory, List<LeaveTypeNameInfo> allLeaveTypes)
        {
            Id = leaveCategory.Id;
            CategoryName = leaveCategory.CategoryName;
            IsActive = leaveCategory.IsActive;

            allLeaveTypesSelectList = new List<SelectListItem>();

            foreach (LeaveTypeNameInfo leaveTypeName in allLeaveTypes)
            {
                SelectListItem item = new SelectListItem
                {
                    Text = leaveTypeName.LeaveTypeName,
                    Value = Convert.ToString(leaveTypeName.Id)
                };
                allLeaveTypesSelectList.Add(item);
            }

            SelectedLeaveTypes = leaveCategory.LeaveCategoryTypes
                                            .Select(n => n.LeaveType.Id)
                                            .ToList();
        }

        public LeaveCategory GetLeaveCategory()
        {
            LeaveCategory leaveCategory = new LeaveCategory
            {
                Id = this.Id,
                CategoryName = this.CategoryName,
                IsActive = this.IsActive
            };
            return leaveCategory;
        }
    }
}