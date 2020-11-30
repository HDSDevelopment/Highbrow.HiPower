using System;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using Highbrow.HiPower.Data;
using Microsoft.EntityFrameworkCore;
using Highbrow.HiPower.Models;

namespace Highbrow.HiPower.Services
{
public class DataGenerator
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        var context = new HiPowerContext(

            serviceProvider.GetRequiredService<DbContextOptions<HiPowerContext>>());
        
            if(context.CompanyProfiles.Any())
            {
                return;
            }

            context.CompanyProfiles.AddRange(

                new CompanyProfile
                {
                    Id = 1,

                    CompanyName = "Highbrow Diligence Services Private Limited",

                    CompanyAddress = "Plot No. 267, 2nd Floor, 2nd Main Road, Nehru Nagar, Kandanchavadi, Chennai 600096",

                    Phone = "9840352595",

                    PFNumber = "TNMAS1234568",

                    LogoFileName = "HDS_Logo_Transperant_-_Small.png",

                    LogoContentType = ".jpeg",

                    LogoFileSize = 356,

                    LogoCreatedAt = Convert.ToDateTime("11/11/2020"),

                    TermsOfService = true,

                    PrivacyPolicy = true

                });

            context.Departments.AddRange(
             new Department
             {
                 Id = 1,
                 DepartmentName = "Accounting",
                 IsActive = true,
                 CreatedAt = Convert.ToDateTime("11/11/2020")
             });

            context.Departments.AddRange(
            new Department
            {
                Id = 2,
                DepartmentName = "Finance",
                IsActive = true,
                CreatedAt = Convert.ToDateTime("11/11/2020")
            });
            context.Departments.AddRange(
                new Department
                {
                    Id = 3,
                    DepartmentName = "Development",
                    IsActive = true,
                    CreatedAt = Convert.ToDateTime("11/11/2020")
                });
            context.Departments.AddRange(
                new Department
                {
                    Id = 4,
                    DepartmentName = "Marketing",
                    IsActive = false,
                    CreatedAt = Convert.ToDateTime("11/11/2020")
                });

            context.Designations.AddRange(
                    new Designation
                    {
                        Id = 1,
                        DesignationName = "Chief Executive Officer",
                        IsActive = true,
                        LeaveApprovalLevel = 1,
                        CreatedAt = Convert.ToDateTime("11/11/2020")
                    });

            context.Designations.AddRange(
                new Designation
                {
                    Id = 2,
                    DesignationName = "Director",
                    IsActive = true,
                    LeaveApprovalLevel = 1,
                    CreatedAt = Convert.ToDateTime("11/11/2020")
                });

            context.Designations.AddRange(
            new Designation
            {
                Id = 3,
                DesignationName = "Manager",
                IsActive = true,
                LeaveApprovalLevel = 2,
                CreatedAt = Convert.ToDateTime("11/11/2020")
            });
            context.Designations.AddRange(
                    new Designation
                    {
                        Id = 4,
                        DesignationName = "Developer",
                        IsActive = false,
                        LeaveApprovalLevel = 2,
                        CreatedAt = Convert.ToDateTime("11/11/2020")
                    });

            context.WFHs.AddRange(
                    new WFH
                    {
                        Id = 1,
                        WFHName = "HDS_WFH",
                        IsUnlimited = true,
                        DaysPerMonth = null,
                        IsActive = true,
                        CreatedAt = Convert.ToDateTime("11/11/2020")
                    });
            context.WFHs.AddRange(
                   new WFH
                   {
                       Id = 2,
                       WFHName = "HDS_WFH_test",
                       IsUnlimited = false,
                       DaysPerMonth = 10,
                       IsActive = false,
                       CreatedAt = Convert.ToDateTime("11/11/2020")
                   });
            context.Shifts.AddRange(
                    new Shift
                    {
                        Id = 1,
                        ShiftName = "General Shift",
                        StartTimeInSeconds = 42300,
                        EndTimeInSeconds = 74700,
                        BufferTimeInSeconds = 32400,
                        IsActive = true,
                        CreatedAt = Convert.ToDateTime("11/11/2020")
                    });
            context.Shifts.AddRange(
                    new Shift
                    {
                        Id = 2,
                        ShiftName = "Night Shift",
                        StartTimeInSeconds = 42300,
                        EndTimeInSeconds = 74700,
                        BufferTimeInSeconds = 32400,
                        IsActive = false,
                        CreatedAt = Convert.ToDateTime("11/11/2020")
                    });

            context.LeaveTypes.AddRange(
                    new LeaveType
                    {
                        Id = 1,
                        LeaveTypeName = "Casual Leave",
                        ShortCode = "CL",
                        TotalDays = 12,
                        Gender = GenderType.Both,
                        MaritalStatus = MaritalStatusType.Both,
                        IsActive = true,
                        CreatedAt = Convert.ToDateTime("11/11/2020")
                    });
            context.LeaveTypes.AddRange(
                    new LeaveType
                    {
                        Id = 2,
                        LeaveTypeName = "Earned Leave",
                        ShortCode = "EL",
                        TotalDays = 10,
                        Gender = GenderType.Male,
                        MaritalStatus = MaritalStatusType.Married,
                        IsActive = true,
                        CreatedAt = Convert.ToDateTime("11/11/2020")
                    });
            context.LeaveTypes.AddRange(
                    new LeaveType
                    {
                        Id = 3,
                        LeaveTypeName = "Sick Leave",
                        ShortCode = "SL",
                        TotalDays = 12,
                        Gender = GenderType.Both,
                        MaritalStatus = MaritalStatusType.Both,
                        IsActive = true,
                        CreatedAt = Convert.ToDateTime("11/11/2020")
                    });
            context.LeaveTypes.AddRange(
                    new LeaveType
                    {
                        Id = 4,
                        LeaveTypeName = "Restricted Leave",
                        ShortCode = "RL",
                        TotalDays = 1,
                        Gender = GenderType.Both,
                        MaritalStatus = MaritalStatusType.Both,
                        IsActive = false,
                        CreatedAt = Convert.ToDateTime("11/11/2020")
                    });
            context.Holidays.AddRange(
                    new Holiday
                    {
                        Id = 1,
                        HolidayName = "Diwali",
                        HolidayDate = Convert.ToDateTime("13/11/2020"),
                        CreatedAt = Convert.ToDateTime("11/11/2020"),
                        UpdatedAt = Convert.ToDateTime("11/11/2020")
                    });
            context.Holidays.AddRange(
                    new Holiday
                    {
                        Id = 2,
                        HolidayName = "Diwali",
                        HolidayDate = Convert.ToDateTime("14/11/2020"),
                        CreatedAt = Convert.ToDateTime("11/11/2020"),
                        UpdatedAt = Convert.ToDateTime("11/11/2020")
                    });
            context.Holidays.AddRange(
                    new Holiday
                    {
                        Id = 3,
                        HolidayName = "Christmas",
                        HolidayDate = Convert.ToDateTime("25/12/2020"),
                        CreatedAt = Convert.ToDateTime("11/11/2020"),
                        UpdatedAt = Convert.ToDateTime("11/11/2020")
                    });

            context.SaveChanges();
    }
}
}