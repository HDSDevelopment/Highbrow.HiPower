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

            context.SaveChanges();
    }
}

}