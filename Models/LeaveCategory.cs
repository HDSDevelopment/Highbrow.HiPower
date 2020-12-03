using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic; 

namespace Highbrow.HiPower.Models
{
    public class LeaveCategory
    {
        public int Id { get; set; }

        public string CategoryName { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; } 

        public List<LeaveCategoryType> LeaveCategoryTypes { get; set; }
    }
}