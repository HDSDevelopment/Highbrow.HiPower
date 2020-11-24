using System;
using System.ComponentModel.DataAnnotations;

namespace Highbrow.HiPower.Models
{
    public class WFH
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Required")]
        public string WFHName { get; set; }

        public float? DaysPerMonth { get; set; }

        public bool IsUnlimited { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}