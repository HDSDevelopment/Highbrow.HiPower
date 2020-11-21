using System.Collections.Generic;
using Highbrow.HiPower.Models;

namespace Highbrow.HiPower.ViewModels.DesignationVM
{
    public class DesignationListViewModel
    {
        public List<Designation> ActiveDesignations { get; set;}
        public List<Designation> InActiveDesignations { get; set;}

        public int ActiveCount {get; set;}

        public int InActiveCount { get; set; }
    }
}