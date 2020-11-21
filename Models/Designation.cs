using System;
using System.ComponentModel.DataAnnotations;

namespace Highbrow.HiPower.Models
{
    public class Designation
    {
        public int Id { get; set; }
        
        //[Required(ErrorMessage = "Required")]
        public string DesignationName { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public int LeaveApprovalLevel { get; set; } 
    }
}