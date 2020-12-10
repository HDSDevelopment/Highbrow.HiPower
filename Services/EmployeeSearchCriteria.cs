using System;

namespace Highbrow.HiPower.Services
{
    public class EmployeeSearchCriteria
    {
        public string OfficeId { get; set; }

        public string EmployeeName { get; set; }

        public int? DepartmentId { get; set; }        
    }
}