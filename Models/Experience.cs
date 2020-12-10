using System;

namespace Highbrow.HiPower.Models
{
    public class Experience
    {
        public int Id { get; set; }

        public string CompanyName { get; set; }

        public string Designation { get; set; }

        public DateTime? FromDate { get; set; }

        public DateTime? ToDate { get; set; }

        public string Duration { get; set; }

        public long? EmployeeId { get; set; }
    }
}