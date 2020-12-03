using System;
using System.Collections.Generic;
using Highbrow.HiPower.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Highbrow.HiPower.ViewModels.LeaveCategoryVM
{
    public class LeaveCategoryViewModel
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public bool IsActive { get; set; }

        public List<LeaveTypeNameInfo> LeaveTypes { get; set; }
    }
}