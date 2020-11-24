using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Highbrow.HiPower.Services;
using Highbrow.HiPower.Models;
using Highbrow.HiPower.ViewModels.WFHVM;

namespace Highbrow.HiPower.Controllers
{
    public class WFHController : Controller
    {
        IWFHService _wfhService;

        public WFHController(IWFHService wfhService)
        {
            _wfhService = wfhService;
        }

        public IActionResult Index()
        {
            return RedirectToAction("List","WFH");
        }

        [HttpGet, ActionName("Add")]
        public IActionResult AddGet()
        {            
            return View("Add", new WFH());
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Add")]
        public async Task<IActionResult> AddPost(WFH wfh)
        {
            ServiceResult result = ServiceResult.Failure;

            if (ModelState.IsValid)
            {
                try
                {
                    if (wfh.IsUnlimited == true)
                        wfh.DaysPerMonth = null;
                    result = await _wfhService.Add(wfh);

                    if (result == ServiceResult.Success)
                        return RedirectToAction("List", "WFH");
                }
                catch (Exception ex)
                {
                    //Perform logging here
                    return View("~/Views/Home/Error/500.cshtml");
                }
            }
            return View(wfh);
        }

        [HttpGet, ActionName("Update")]
        public async Task<IActionResult> UpdateGet(int id)
        {
            WFH wfh = await _wfhService.Details(id);

            if (wfh != null)
                return View("Update", wfh);

            return View("Error");
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Update")]
        public async Task<IActionResult> UpdatePost(WFH wfh)
        {
            ServiceResult result = ServiceResult.Failure;

            if (ModelState.IsValid)
            {
                try
                {
                    if (wfh.IsUnlimited == true)
                        wfh.DaysPerMonth = null;
                    result = await _wfhService.Update(wfh);

                    if (result == ServiceResult.Success)
                        return RedirectToAction("List", "WFH");
                }
                catch (Exception ex)
                {
                    //Perform logging here
                    return View("~/Views/Home/Error/500.cshtml");
                }
            }
            return View("Update", wfh);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeletePost(int id)
        {
            try
            {
                ServiceResult result = await _wfhService.Delete(id);

                if (result == ServiceResult.Success)
                    return RedirectToAction("List", "WFH");
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
                    WFHListViewModel listViewModel = new WFHListViewModel();

                    listViewModel.ActiveWFHs = await _wfhService.ListActiveWFHs();
                    listViewModel.InactiveWFHs = await _wfhService.ListInactiveWFHs();
                    listViewModel.ActiveCount = await _wfhService.GetActiveCount();
                    listViewModel.InactiveCount = await _wfhService.GetInactiveCount();

                    return View("List", listViewModel);                
            }
            catch (Exception ex)
            {
                //Perform logging here
                return View("~/Views/Home/Error/500.cshtml");
            }            
        }
    }
}