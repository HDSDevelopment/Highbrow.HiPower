using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Highbrow.HiPower.Models;
using Highbrow.HiPower.Data;
using Highbrow.HiPower.DTO;

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
            
            if(id != null)
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

        public async Task<ServiceResult> Update(Department department)
        {
            ServiceResult result = ServiceResult.Failure;
            int recordsAffected = 0;            

            if (await Exists(department.Id))
            {
                department.UpdatedAt = DateTime.Now;
                
                _context.Update(department);
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

            if(await Exists(id))
            {
                Department department = await _context.Departments.FindAsync(id);
                _context.Departments.Remove(department);
                recordsAffected = await _context.SaveChangesAsync();
            }

            if(recordsAffected != 0)
                result = ServiceResult.Success;

                return result;
        }

        public async Task<List<Department>> List()
        {
            List<Department> departments = null;

            if(await _context.Departments.AnyAsync())
                departments = await _context.Departments.AsNoTracking().ToListAsync();

                return departments;
        }

        private async Task<List<Department>> ListByIsActive(bool isActive)
        {
            return await (from department in _context.Departments.AsNoTracking()
                         where department.IsActive == isActive
                         select department)
                        .ToListAsync();
        }

        public async Task<List<Department>> ListActiveDepartments()
        {
            return await ListByIsActive(true);
        }

        public async Task<int> GetActiveCount()
        {
            List<Department> activeDepartments = await ListActiveDepartments();
            
            if (activeDepartments != null)
                return activeDepartments.Count;
            return 0;
        }

        public async Task<List<Department>> ListInactiveDepartments()
        {
            return await ListByIsActive(false);
        }

        public async Task<int> GetInactiveCount()
        {
            List<Department> inactiveDepartments = await ListInactiveDepartments();
            
            if (inactiveDepartments != null)
                return inactiveDepartments.Count;
            return 0;
        }

        public async Task<List<DepartmentNameResponse>> ListActiveDepartmentNames()
        {
            List<DepartmentNameResponse> departments = 
            await (from department in _context.Departments.AsNoTracking()
            where department.IsActive == true
            select new DepartmentNameResponse{
                                            Id = department.Id,
                                            DepartmentName = department.DepartmentName
                                            }).ToListAsync();

                                            return departments;
        }

        public async Task<bool> Exists(int id)
        {
            Department department = await _context.Departments.AsNoTracking().FirstOrDefaultAsync(n => n.Id == id);
            return department != null ? true : false;
        }
    }
}