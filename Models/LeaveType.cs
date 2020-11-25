using System;
using System.ComponentModel.DataAnnotations;

namespace Highbrow.HiPower.Models
{
    public class LeaveType
    {
        public int Id { get; set; }

        public string LeaveTypeName { get; set; }

        public string ShortCode { get; set; }
        
        public int TotalDays { get; set; }
        
        public GenderType Gender { get; set; }

        public MaritalStatusType MaritalStatus { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool IsActive { get; set; }
    }
}