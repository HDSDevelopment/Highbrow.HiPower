using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Highbrow.HiPower.Models;
using Highbrow.HiPower.Data;

namespace Highbrow.HiPower.Services
{
    public class DepartmentService : IDepartmentService
    {
        HiPowerContext _context;
        public DepartmentService(HiPowerContext context)
        {
            _context = context;
        }

        public async Task<Department> Details(int? id)
        {
            Department department = null;

            if (id != null)
                department = await _context.Departments.AsNoTracking()
                                                .FirstOrDefaultAsync(n => n.Id == id);

            return department;
        }

        public async Task<ServiceResult> Add(Department department)
        {
            ServiceResult result = ServiceResult.Failure;

            department.CreatedAt = DateTime.Now;
            department.UpdatedAt = department.CreatedAt;

            await _context.AddAsync(department);
            int recordsAffected = await _context.SaveChangesAsync();

            if (recordsAffected != 0)
                result = ServiceResult.Success;

            return result;
        }

        public async Task<ServiceResult> Update(int id, Department department)
        {
            ServiceResult result = ServiceResult.Failure;
            int recordsAffected = 0;

            if (id == department.Id && await Exists(id))
            {
                department.UpdatedAt = DateTime.Now;

                _context.Departments.Update(department);
                recordsAffected = await _context.SaveChangesAsync();
            }

            if (recordsAffected != 0)
                result = ServiceResult.Success;

            return result;
        }

        public async Task<ServiceResult> Delete(int id)
        {
            ServiceResult result = ServiceResult.Failure;
            int recordsAffected = 0;

            if (await Exists(id))
            {
                Department department = await _context.Departments.FindAsync(id);
                _context.Departments.Remove(department);
                recordsAffected = await _context.SaveChangesAsync();
            }

            if (recordsAffected != 0)
                result = ServiceResult.Success;

            return result;
        }

        public async Task<List<Department>> List()
        {
            List<Department> departments = null;

            if (await _context.Departments.AnyAsync())
                departments = await _context.Departments.AsNoTracking().ToListAsync();

            return departments;
        }

        public List<Department> ListByIsActive(List<Department> departments, bool isActive)
        {
            List<Department> departmentsByIsActive = null;

            if (departments != null)
                departmentsByIsActive = departments.Where(n => n.IsActive == isActive)
                                                    .ToList();
            return departmentsByIsActive;
        }

        public async Task<bool> Exists(int id)
        {
            Department department = await _context.Departments.AsNoTracking().FirstOrDefaultAsync(n => n.Id == id);
            return department != null ? true : false;
        }
    }
}