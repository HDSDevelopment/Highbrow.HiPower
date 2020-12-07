using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Highbrow.HiPower.Services;
using Highbrow.HiPower.ViewModels.LeaveCategoryVM;
using Highbrow.HiPower.Models;

namespace Highbrow.HiPower.Controllers
{
    public class LeaveCategoryController : Controller
    {
        ILeaveCategoryService _leaveCategoryService;

        ILeaveTypeService _leaveTypeService;

        public LeaveCategoryController(ILeaveCategoryService leaveCategoryService,
                                    ILeaveTypeService leaveTypeService)
        {
            _leaveCategoryService = leaveCategoryService;
            _leaveTypeService = leaveTypeService;
        }

        public async Task<IActionResult> List()
        {
            try
            {
                LeaveCategoryListViewModel listViewModel = new LeaveCategoryListViewModel();

                List<LeaveCategory> activeLeaveCategories = await _leaveCategoryService.ListActiveLeaveCategories();

                List<LeaveCategory> inactiveLeaveCategories = await _leaveCategoryService.ListInactiveLeaveCategories();

                listViewModel.SetActiveLeaveCategories(activeLeaveCategories);
                listViewModel.SetInactiveLeaveCategories(inactiveLeaveCategories);

                listViewModel.ActiveCount = await _leaveCategoryService.GetActiveCount();
                listViewModel.InactiveCount = await _leaveCategoryService.GetInactiveCount();

                return View("List", listViewModel);
            }
            catch (Exception ex)
            {
                //Perform logging here
                return View("~/Views/Home/Error/500.cshtml");
            }
        }

        [HttpGet, ActionName("Update")]
        public async Task<IActionResult> UpdateGet(int id)
        {
            LeaveCategory leaveCategory = await _leaveCategoryService.Details(id);

            List<LeaveTypeNameInfo> allLeaveTypes = await _leaveTypeService.ListLeaveTypeNames();

            if (leaveCategory != null)
            {
                LeaveCategoryUpdateViewModel viewModel = new LeaveCategoryUpdateViewModel();

                viewModel.SetViewModel(leaveCategory, allLeaveTypes);

                return View("Update", viewModel);
            }
            return View("Error");
        }

        [HttpPost, ActionName("Update")]
        public async Task<IActionResult> UpdatePost(LeaveCategoryUpdateViewModel viewModel)
        {
            ServiceResult result = ServiceResult.Failure;

            if (ModelState.IsValid)
            {
                try
                {
                    LeaveCategory leaveCategory = viewModel.GetLeaveCategory();

                    result = await _leaveCategoryService.Update(leaveCategory, viewModel.SelectedLeaveTypes);

                    if (result == ServiceResult.Success)
                        return RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    //Perform logging here
                    return View("~/Views/Home/Error/500.cshtml");
                }
            }
            return View("Update", viewModel);
        }
    }
}