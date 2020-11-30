using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Highbrow.HiPower.Services;
using Highbrow.HiPower.Models;
using Highbrow.HiPower.ViewModels.HolidayVM;

namespace Highbrow.HiPower.Controllers
{
    public class HolidayController : Controller
    {
        IHolidayService _holidayService;

        public HolidayController(IHolidayService holidayService)
        {
            _holidayService = holidayService;
        }

        [HttpGet, ActionName("Add")]
        public IActionResult AddGet()
        {
            return View("Add", new Holiday());
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Add")]
        public async Task<IActionResult> AddPost(Holiday holiday)
        {
            ServiceResult result = ServiceResult.Failure;

            if (ModelState.IsValid)
            {
                try
                {
                    result = await _holidayService.Add(holiday);

                    if (result == ServiceResult.Success)
                        return RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    //Perform logging here
                    return View("~/Views/Home/Error/500.cshtml");
                }
            }
            return View(holiday);
        }

        [HttpGet, ActionName("Update")]
        public async Task<IActionResult> UpdateGet(int id)
        {
            Holiday holiday = await _holidayService.Details(id);

            if (holiday != null)
            {
                return View("Update", holiday);
            }

            return View("Error");
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Update")]
        public async Task<IActionResult> UpdatePost(Holiday holiday)
        {
            ServiceResult result = ServiceResult.Failure;

            if (ModelState.IsValid)
            {
                try
                {
                    result = await _holidayService.Update(holiday);

                    if (result == ServiceResult.Success)
                        return RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    //Perform logging here
                    return View("~/Views/Home/Error/500.cshtml");
                }
            }
            return View("Update", holiday);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int id)
        {
            try
            {
                ServiceResult result = await _holidayService.Delete(id);

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
            List<Holiday> holidays = null;

            try
            {
                holidays = await _holidayService.List();

                if (holidays != null)
                {
                    List<HolidayListViewModel> viewModelList = new List<HolidayListViewModel>();
                    HolidayListViewModel viewModel;

                    foreach (Holiday holiday in holidays)
                    {
                        viewModel = new HolidayListViewModel();
                        viewModel.Id = holiday.Id;
                        viewModel.HolidayName = holiday.HolidayName;
                        viewModel.HolidayDate = string.Format("{0:dd-MMM-yyyy}", holiday.HolidayDate);
                        viewModelList.Add(viewModel);

                    }
                    return View("List", viewModelList);
                }
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
