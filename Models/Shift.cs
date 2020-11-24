using System;
using System.ComponentModel.DataAnnotations;

namespace Highbrow.HiPower.Models
{
    public class Shift
    {
        public int Id { get; set; }

        [Required]
        public string ShiftName { get; set; }

        public double StartTimeInSeconds { get; set; }        

        public double EndTimeInSeconds { get; set; }

        public double BufferTimeInSeconds { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}