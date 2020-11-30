using System;
using System.ComponentModel.DataAnnotations;

namespace Highbrow.HiPower.Models
{
    public class Holiday
    {
        public int Id { get; set; }

        public string HolidayName { get; set; }

        [DisplayFormat(DataFormatString = @"{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime HolidayDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }
    }
}