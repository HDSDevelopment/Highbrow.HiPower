using System.Collections.Generic;
using Highbrow.HiPower.Models;

namespace Highbrow.HiPower.ViewModels.LeaveTypeVM
{
    public class LeaveTypeListViewModel
    {
        public List<LeaveType> ActiveLeaveTypes { get; set;}
        
        public List<LeaveType> InactiveLeaveTypes { get; set;}

        public int ActiveCount {get; set;}

        public int InactiveCount { get; set; }
    }
}