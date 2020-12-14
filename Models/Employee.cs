using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Highbrow.HiPower.Models;

namespace Highbrow.HiPower.Models
{
    public class Employee
    {
        //General
        public long Id { get; set; }

        public string OfficeId { get; set; }

        public string CardRFId { get; set; }

        public TitleType? Title { get; set; }

        [Required]
        public string EmployeeName { get; set; }

        public GenderType Gender { get; set; }

        public bool? IsActive { get; set; }

        public int? NoticePeriod { get; set; }

        public long? FirstLevelSupervisorId { get; set; }
        public Employee FirstLevelSupervisor { get; set; }

        public long? SecondLevelSupervisorId { get; set; }
        public Employee SecondLevelSupervisor { get; set; }

        public List<Employee> FirstLevelSupervised { get; set; }
        public List<Employee> SecondLevelSupervised { get; set; }

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
        public Department Department { get; set; }

        public int? DesignationId { get; set; }
        public Designation Designation { get; set; }

        public int? PreviousDesignationId { get; set; }
        public Designation PreviousDesignation { get; set; }

        public int? ShiftId { get; set; }

        public Shift Shift { get; set; }

        public int LeaveCategoryId { get; set; }
        public LeaveCategory LeaveCategory { get; set; }

        public string Location { get; set; }

        public DateTime JoiningDate { get; set; }

        public DateTime ConfirmationDate { get; set; }

        public int NoticePeriodInDays { get; set; }

        //First and second Manager

        public bool? IsEligibleForWFH { get; set; }

        public bool? IsMedicalLOPEligible { get; set; }

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


        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string PhotoFileName { get; set; }

        public string PhotoContentType { get; set; }

        public int? PhotoFileSize { get; set; }

        public DateTime? PhotoUpdatedAt { get; set; }

        public DateTime? RelievingDate { get; set; }

        public bool IsExit { get; set; }

        public string ProfileSummary { get; set; }

        public string RelievingNotes { get; set; }

        public int? WFHId { get; set; }

        public WFH WFH { get; set; }
    }
}