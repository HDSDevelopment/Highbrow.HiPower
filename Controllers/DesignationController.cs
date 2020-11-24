using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Highbrow.HiPower.Services;
using Highbrow.HiPower.Models;
using Highbrow.HiPower.ViewModels.DesignationVM;

namespace Highbrow.HiPower.Controllers
{
    public class DesignationController : Controller
    {
        IDesignationService _designationService;

        public DesignationController(IDesignationService designationService)
        {
            _designationService = designationService;
        }
        public IActionResult Index()
        {
            return RedirectToAction("List", "Designation");
        }

        [HttpGet, ActionName("Add")]
        public IActionResult AddGet()
        {            
            return View("Add", new DesignationAddUpdateViewModel());
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Add")]
        public async Task<IActionResult> AddPost(DesignationAddUpdateViewModel designationViewModel)
        {
            ServiceResult result = ServiceResult.Failure;

            if (ModelState.IsValid)
            {
                try
                {
                    Designation designation = new Designation
                    {
                        DesignationName = designationViewModel.DesignationName,
                        LeaveApprovalLevel = designationViewModel.LeaveApprovalLevel
                    };

                    result = await _designationService.Add(designation);

                    if (result == ServiceResult.Success)
                        return RedirectToAction("List", "Designation");
                }
                catch (Exception ex)
                {
                    //Perform logging here
                    return View("~/Views/Home/Error/500.cshtml");
                }
            }
            return View(designationViewModel);
        }

        [HttpGet, ActionName("Update")]
        public async Task<IActionResult> UpdateGet(int id)
        {
            Designation designation = await _designationService.Details(id);

            if (designation != null)
            {
                DesignationAddUpdateViewModel designationViewModel = new DesignationAddUpdateViewModel() 
                {Id = designation.Id,
                    DesignationName = designation.DesignationName,
                    CreatedAt = designation.CreatedAt,
                    IsActive = designation.IsActive,
                    LeaveApprovalLevel = designation.LeaveApprovalLevel};

                return View("Update", designationViewModel);
            }
            return View("Error");
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Update")]
        public async Task<IActionResult> UpdatePost(DesignationAddUpdateViewModel designationViewModel)
        {
            ServiceResult result = ServiceResult.Failure;

            if (ModelState.IsValid)
            {
                try
                {
                    Designation designation = new Designation 
                    {
                        Id = designationViewModel.Id,
                        DesignationName = designationViewModel.DesignationName,
                    LeaveApprovalLevel = designationViewModel.LeaveApprovalLevel};

                    result = await _designationService.Update(designation);

                    if (result == ServiceResult.Success)
                        return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    //Perform logging here
                    return View("~/Views/Home/Error/500.cshtml");
                }
            }
            return View("Update", designationViewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int id)
        {
            try
            {
                ServiceResult result = await _designationService.Delete(id);

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
            List<Designation> allDesignations = null;

            try
            {
                allDesignations = await _designationService.List();

                if (allDesignations != null)
                {
                    DesignationListViewModel listViewModel = new DesignationListViewModel();

            listViewModel.ActiveDesignations = await _designationService.ListActiveDesignations();
        listViewModel.InactiveDesignations = await _designationService.ListInactiveDesignations();
        
                    listViewModel.ActiveCount = await _designationService.GetActiveCount();
                    listViewModel.InactiveCount = await _designationService.GetInactiveCount();

                    return View("List", listViewModel);
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