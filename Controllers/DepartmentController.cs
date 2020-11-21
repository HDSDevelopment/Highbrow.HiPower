using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Highbrow.HiPower.Services;
using Highbrow.HiPower.Models;
using Highbrow.HiPower.ViewModels.DepartmentVM;

namespace Highbrow.HiPower.Controllers
{
	public class DepartmentController : Controller
	{
		IDepartmentService _departmentService;

		public DepartmentController(IDepartmentService departmentService)
		{
			_departmentService = departmentService;
		}

        public IActionResult Index()
        {
            return RedirectToAction("List", "Department");
        }

        [HttpGet, ActionName("Add")]
        public IActionResult AddGet()
        {
            return View("Add", new Department());
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Add")]
        public async Task<IActionResult> AddPost(Department department)
        {
            ServiceResult result = ServiceResult.Failure;

            if (ModelState.IsValid)
            {
                try
                {
                    result = await _departmentService.Add(department);

                    if (result == ServiceResult.Success)
                        return RedirectToAction("List", "Department");
                }
                catch (Exception ex)
                {
                    //Perform logging here
                    return View("~/Views/Home/Error/500.cshtml");
                }
            }
            return View(department);
        }

        [HttpGet, ActionName("Update")]
        public async Task<IActionResult> UpdateGet(int id)
        {
            Department department = await _departmentService.Details(id);

            if (department != null)
                return View("Update", department);

            return View("Error");
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Update")]
        public async Task<IActionResult> UpdatePost(int id, Department department)
        {
            ServiceResult result = ServiceResult.Failure;

            if (ModelState.IsValid)
            {
                try
                {
                    result = await _departmentService.Update(id, department);

                    if (result == ServiceResult.Success)
                        return RedirectToAction("List", "Department");
                }
                catch (Exception ex)
                {
                    //Perform logging here
                    return View("~/Views/Home/Error/500.cshtml");
                }
            }
            return View("Update", department);
        }

        //[HttpGet, ActionName("Delete")]
        //public async Task<IActionResult> DeleteGet(int? id)
        //{
        //    int idValue = id.Value;
        //    Department department = await _departmentService.Details(idValue);

        //    if (department != null)
        //        return View("Delete", department);
        //    else
        //    {
        //        Response.StatusCode = 404;
        //        return View("~/Views/Department/NotFound.cshtml", idValue);
        //    }
        //}

        
        [HttpGet, ActionName("Delete")]
        public async Task<IActionResult> DeleteGet(int id)
        {
            try
            {
                ServiceResult result = await _departmentService.Delete(id);

                if (result == ServiceResult.Success)
                    return RedirectToAction("List", "Department");
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
            List<Department> departments = null;

            try
            {
                departments = await _departmentService.List();

                if (departments != null)
                {
                    DepartmentListViewModel listViewModel = new DepartmentListViewModel();

                    listViewModel.ActiveDepartments = _departmentService.ListByIsActive(departments, true);
                    listViewModel.InActiveDepartments = _departmentService.ListByIsActive(departments, false);
                    listViewModel.ActiveCount = listViewModel.ActiveDepartments.Count;
                    listViewModel.InActiveCount = listViewModel.InActiveDepartments.Count;

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