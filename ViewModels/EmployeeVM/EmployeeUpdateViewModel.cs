using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Highbrow.HiPower.Models;
using Highbrow.HiPower.Utilities;

namespace Highbrow.HiPower.ViewModels.EmployeeVM
{
    public enum EmployeeTabs
    {
        General = 1, Personal = 2, HR = 3, Dependent = 4,
        Education = 5, Experience = 6, Bank = 7, Social = 8,
        ProfilePicture = 9
    }

    public class EmployeeUpdateViewModel
    {
        //General
        public long Id { get; set; }

        [Required]
        public string OfficeId { get; set; }

        public string CardRFId { get; set; }

        public TitleType? Title { get; set; }

        [Required]
        public string EmployeeName { get; set; }

        //[Required]
        public GenderType Gender { get; set; }

        [EmailAddress]
        //[Required]
        public string OfficeEmail { get; set; }

        /*         [DataType(DataType.Password)]
                public string Password { get; set; }

                [DataType(DataType.Password)]
                [Compare("Password")]
                public string ConfirmPassword { get; set; }

                */

        public List<int> Roles { get; set; }
        public List<SelectListItem> RolesSelectList { get; set; }

        public bool IsActive { get; set; }


        //Personal
        public string FatherName { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public int? Age { get; set; }

        public BloodGroupType? BloodGroup { get; set; }

        public MaritalStatusType? MaritalStatus { get; set; }

        public ReligionType? Religion { get; set; }

        public string PermanentAddress { get; set; }

        public string CommunicationAddress { get; set; }

        public string ContactNumber { get; set; }

        public string EmergencyNumber { get; set; }

        public string PersonalMail { get; set; }

        public NationalityType Nationality { get; set; }


        //HR
        public int? DepartmentId { get; set; }

        public List<SelectListItem> DepartmentsSelectList { get; set; }

        public int? DesignationId { get; set; }

        public int? PreviousDesignationId { get; set; }

        public List<SelectListItem> DesignationsSelectList { get; set; }

        public int? ShiftId { get; set; }

        public List<SelectListItem> ShiftsSelectList { get; set; }

        public int LeaveCategoryId { get; set; }

        public List<SelectListItem> EmploymentStatusSelectList { get; set; }

        public string Location { get; set; }
        public List<SelectListItem> LocationSelectList { get; set; }

        public DateTime JoiningDate { get; set; }

        public DateTime ConfirmationDate { get; set; }

        public int NoticePeriodInDays { get; set; }

        public long? FirstLevelSupervisorId { get; set; }

        public long? SecondLevelSupervisorId { get; set; }
        public List<SelectListItem> SupervisorsSelectList { get; set; }

        public bool IsEligibleForWFH { get; set; }

        public bool IsMedicalLOPEligible { get; set; }


        //Dependent
        public List<Dependent> Dependents { get; set; }


        //Education
        public List<Education> Educations { get; set; }


        //Experience
        public List<Experience> Experiences { get; set; }


        //Bank
        public BankDetail BankDetail { get; set; }


        //Social / Official
        public string AadharNumber { get; set; }

        public string PANNumber { get; set; }

        public string UAN { get; set; }

        public string ESINumber { get; set; }

        public string PassportNumber { get; set; }

        public DateTime PassportExpiry { get; set; }

        public string VisaNumber { get; set; }

        public string VisaType { get; set; }

        public DateTime VisaExpiry { get; set; }

        public string SkypeId { get; set; }

        public string FacebookId { get; set; }

        public string LinkedinId { get; set; }

        public IFormFile PhotoFile { get; set; }

        public EmployeeTabs CurrentTab { get; set; }

        public EmployeeUpdateViewModel()
        {
            LocationSelectList = new List<SelectListItem>{
                                    new SelectListItem{Text = "India",
                                                        Value = "India"}};
            DepartmentsSelectList = new List<SelectListItem>();
            SupervisorsSelectList = new List<SelectListItem>();
            Dependents = new List<Dependent>();
            Educations = new List<Education>();
            Experiences = new List<Experience>();
        }
        public Employee GetEmployee()
        {
            Employee employee = new Employee();

            employee.Id = this.Id;
            
            employee.OfficeId = StringUtility.GetTrimmedUpperCase(OfficeId);

            employee.CardRFId = StringUtility.GetTrimmed(CardRFId);

            employee.Title = Title;

            employee.EmployeeName = StringUtility.GetTrimmed(EmployeeName);

            employee.Gender = Gender;

            employee.IsActive = IsActive;

            employee.FatherName = FatherName;

            employee.DateOfBirth = DateOfBirth;

            employee.Age = Age;

            employee.BloodGroup = BloodGroup;

            employee.MaritalStatus = MaritalStatus;

            employee.Religion = Religion;

            employee.PermanentAddress = PermanentAddress;

            employee.CommunicationAddress = CommunicationAddress;

            employee.ContactNumber = ContactNumber;

            employee.EmergencyNumber = EmergencyNumber;

            employee.PersonalMail = PersonalMail;

            employee.Nationality = Nationality;

            employee.DepartmentId = DepartmentId;

            employee.DesignationId = DesignationId;

            employee.PreviousDesignationId = PreviousDesignationId;

            employee.ShiftId = ShiftId;

            employee.LeaveCategoryId = LeaveCategoryId;

            employee.Location = Location;

            employee.JoiningDate = JoiningDate;

            employee.NoticePeriodInDays = NoticePeriodInDays;

            employee.FirstLevelSupervisorId = this.FirstLevelSupervisorId;

            employee.SecondLevelSupervisorId = this.SecondLevelSupervisorId;

            employee.IsEligibleForWFH = IsEligibleForWFH;

            employee.IsMedicalLOPEligible = IsMedicalLOPEligible;

            employee.Dependents = Dependents;

            employee.Educations = Educations;

            employee.Experiences = Experiences;

            employee.BankDetail = BankDetail;

            employee.AadharNumber = AadharNumber;

            employee.PANNumber = PANNumber;

            employee.UAN = UAN;

            employee.ESINumber = ESINumber;

            employee.PassportNumber = PassportNumber;

            employee.PassportExpiry = PassportExpiry;

            employee.VisaNumber = VisaNumber;

            employee.VisaType = VisaType;

            employee.VisaExpiry = VisaExpiry;

            employee.SkypeId = SkypeId;

            employee.FacebookId = FacebookId;

            employee.LinkedinId = LinkedinId;

            return employee;
        }

        public void SetViewModel(Employee employee)
        {
            Id = employee.Id;

            OfficeId = employee.OfficeId;

            CardRFId = employee.CardRFId;

            Title = employee.Title;

            EmployeeName = employee.EmployeeName;

            Gender = employee.Gender;

            IsActive = (bool)employee.IsActive;

            FatherName = employee.FatherName;

            DateOfBirth = employee.DateOfBirth;

            Age = employee.Age;

            BloodGroup = employee.BloodGroup;

            MaritalStatus = employee.MaritalStatus;

            Religion = employee.Religion;

            PermanentAddress = employee.PermanentAddress;

            CommunicationAddress = employee.CommunicationAddress;

            ContactNumber = employee.ContactNumber;

            EmergencyNumber = employee.EmergencyNumber;

            PersonalMail = employee.PersonalMail;

            Nationality = employee.Nationality;

            DepartmentId = employee.DepartmentId;

            DesignationId = employee.DesignationId;

            PreviousDesignationId = employee.PreviousDesignationId;

            ShiftId = employee.ShiftId;

            LeaveCategoryId = employee.LeaveCategoryId;

            Location = employee.Location;

            JoiningDate = employee.JoiningDate;

            NoticePeriodInDays = employee.NoticePeriodInDays;

            FirstLevelSupervisorId = employee.FirstLevelSupervisorId;

            SecondLevelSupervisorId = employee.SecondLevelSupervisorId;

            IsEligibleForWFH = (bool)employee.IsEligibleForWFH;

            IsMedicalLOPEligible = (bool)employee.IsMedicalLOPEligible;

            Dependents = employee.Dependents;

            Educations = employee.Educations;

            Experiences = employee.Experiences;

            BankDetail = employee.BankDetail;

            AadharNumber = employee.AadharNumber;

            PANNumber = employee.PANNumber;

            UAN = employee.UAN;

            ESINumber = employee.ESINumber;

            PassportNumber = employee.PassportNumber;

            PassportExpiry = employee.PassportExpiry;

            VisaNumber = employee.VisaNumber;

            VisaType = employee.VisaType;

            VisaExpiry = employee.VisaExpiry;

            SkypeId = employee.SkypeId;

            FacebookId = employee.FacebookId;

            LinkedinId = employee.LinkedinId;
        }
    }
}