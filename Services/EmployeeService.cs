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
                employee = await _context.Employees.
                                         Include(n => n.Dependents)
                                         .Include(n => n.Educations)
                                         .Include(n => n.Experiences)
                                         .Include(n => n.BankDetail)
                                         .FirstOrDefaultAsync(n => n.Id == id);
            return employee;
        }

        public async Task<ServiceResponse<Employee>> Add(Employee employee)
        {
            ServiceResponse<Employee> response = new ServiceResponse<Employee>();
            int recordsAffected = 0;
            response.Data = employee;
            response.Message = "Unable to add Employee";
            response.Result = ServiceResult.Failure;

            if (!await Exists(employee.OfficeId))
            {
                employee.CreatedAt = DateTime.Now;
                employee.UpdatedAt = employee.CreatedAt;

                await _context.AddAsync(employee);
                recordsAffected = await _context.SaveChangesAsync();

                if (recordsAffected != 0)
                {
                    response.Message = "Employee Successfully added";
                    response.Result = ServiceResult.Success;
                }
                return response;
            }
            return response;
        }

        public async Task<ServiceResponse<Employee>> Update(Employee employee)
        {
            ServiceResponse<Employee> response = new ServiceResponse<Employee>();
            int recordsAffected = 0;
            response.Data = employee;
            response.Message = "Unable to update Employee";
            response.Result = ServiceResult.Failure;

            string officeId = await GetOfficeID(employee.Id);

            if (await Exists(employee.Id) && employee.OfficeId == officeId)
            {
                employee.UpdatedAt = DateTime.Now;

                _context.Update(employee);
                recordsAffected = await _context.SaveChangesAsync();

                if (recordsAffected != 0)
                {
                    response.Message = "Employee Successfully added";
                    response.Result = ServiceResult.Success;
                }
                return response;
            }
            return response;
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
                              OfficeId = employee.OfficeId,
                              CardRFId = employee.CardRFId,
                              EmployeeName = employee.EmployeeName,
                              DepartmentName = employee.Department.DepartmentName,
                              DesignationName = employee.Designation.DesignationName,
                              PersonalMail = employee.PersonalMail,
                              PhotoFileName = employee.PhotoFileName,
                              SkypeId = employee.SkypeId,
                              FacebookId = employee.FacebookId,
                              LinkedinId = employee.LinkedinId
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
                                OfficeId = employee.OfficeId,
                                CardRFId = employee.CardRFId,
                                EmployeeName = employee.EmployeeName,
                                DepartmentName = employee.Department.DepartmentName,
                                DesignationName = employee.Designation.DesignationName,
                                PersonalMail = employee.PersonalMail,
                                PhotoFileName = employee.PhotoFileName,
                                SkypeId = employee.SkypeId,
                                FacebookId = employee.FacebookId,
                                LinkedinId = employee.LinkedinId,
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

        public async Task<List<EmployeeNameResponse>> ListActiveEmployeeNames()
        {
            if (await _context.Employees.AnyAsync())
                return await (from employee in _context.Employees.AsNoTracking()
                              where employee.IsActive == true
                              select new EmployeeNameResponse
                              {
                                  Id = employee.Id,
                                  EmployeeName = employee.EmployeeName
                              })
                                  .ToListAsync();

            return null;
        }

        public async Task<List<EmployeeListResponse>> ListInactiveEmployees(EmployeeSearchCriteria criteria)
        {
            return await ListByIsActive(criteria, false);
        }

        public async Task<bool> Exists(long id)
        {
            long idToFind = 0;

            idToFind = await (from employee in _context.Employees.AsNoTracking()
                              where employee.Id == id
                              select employee.Id)
                                        .FirstOrDefaultAsync();

            return idToFind != 0 ? true : false;
        }

        public async Task<bool> Exists(string officeId)
        {
            long id = 0;

            id = await (from employee in _context.Employees.AsNoTracking()
                        where employee.OfficeId == officeId
                        select employee.Id)
                                        .FirstOrDefaultAsync();

            return id != 0 ? true : false;
        }

        public async Task<string> GetOfficeID(long id)
        {
            string officeId = "";

            officeId = await (from employee in _context.Employees.AsNoTracking()
                              where employee.Id == id
                              select employee.OfficeId)
                                        .FirstOrDefaultAsync();
            return officeId;
        }
    }
}