using System.Collections.Generic;
using Highbrow.HiPower.Models;

namespace Highbrow.HiPower.ViewModels.DepartmentVM
{
    public class DepartmentListViewModel
    {
        public List<Department> ActiveDepartments { get; set; }

        public List<Department> InActiveDepartments { get; set; }

        public int ActiveCount { get; set; }

        public int InActiveCount { get; set; }
    }
}