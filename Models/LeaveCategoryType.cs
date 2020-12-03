using Highbrow.HiPower.Models;

namespace Highbrow.HiPower.Models
{
    public class LeaveCategoryType
    {
        public int Id { get; set; }

        public int LeaveTypeId { get; set; }

        public LeaveType LeaveType { get; set; }

        public int LeaveCategoryId { get; set; }

        public LeaveCategory LeaveCategory { get; set; }
    }
}