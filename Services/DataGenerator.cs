using System;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Highbrow.HiPower.Data;
using Highbrow.HiPower.Models;



namespace Highbrow.HiPower.Services
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            var context = new HiPowerContext(

                serviceProvider.GetRequiredService<DbContextOptions<HiPowerContext>>());

            CultureInfo culture = CultureInfo.InvariantCulture;
            string format = "dd/MM/yyyy";

            if (context.CompanyProfiles.Any())
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

                    LogoCreatedAt = DateTime.ParseExact("11/11/2020", format, culture),

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
                        IsActive = true,
                        CreatedAt = Convert.ToDateTime("11/11/2020")
                    });
            context.LeaveTypes.AddRange(
                    new LeaveType
                    {
                        Id = 5,
                        LeaveTypeName = "Loss of Pay",
                        ShortCode = "LOP",
                        TotalDays = 30,
                        Gender = GenderType.Both,
                        MaritalStatus = MaritalStatusType.Both,
                        IsActive = true,
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
            context.LeaveCategories.AddRange(
                        new LeaveCategory
                        {
                            Id = 1,
                            CategoryName = "Confirmed",
                            IsActive = true,
                            CreatedAt = Convert.ToDateTime("11/11/2020"),
                            UpdatedAt = Convert.ToDateTime("11/11/2020")
                        });

            context.LeaveCategories.AddRange(
               new LeaveCategory
               {
                   Id = 2,
                   CategoryName = "Probationary",
                   IsActive = true,
                   CreatedAt = Convert.ToDateTime("11/11/2020"),
                   UpdatedAt = Convert.ToDateTime("11/11/2020")
               });

            context.LeaveCategoryTypes.AddRange(
                new LeaveCategoryType
                {
                    LeaveCategoryId = 1,
                    LeaveTypeId = 1

                });

            context.LeaveCategoryTypes.AddRange(
                new LeaveCategoryType
                {
                    LeaveCategoryId = 1,
                    LeaveTypeId = 2

                });
            context.LeaveCategoryTypes.AddRange(
                new LeaveCategoryType
                {
                    LeaveCategoryId = 1,
                    LeaveTypeId = 3

                });
            context.LeaveCategoryTypes.AddRange(
                new LeaveCategoryType
                {
                    LeaveCategoryId = 1,
                    LeaveTypeId = 4

                });

            context.LeaveCategoryTypes.AddRange(
            new LeaveCategoryType
            {
                LeaveCategoryId = 2,
                LeaveTypeId = 1

            });

            context.LeaveCategoryTypes.AddRange(
                new LeaveCategoryType
                {
                    LeaveCategoryId = 2,
                    LeaveTypeId = 2

                });


            context.Employees.AddRange(
new Employee
{
    //General
    Id = 1,
    OfficeId = "E0001",
    Title = TitleType.Mr,
    EmployeeName = "Shivakumar",
    Gender = GenderType.Male,
    IsActive = true,
    FirstLevelSupervisorId = 3,

    FatherName = "Narayanan",
    DateOfBirth = DateTime.ParseExact("22/06/1990", "dd/MM/yyyy", CultureInfo.InvariantCulture),
    Age = 25,
    BloodGroup = BloodGroupType.B_Positive,
    MaritalStatus = MaritalStatusType.Single,
    Religion = ReligionType.Hindu,
    PermanentAddress = "34, DEF Street, EFG Nagar, Trichy, Tamilnadu",
    CommunicationAddress = "12, ABC street, DEF Nagar, Chennai, Tamilnadu",
    ContactNumber = "7777777777",
    EmergencyNumber = "9999999999",
    PersonalMail = "def@ghi.com",
    Nationality = NationalityType.Indian,


    //HR
    DepartmentId = 1,
    DesignationId = 1,
    ShiftId = 1,
    LeaveCategoryId = 1,
    Location = "India",
    JoiningDate = DateTime.ParseExact("02/01/2019", format, culture),
    ConfirmationDate = DateTime.ParseExact("02/07/2020", format, culture),
    NoticePeriodInDays = 60,
    IsEligibleForWFH = true,
    IsMedicalLOPEligible = true,
    //Dependents = null,

	Dependents = new List<Dependent>{
		new Dependent{
			Id = 1,
			DependentName = "Seetha",
			Relation = RelationType.Spouse,
			DateOfBirth = DateTime.ParseExact("23/07/1992", format, culture),
		EmployeeId = 1},
	new Dependent{
			Id = 4,
			DependentName = "Reetha",
			Relation = RelationType.Daughter,
			DateOfBirth = DateTime.ParseExact("23/07/1992", format, culture),
		EmployeeId = 1},
	new Dependent{
			Id = 5,
			DependentName = "Leetha",
			Relation = RelationType.Daughter,
			DateOfBirth = DateTime.ParseExact("23/07/1992", format, culture),
		EmployeeId = 1}
	},

	Educations = new List<Education>{
                            new Education{
                                Id = 1,
                                Degree = "BE Computer Science",
                                Institute = "PSG Institute of Technology",
                                BoardOrUniversity = "Coimbatore University",
                                YearOfPassing = 2007,
                                PercentageCGPA = 7.7F,
                                EmployeeId = 1}},

    Experiences = new List<Experience>{
                            new Experience{
                                Id = 1,
                                CompanyName = "Hexaware Technologies Private Limited",
                                Designation = "Junior Software Engineer",
                                FromDate = DateTime.ParseExact("28/10/2008", format, culture),
                                ToDate = DateTime.ParseExact("15/12/2009", format, culture),
                                Duration = "1 year 2 months",
                                EmployeeId = 1}},

    BankDetail = new BankDetail
    {
        Id = 1,
        BankName = "Corporation Bank",
        AccountNumber = "111111111111",
        IFSCCode = "CORP0000504",
        Branch = "Whites Road",
        EmployeeId = 1
    },

    UAN = "MH11576494191",

    ESINumber = "31001234560000001",

    CreatedAt = DateTime.ParseExact("10/01/2019", format, culture),

    UpdatedAt = DateTime.ParseExact("10/01/2019", format, culture),

    PhotoFileName = "default-profile.jpg",

    PhotoFileSize = 8000,

    PhotoUpdatedAt = DateTime.ParseExact("10/01/2019", format, culture),

    IsExit = false,
});

            context.Employees.AddRange(
new Employee
{
    //General
    Id = 2,
    OfficeId = "E0002",
    Title = TitleType.Mr,
    EmployeeName = "Test1",
    Gender = GenderType.Male,
    IsActive = true,
    FirstLevelSupervisorId = 3,

    FatherName = "Narayanan",
    DateOfBirth = DateTime.ParseExact("22/06/1990", "dd/MM/yyyy", CultureInfo.InvariantCulture),
    Age = 25,
    BloodGroup = BloodGroupType.B_Positive,
    MaritalStatus = MaritalStatusType.Single,
    Religion = ReligionType.Hindu,
    PermanentAddress = "34, DEF Street, EFG Nagar, Trichy, Tamilnadu",
    CommunicationAddress = "12, ABC street, DEF Nagar, Chennai, Tamilnadu",
    ContactNumber = "7777777777",
    EmergencyNumber = "9999999999",
    PersonalMail = "test@gmail.com",
    Nationality = NationalityType.Indian,


    //HR
    DepartmentId = 1,
    DesignationId = 1,
    ShiftId = 1,
    LeaveCategoryId = 1,
    Location = "India",
    JoiningDate = DateTime.ParseExact("02/01/2019", format, culture),
    ConfirmationDate = DateTime.ParseExact("02/07/2020", format, culture),
    NoticePeriodInDays = 60,
    IsEligibleForWFH = true,
    IsMedicalLOPEligible = true,

    Dependents = new List<Dependent>{
                            new Dependent{
                                Id = 2,
                                DependentName = "Geetha",
                                Relation = RelationType.Spouse,
                                DateOfBirth = DateTime.ParseExact("23/07/1992", format, culture),
                            EmployeeId = 2}},

    Educations = new List<Education>{
                            new Education{
                                Id = 2,
                                Degree = "BE Computer Science",
                                Institute = "PSG Institute of Technology",
                                BoardOrUniversity = "Coimbatore University",
                                YearOfPassing = 2007,
                                PercentageCGPA = 7.7F,
                                EmployeeId = 2}},

    Experiences = new List<Experience>{
                            new Experience{
                                Id = 2,
                                CompanyName = "Hexaware Technologies Private Limited",
                                Designation = "Junior Software Engineer",
                                FromDate = DateTime.ParseExact("28/10/2008", format, culture),
                                ToDate = DateTime.ParseExact("15/12/2009", format, culture),
                                Duration = "1 year 2 months",
                                EmployeeId = 2}},

    BankDetail = new BankDetail
    {
        Id = 2,
        BankName = "Corporation Bank",
        AccountNumber = "111111111111",
        IFSCCode = "CORP0000504",
        Branch = "Whites Road",
        EmployeeId = 2
    },

    UAN = "MH11576494191",

    ESINumber = "31001234560000001",

    CreatedAt = DateTime.ParseExact("10/01/2019", format, culture),

    UpdatedAt = DateTime.ParseExact("10/01/2019", format, culture),

    PhotoFileName = "default-profile.jpg",

    PhotoFileSize = 8000,

    PhotoUpdatedAt = DateTime.ParseExact("10/01/2019", format, culture),

    IsExit = false,
});

            context.Employees.AddRange(
new Employee
{
    //General
    Id = 3,
    OfficeId = "E0003",
    Title = TitleType.Mr,
    EmployeeName = "Test2",
    Gender = GenderType.Male,
    IsActive = false,

    FatherName = "Narayanan",
    DateOfBirth = DateTime.ParseExact("22/06/1990", "dd/MM/yyyy", CultureInfo.InvariantCulture),
    Age = 25,
    BloodGroup = BloodGroupType.B_Positive,
    MaritalStatus = MaritalStatusType.Single,
    Religion = ReligionType.Hindu,
    PermanentAddress = "34, DEF Street, EFG Nagar, Trichy, Tamilnadu",
    CommunicationAddress = "12, ABC street, DEF Nagar, Chennai, Tamilnadu",
    ContactNumber = "7777777777",
    EmergencyNumber = "9999999999",
    PersonalMail = "test2@gmail.com",
    Nationality = NationalityType.Indian,


    //HR
    DepartmentId = 1,
    DesignationId = 1,
    ShiftId = 1,
    LeaveCategoryId = 1,
    Location = "India",
    JoiningDate = DateTime.ParseExact("02/01/2019", format, culture),
    ConfirmationDate = DateTime.ParseExact("02/07/2020", format, culture),
    NoticePeriodInDays = 60,
    IsEligibleForWFH = true,
    IsMedicalLOPEligible = true,

    Dependents = new List<Dependent>{
                            new Dependent{
                                Id = 3,
                                DependentName = "Reetha",
                                Relation = RelationType.Spouse,
                                DateOfBirth = DateTime.ParseExact("23/07/1992", format, culture),
                            EmployeeId = 3}},

    Educations = new List<Education>{
                            new Education{
                                Id = 3,
                                Degree = "BE Computer Science",
                                Institute = "PSG Institute of Technology",
                                BoardOrUniversity = "Coimbatore University",
                                YearOfPassing = 2007,
                                PercentageCGPA = 7.7F,
                                EmployeeId = 3}},

    Experiences = new List<Experience>{
                            new Experience{
                                Id = 3,
                                CompanyName = "Hexaware Technologies Private Limited",
                                Designation = "Junior Software Engineer",
                                FromDate = DateTime.ParseExact("28/10/2008", format, culture),
                                ToDate = DateTime.ParseExact("15/12/2009", format, culture),
                                Duration = "1 year 2 months",
                                EmployeeId = 3}},

    BankDetail = new BankDetail
    {
        Id = 3,
        BankName = "Corporation Bank",
        AccountNumber = "111111111111",
        IFSCCode = "CORP0000504",
        Branch = "Whites Road",
        EmployeeId = 3
    },

    UAN = "MH11576494191",

    ESINumber = "31001234560000001",

    CreatedAt = DateTime.ParseExact("10/01/2019", format, culture),

    UpdatedAt = DateTime.ParseExact("10/01/2019", format, culture),

    PhotoFileName = "default-profile.jpg",

    PhotoFileSize = 8000,

    PhotoUpdatedAt = DateTime.ParseExact("10/01/2019", format, culture),

    IsExit = false,
});

            context.SaveChanges();
        }
    }
}