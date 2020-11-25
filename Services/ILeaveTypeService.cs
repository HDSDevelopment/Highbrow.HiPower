using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Highbrow.HiPower.Models;
using Highbrow.HiPower.Data;

namespace Highbrow.HiPower.Services
{
    public interface ILeaveTypeService
    {
    Task<LeaveType> Details(int? id);

    Task<ServiceResult> Add(LeaveType leaveType);

    Task<ServiceResult> Update(LeaveType leaveType);

    Task<ServiceResult> Delete(int id);

    Task<List<LeaveType>> List();

    Task<List<LeaveType>> ListActiveLeaveTypes();

    Task<List<LeaveType>> ListInactiveLeaveTypes();

    Task<int> GetActiveCount();

    Task<int> GetInactiveCount();
    }
}