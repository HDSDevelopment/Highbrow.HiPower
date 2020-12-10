using System.Collections.Generic;
using Highbrow.HiPower.DTO;

namespace Highbrow.HiPower.ViewModels.EmployeeVM
{
    public class EmployeeListViewModel
    {
        public List<EmployeeListResponse> ActiveEmployees { get; set; }

        public List<EmployeeListResponse> InactiveEmployees { get; set; }

        public int ActiveCount { get; set; }

        public int InactiveCount { get; set; }

        public List<DepartmentNameResponse> Departments {get; set;}
    }
}