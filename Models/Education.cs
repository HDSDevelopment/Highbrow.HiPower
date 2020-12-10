namespace Highbrow.HiPower.Models
{
    public class Education
    {
        public int Id { get; set; }

        public string Degree { get; set; }

        public string Institute { get; set; }

        public string BoardOrUniversity { get; set; }

        public int YearOfPassing { get; set; }

        public float PercentageCGPA { get; set; }

        public long? EmployeeId { get; set; }
    }
}