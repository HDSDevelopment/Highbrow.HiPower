using System;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Highbrow.HiPower.Services;
using Highbrow.HiPower.Models;
using Highbrow.HiPower.ViewModels.EmployeeVM;

namespace Highbrow.HiPower.Controllers
{
    public class EmployeeController : Controller
    {
        IEmployeeService _employeeService;
        IDepartmentService _departmentService;
        IDesignationService _designationService;
        IShiftService _shiftService;
        IWFHService _wfhService;

        public EmployeeController(IEmployeeService employeeService,
                                    IDepartmentService departmentService,
                                    IDesignationService designationService,
                                    IShiftService shiftService,
                                    IWFHService wfhService)
        {
            _employeeService = employeeService;
            _departmentService = departmentService;
            _designationService = designationService;
            _shiftService = shiftService;
            _wfhService = wfhService;
        }

        [HttpGet, ActionName("Add")]
        public IActionResult AddGet()
        {
            return View("Add", new EmployeeAddViewModel());
        }

        [HttpPost, ActionName("Add")]
        public async Task<IActionResult> AddPost(EmployeeAddViewModel empViewModel)
        {
            ServiceResult result = ServiceResult.Failure;
            Employee employee;

            if(ModelState.IsValid)
            {
                try
                {
                    employee = empViewModel.GetEmployee();
                    
                    if(empViewModel.Id == 0)
                    {
                        result = await _employeeService.Add(employee);
                    }                        
                    else
                    {
                        result = await _employeeService.Update(employee);
                    }                        
                    
                    if(result == ServiceResult.Success)
                    {
                        empViewModel.CurrentTab ++;
                        
                        int currentTabNumber = (int)empViewModel.CurrentTab;
                        const int lastTab = 9;
                        
                        if(currentTabNumber >= lastTab)
                        {
                            return RedirectToAction("List");
                        }
                    }                    
                        return View(empViewModel);
                }
                catch (Exception ex)
                {
                    //Perform logging here
                    return View("~/Views/Home/Error/500.cshtml");
                }
            }
            return View(empViewModel);
        }

        public async Task<IActionResult> List()
        {
            EmployeeListViewModel viewModel = new EmployeeListViewModel();
        viewModel.ActiveEmployees = await _employeeService.ListActiveEmployees();
        viewModel.InactiveEmployees = await _employeeService.ListInactiveEmployees();
            viewModel.ActiveCount = await _employeeService.GetActiveCount();
            viewModel.InactiveCount = await _employeeService.GetInactiveCount();
            viewModel.Departments = await _departmentService.ListDepartmentNames();

            return View("List", viewModel);
        }

        public async Task<IActionResult> ListBySearchCriteria(EmployeeSearchCriteria criteria)
        {
            EmployeeListViewModel viewModel = new EmployeeListViewModel();
    viewModel.ActiveEmployees = await _employeeService.ListActiveEmployees(criteria);
            viewModel.ActiveCount = await _employeeService.GetActiveCount();
    viewModel.InactiveEmployees = await _employeeService.ListInactiveEmployees();
            viewModel.InactiveCount = await _employeeService.GetInactiveCount();
            viewModel.Departments = await _departmentService.ListDepartmentNames();

            return View("List", viewModel);
        }
    }
}