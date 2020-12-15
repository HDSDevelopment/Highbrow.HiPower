using System;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Highbrow.HiPower.Services;
using Highbrow.HiPower.Models;
using Highbrow.HiPower.DTO;
using Highbrow.HiPower.ViewModels.EmployeeVM;

namespace Highbrow.HiPower.Controllers
{
    public class EmployeeController : Controller
    {
        IEmployeeService _employeeService;
        IDepartmentService _departmentService;

        public EmployeeController(IEmployeeService employeeService,
                                    IDepartmentService departmentService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;            
        }

        [HttpGet, ActionName("Add")]
        public async Task<IActionResult> AddGet()
        {
            EmployeeAddViewModel viewModel = new EmployeeAddViewModel();
            
            List<EmployeeNameResponse> supervisors = await _employeeService.ListActiveEmployeeNames();
            SetSupervisors(supervisors, viewModel.SupervisorsSelectList);

            List<DepartmentNameResponse> departments = await _departmentService.ListActiveDepartmentNames();
            SetDepartments(departments, viewModel.DepartmentsSelectList);

            return View("Add", viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Add")]
        public async Task<IActionResult> AddPost(EmployeeAddViewModel viewModel)
        {
            ServiceResponse<Employee> response = null;
            Employee employee;

            if (ModelState.IsValid)
            {
                try
                {
                    employee = viewModel.GetEmployee();

                    if (viewModel.Id == 0)
                        response = await _employeeService.Add(employee);

                    if (response.Result == ServiceResult.Success)
                    {
                        const int lastTab = 9;
                        int currentTabNumber = (int)viewModel.CurrentTab;

                        if (currentTabNumber >= lastTab)
                            return RedirectToAction("List");

                        viewModel.SetViewModel(response.Data);
                        currentTabNumber++;
                        viewModel.CurrentTab = (EmployeeTabs)currentTabNumber;
                    }
                    return View(viewModel);
                }
                catch (Exception ex)
                {
                    //Perform logging here
                    return View("~/Views/Home/Error/500.cshtml");
                }
            }
            return View(viewModel);
        }

        [HttpGet, ActionName("Update")]
        public async Task<IActionResult> UpdateGet(int? id)
        {
            Employee employee = await _employeeService.Details(id);
            
            EmployeeAddViewModel viewModel = new EmployeeAddViewModel();
            viewModel.SetViewModel(employee);
            
            List<EmployeeNameResponse> supervisors = await _employeeService.ListActiveEmployeeNames();
            SetSupervisors(supervisors, viewModel.SupervisorsSelectList);

            List<DepartmentNameResponse> departments = await _departmentService.ListActiveDepartmentNames();
            SetDepartments(departments, viewModel.DepartmentsSelectList);
            
            return View("Update", viewModel);
        }

        [ValidateAntiForgeryToken]
        [HttpPost, ActionName("Update")]
        public async Task<IActionResult> UpdatePost(EmployeeAddViewModel viewModel)
        {
            ServiceResponse<Employee> response = null;
            Employee employee;

            if (ModelState.IsValid)
            {
                try
                {
                    employee = viewModel.GetEmployee();

                    if (viewModel.Id != 0)
                        response = await _employeeService.Update(employee);

                    if (response.Result == ServiceResult.Success)
                    {
                        const int lastTab = 9;
                        int currentTabNumber = (int)viewModel.CurrentTab;

                        if (currentTabNumber >= lastTab)
                            return RedirectToAction("List");

                        viewModel.SetViewModel(response.Data);
                        currentTabNumber++;
                        viewModel.CurrentTab = (EmployeeTabs)currentTabNumber;
                    }
                    return View("Update", viewModel);
                }
                catch (Exception ex)
                {
                    //Perform logging here
                    return View("~/Views/Home/Error/500.cshtml");
                }
            }
            return View("Update", viewModel);
        }

        void SetSupervisors(List<EmployeeNameResponse> supervisors, List<SelectListItem> supervisorItems)
        {
            SelectListItem supervisorItem;

            if (supervisors != null)
            {
                foreach (EmployeeNameResponse supervisor in supervisors)
                {
                    supervisorItem = new SelectListItem { Text = supervisor.EmployeeName, Value = Convert.ToString(supervisor.Id) };

                    supervisorItems.Add(supervisorItem);
                }
            }
        }

        void SetDepartments(List<DepartmentNameResponse> departments, List<SelectListItem> departmentItems)
        {
            SelectListItem departmentItem;

            if (departments != null)
            {
                foreach (DepartmentNameResponse department in departments)
                {
                    departmentItem = new SelectListItem { Text = department.DepartmentName, Value = Convert.ToString(department.Id) };

                    departmentItems.Add(departmentItem);
                }
            }
        }

        public async Task<IActionResult> List()
        {
            EmployeeListViewModel viewModel = new EmployeeListViewModel();
        viewModel.ActiveEmployees = await _employeeService.ListActiveEmployees();
        viewModel.InactiveEmployees = await _employeeService.ListInactiveEmployees();
            viewModel.ActiveCount = await _employeeService.GetActiveCount();
            viewModel.InactiveCount = await _employeeService.GetInactiveCount();
            viewModel.Departments = await _departmentService.ListActiveDepartmentNames();

            return View("List", viewModel);
        }

        public async Task<IActionResult> ListBySearchCriteria(EmployeeSearchCriteria criteria)
        {
            EmployeeListViewModel viewModel = new EmployeeListViewModel();
    viewModel.ActiveEmployees = await _employeeService.ListActiveEmployees(criteria);    
    viewModel.InactiveEmployees = await _employeeService.ListInactiveEmployees(criteria);
    viewModel.ActiveCount = await _employeeService.GetActiveCount();
            viewModel.InactiveCount = await _employeeService.GetInactiveCount();
            viewModel.Departments = await _departmentService.ListActiveDepartmentNames();

            return View("List", viewModel);
        }
    }
}