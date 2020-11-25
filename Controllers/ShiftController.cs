using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Highbrow.HiPower.ViewModels.ShiftVM;
using Highbrow.HiPower.Services;
using Highbrow.HiPower.Models;

namespace Highbrow.HiPower.Controllers
{
    public class ShiftController : Controller
    {
        IShiftService _shiftService;

        public ShiftController(IShiftService shiftService)
        {
            _shiftService = shiftService;
        }

        public IActionResult Index()
        {
            return View("List");
        }

        [HttpGet, ActionName("Add")]
        public IActionResult AddGet()
        {
            return View("Add", new ShiftAddUpdateViewModel());
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Add")]
        public async Task<IActionResult> AddPost(ShiftAddUpdateViewModel shiftVM)
        {
            ServiceResult result = ServiceResult.Failure;

            if (ModelState.IsValid)
            {
                try
                {
                    Shift shift = new Shift();
                    shift.ShiftName = shiftVM.ShiftName;
                    shift.StartTimeInSeconds = shiftVM.GetStartTimeInSeconds();
                    shift.EndTimeInSeconds = shiftVM.GetEndTimeInSeconds();
                    shift.BufferTimeInSeconds = shiftVM.GetBufferTimeInSeconds();
                    shift.IsActive = shiftVM.IsActive;

                    result = await _shiftService.Add(shift);

                    if (result == ServiceResult.Success)
                        return RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    //Perform logging here
                    return View("~/Views/Home/Error/500.cshtml");
                }
            }
            return View(shiftVM);
        }

        [HttpGet, ActionName("Update")]
        public async Task<IActionResult> UpdateGet(int id)
        {
            Shift shift = await _shiftService.Details(id);

            if (shift != null)
            {
                ShiftAddUpdateViewModel shiftVM = new ShiftAddUpdateViewModel();

                shiftVM.ShiftName = shift.ShiftName;
                //shiftVM.SetStartTime(shift.StartTimeInSeconds);
                shiftVM.StartHour = shiftVM.SetStartTime(shift.StartTimeInSeconds).Hours;
                shiftVM.StartMinute = shiftVM.SetStartTime(shift.StartTimeInSeconds).Minutes;
                shiftVM.EndHour = shiftVM.SetEndTime(shift.EndTimeInSeconds).Hours;
                shiftVM.EndMinute = shiftVM.SetEndTime(shift.EndTimeInSeconds).Minutes;
                shiftVM.BufferHour = shiftVM.SetBufferTime(shift.BufferTimeInSeconds).Hours;
                shiftVM.BufferMinute = shiftVM.SetBufferTime(shift.BufferTimeInSeconds).Minutes;
                //shiftVM.SetEndTime(shift.EndTimeInSeconds);
                //shiftVM.SetBufferTime(shift.BufferTimeInSeconds);
                shiftVM.IsActive = shift.IsActive;

                return View("Update", shiftVM);
            }
            return View("Error");
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Update")]
        public async Task<IActionResult> UpdatePost(ShiftAddUpdateViewModel shiftVM)
        {
            ServiceResult result = ServiceResult.Failure;

            if (ModelState.IsValid)
            {
                try
                {
                    Shift shift = new Shift();
                    shift.Id = shiftVM.Id;
                    shift.ShiftName = shiftVM.ShiftName;
                    shift.StartTimeInSeconds = shiftVM.GetStartTimeInSeconds();
                    shift.EndTimeInSeconds = shiftVM.GetEndTimeInSeconds();
                    shift.BufferTimeInSeconds = shiftVM.GetBufferTimeInSeconds();
                    shift.IsActive = shiftVM.IsActive;
                    shift.CreatedAt = shiftVM.CreatedAt;

                    result = await _shiftService.Update(shift);

                    if (result == ServiceResult.Success)
                        return RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    //Perform logging here
                    return View("~/Views/Home/Error/500.cshtml");
                }
            }
            return View("Update", shiftVM);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int id)
        {
            try
            {
                ServiceResult result = await _shiftService.Delete(id);

                if (result == ServiceResult.Success)
                    return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                //Perform logging here
                return View("~/Views/Home/Error/500.cshtml");
            }
            return View("Error");
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            try
            {
                ShiftListViewModel listViewModel = new ShiftListViewModel();
                List<Shift> activeShifts = await _shiftService.ListActiveShifts();

                ShiftDetailsViewModel detailsViewModel;

                if (activeShifts != null)
                {
                    foreach (Shift activeShift in activeShifts)
                    {
                        detailsViewModel = new ShiftDetailsViewModel();
                        detailsViewModel.Id = activeShift.Id;
                        detailsViewModel.ShiftName = activeShift.ShiftName;
                        detailsViewModel.SetStartTime(activeShift.StartTimeInSeconds);
                        detailsViewModel.SetEndTime(activeShift.EndTimeInSeconds);
                        detailsViewModel.SetBufferTime(activeShift.BufferTimeInSeconds);
                        listViewModel.ActiveShifts.Add(detailsViewModel);
                    }
                }

                List<Shift> inactiveShifts = await _shiftService.ListInactiveShifts();

                if (inactiveShifts != null)
                {
                    foreach (Shift inactiveShift in inactiveShifts)
                    {
                        detailsViewModel = new ShiftDetailsViewModel();
                        detailsViewModel.Id = inactiveShift.Id;
                        detailsViewModel.ShiftName = inactiveShift.ShiftName;
                        detailsViewModel.SetStartTime(inactiveShift.StartTimeInSeconds);
                        detailsViewModel.SetEndTime(inactiveShift.EndTimeInSeconds);
                        detailsViewModel.SetBufferTime(inactiveShift.BufferTimeInSeconds);
                        listViewModel.InactiveShifts.Add(detailsViewModel);
                    }
                }

                listViewModel.ActiveCount = await _shiftService.GetActiveCount();
                listViewModel.InactiveCount = await _shiftService.GetInactiveCount();

                return View("List", listViewModel);
            }
            catch (Exception ex)
            {
                //Perform logging here
                return View("~/Views/Home/Error/500.cshtml");
            }
            return View("Error");
        }
    }
}
