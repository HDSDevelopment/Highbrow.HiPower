using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Highbrow.HiPower.Models;
using Highbrow.HiPower.Data;
using Highbrow.HiPower.DTO;
using Highbrow.HiPower.Utilities;

namespace Highbrow.HiPower.Services
{
    public class EmployeeService : IEmployeeService
    {
        HiPowerContext _context;
        public EmployeeService(HiPowerContext context)
        {
            _context = context;
        }

        public async Task<Employee> Details(int? id)
        {
            Employee employee = null;

            if (id != null)
                employee = await _context.Employees
                                        .FirstOrDefaultAsync(n => n.Id == id);

            return employee;
        }

        public async Task<ServiceResult> Add(Employee employee)
        {
            ServiceResult result = ServiceResult.Failure;

            if (await Exists(employee.OfficeId))
                return result;

            employee.CreatedAt = DateTime.Now;
            employee.UpdatedAt = employee.CreatedAt;

            await _context.AddAsync(employee);
            int recordsAffected = await _context.SaveChangesAsync();

            if (recordsAffected != 0)
                result = ServiceResult.Success;

            return result;
        }

        public async Task<ServiceResult> Update(Employee employee)
        {
            ServiceResult result = ServiceResult.Failure;
            int recordsAffected = 0;

            if (await Exists(employee.Id))
            {
                employee.UpdatedAt = DateTime.Now;

                _context.Update(employee);
                recordsAffected = await _context.SaveChangesAsync();
            }

            if (recordsAffected != 0)
                result = ServiceResult.Success;

            return result;
        }

        private async Task<List<EmployeeListResponse>> ListByIsActive(bool isActive)
        {
            return await (from employee in _context.Employees.AsNoTracking()
                        .Include(n => n.Department)
                        .Include(n => n.Designation)
                          where employee.IsActive == isActive
                          select new EmployeeListResponse
                          {
                              Id = employee.Id,
                              EmployeeName = employee.EmployeeName,
                              DepartmentName = employee.Department.DepartmentName,
                              DesignationName = employee.Designation.DesignationName
                          })
                        .ToListAsync();
        }

        public async Task<List<EmployeeListResponse>> ListActiveEmployees()
        {
            return await ListByIsActive(true);
        }

        public async Task<int> GetActiveCount()
        {
            return await (from employee in _context.Employees.AsNoTracking()
                          where employee.IsActive == true
                          select employee.Id)
                                        .CountAsync();
        }

        public async Task<List<EmployeeListResponse>> ListInactiveEmployees()
        {
            return await ListByIsActive(false);
        }

        public async Task<int> GetInactiveCount()
        {
            return await (from employee in _context.Employees.AsNoTracking()
                          where employee.IsActive == false
                          select employee.Id)
                                        .CountAsync();
        }

        private async Task<List<EmployeeListResponse>> ListByIsActive(EmployeeSearchCriteria searchCriteria, bool isActive)
        {
            var employees = from employee in _context.Employees
                        .Include(n => n.Department)
                        .Include(n => n.Designation)
                            where employee.IsActive == isActive
                            select new
                            {
                                Id = employee.Id,

                                EmployeeName = employee.EmployeeName,

                                DepartmentName = employee.Department.DepartmentName,

                                DesignationName = employee.Designation.DesignationName,

                                OfficeId = employee.OfficeId,

                                DepartmentId = employee.DepartmentId
                            };

            string officeId = StringUtility.GetTrimmedUpperCase(searchCriteria.OfficeId);

            if (!string.IsNullOrEmpty(searchCriteria.OfficeId))
                employees = from employee in employees
                            where employee.OfficeId.ToUpper() == officeId
                            select employee;

            string employeeName = StringUtility.GetTrimmedLowerCase(searchCriteria.EmployeeName);

            if (!string.IsNullOrEmpty(employeeName))
                employees = from employee in employees
                            where employee.EmployeeName.ToLower() == employeeName
                            select employee;

            if (searchCriteria.DepartmentId != null)
                employees = from employee in employees
                            where employee.DepartmentId == searchCriteria.DepartmentId
                            select employee;

            List<EmployeeListResponse> response = await (from employee in employees.AsNoTracking()
                                                         select new EmployeeListResponse
                                                         {
                                                             Id = employee.Id,

                                                             EmployeeName = employee.EmployeeName,

                                                             DepartmentName = employee.DepartmentName,

                                                             DesignationName = employee.DesignationName
                                                         })
                                            .ToListAsync();

            return response;
        }

        public async Task<List<EmployeeListResponse>> ListActiveEmployees(EmployeeSearchCriteria criteria)
        {
            return await ListByIsActive(criteria, true);
        }

        public async Task<List<EmployeeListResponse>> ListInactiveEmployees(EmployeeSearchCriteria criteria)
        {
            return await ListByIsActive(criteria, false);
        }

        public async Task<bool> Exists(long id)
        {
            Employee employee = await _context.Employees.AsNoTracking().FirstAsync(n => n.Id == id);
            return employee != null ? true : false;
        }

        public async Task<bool> Exists(string officeId)
        {
            Employee employee = await _context.Employees.AsNoTracking()
                                        .FirstAsync(n => n.OfficeId == officeId);
            return employee != null ? true : false;
        }
    }
}