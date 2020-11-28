using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Highbrow.HiPower.Services;
using Highbrow.HiPower.Models;
using Highbrow.HiPower.ViewModels.LeaveTypeVM;

namespace Highbrow.HiPower.Controllers
{
	public class LeaveTypeController : Controller
	{
        ILeaveTypeService _leaveTypeService;

        public LeaveTypeController(ILeaveTypeService leaveTypeService)
        {
            _leaveTypeService = leaveTypeService;
        }

		public IActionResult Index()
		{
			return RedirectToAction("List");
		}

		[HttpGet, ActionName("Add")]
        public IActionResult AddGet()
        {            
            return View("Add", new LeaveType());
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Add")]
        public async Task<IActionResult> AddPost(LeaveType leaveType)
        {
            ServiceResult result = ServiceResult.Failure;

            if (ModelState.IsValid)
            {
                try
                {
                    result = await _leaveTypeService.Add(leaveType);

                    if (result == ServiceResult.Success)
                        return RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    //Perform logging here
                    return View("~/Views/Home/Error/500.cshtml");
                }
            }
            return View(leaveType);
        }

        [HttpGet, ActionName("Update")]
        public async Task<IActionResult> UpdateGet(int id)
        {
            LeaveType leaveType = await _leaveTypeService.Details(id);

            if (leaveType != null)
                return View("Update", leaveType);

            return View("Error");
        }

		        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Update")]
        public async Task<IActionResult> UpdatePost(LeaveType leaveType)
        {
            ServiceResult result = ServiceResult.Failure;

            if (ModelState.IsValid)
            {
                try
                {
                    result = await _leaveTypeService.Update(leaveType);

                    if (result == ServiceResult.Success)
                        return RedirectToAction("List");
                }
                catch (Exception ex)
                {
                    //Perform logging here
                    return View("~/Views/Home/Error/500.cshtml");
                }
            }
            return View("Update", leaveType);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int id)
        {
            try
            {
                ServiceResult result = await _leaveTypeService.Delete(id);

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

        public async Task<IActionResult> List()
        {
            List<LeaveType> allLeaveTypes = null;

            try
            {
                allLeaveTypes = await _leaveTypeService.List();
                
                if(allLeaveTypes != null)
            {
                LeaveTypeListViewModel listViewModel = new LeaveTypeListViewModel();
                
                listViewModel.ActiveLeaveTypes = await _leaveTypeService.ListActiveLeaveTypes();
                listViewModel.InactiveLeaveTypes = await _leaveTypeService.ListInactiveLeaveTypes();
                
                listViewModel.ActiveCount = await _leaveTypeService.GetActiveCount();
                listViewModel.InactiveCount = await _leaveTypeService.GetInactiveCount();              
                
                return View("List", listViewModel);
            }
            }
            catch(Exception ex)
            {
                //Perform logging here
                return View("~/Views/Home/Error/500.cshtml");
            }            
            return View("Error");
        }
	}
}
