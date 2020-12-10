namespace Highbrow.HiPower.Models
{
    public class BankDetail
    {
        public int Id { get; set; }

        public string BankName { get; set; }

        public string AccountNumber { get; set; }

        public string IFSCCode { get; set; }

        public string Branch { get; set; }

        public long EmployeeId { get; set; }
    }
}