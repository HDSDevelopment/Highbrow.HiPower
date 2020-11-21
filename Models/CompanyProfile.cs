using System;
using System.ComponentModel.DataAnnotations;

namespace Highbrow.HiPower.Models
{
    public class CompanyProfile
    {
        public int Id { get; set; }
        
        //[Required(ErrorMessage = "Company name is required")]
        public string CompanyName { get; set; }

        //[MaxLength(150)]
        //[Required(ErrorMessage = "Required")]
        public string CompanyAddress { get; set; }
        
        //[Required]
        //[RegularExpression(@"\d{10,15}", ErrorMessage = "Only numbers")]
        public string Phone { get; set; }

        //[Required]
        public string PFNumber { get; set; }

        public string LogoFileName { get; set; }

        public string LogoContentType { get; set; }

        public long LogoFileSize { get; set; }

        public DateTime? LogoCreatedAt { get; set; }

        public DateTime? LogoUpdatedAt { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        public bool TermsOfService { get; set; }

        public bool PrivacyPolicy { get; set; }
    }
}