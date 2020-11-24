using System;
using System.ComponentModel.DataAnnotations;

namespace Highbrow.HiPower.ViewModels.ShiftVM
{
    public class ShiftDetailsViewModel
    {
        public int Id {get; set;}

        [Required(ErrorMessage = "Required")]
        public string ShiftName { get; set; }

        [Range(0, 23)]
        public int StartHour { get; set; }

        [Range(0, 59)]
        public int StartMinute { get; set; }

        [Range(0, 23)]
        public int EndHour { get; set; }
        
        [Range(0, 59)]
        public int EndMinute { get; set; }

        public int BufferHour { get; set; }

        public int BufferMinute { get; set; }
        
        public bool IsActive { get; set; }

        public void SetStartTime(double startTimeInSeconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(startTimeInSeconds);
            StartHour = time.Hours;
            StartMinute = time.Minutes;
        }

        public void SetEndTime(double endTimeInSeconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(endTimeInSeconds);
            EndHour = time.Hours;
            EndMinute = time.Minutes;
        }

        public void SetBufferTime(double bufferTimeInSeconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(bufferTimeInSeconds);
            BufferHour = time.Hours;
            BufferMinute = time.Minutes;
        }
    }
}