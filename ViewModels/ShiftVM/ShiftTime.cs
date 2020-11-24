using System;

namespace Highbrow.HiPower.ViewModels.ShiftVM
{
    public class ShiftTime
    {
        public int HourPart { get; set; }

        public int MinutePart { get; set; }

        public ShiftTime(int hourPart, int minutePart)
        {
            HourPart = hourPart;
            MinutePart = minutePart;
        }

        public double GetInSeconds()
        {
            TimeSpan time = new TimeSpan(HourPart, MinutePart, 0);
            return time.TotalSeconds;
        }
    }
}