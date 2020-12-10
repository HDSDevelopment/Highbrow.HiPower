using System;
using System.ComponentModel.DataAnnotations;

namespace Highbrow.HiPower.Models
{
    public class Dependent
    {
        public int Id { get; set; }
        
        //[Required(ErrorMessage = "Required")]
        public string DependentName { get; set; }

        public RelationType Relation { get; set; }

        public DateTime DateOfBirth { get; set; }

        public long? EmployeeId { get; set; }
    }
}