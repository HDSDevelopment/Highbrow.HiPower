using System.Collections.Generic;
using Highbrow.HiPower.ViewModels;

namespace Highbrow.HiPower.ViewModels.ShiftVM
{
    public class ShiftListViewModel
    {
        public List<ShiftDetailsViewModel> ActiveShifts { get; set; }

        public List<ShiftDetailsViewModel> InactiveShifts { get; set; }

        public int ActiveCount { get; set; }

        public int InactiveCount { get; set; }
        public ShiftListViewModel()
        {
            ActiveShifts = new List<ShiftDetailsViewModel>();
            InactiveShifts = new List<ShiftDetailsViewModel>();
        }
    }
}