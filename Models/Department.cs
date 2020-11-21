using System;
using System.ComponentModel.DataAnnotations;

namespace Highbrow.HiPower.Models
{
    public class Department
    {
        public int Id { get; set; }
        
        //[Required(ErrorMessage = "Required")]
        public string DepartmentName { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}