using System.Collections.Generic;
using Highbrow.HiPower.ViewModels;
using Highbrow.HiPower.Models;


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
        public void SetActiveShifts(List<Shift> activeShifts)
        {
            SetShiftsViewModel(activeShifts, ActiveShifts);
        }

        public void SetInactiveShifts(List<Shift> inactiveShifts)
        {
            SetShiftsViewModel(inactiveShifts, InactiveShifts);
        }

        void SetShiftsViewModel(List<Shift> shifts, List<ShiftDetailsViewModel> shiftsViewModel)
        {
            ShiftDetailsViewModel detailsViewModel;

            if (shifts != null)
            {
                foreach (Shift shift in shifts)
                {
                    detailsViewModel = new ShiftDetailsViewModel();
                    detailsViewModel.Id = shift.Id;
                    detailsViewModel.ShiftName = shift.ShiftName;
                    detailsViewModel.SetStartTime(shift.StartTimeInSeconds);
                    detailsViewModel.SetEndTime(shift.EndTimeInSeconds);
                    detailsViewModel.SetBufferTime(shift.BufferTimeInSeconds);
                    shiftsViewModel.Add(detailsViewModel);
                }
            }
        }
    }
}