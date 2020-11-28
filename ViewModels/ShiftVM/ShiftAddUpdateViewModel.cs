using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Highbrow.HiPower.ViewModels.ShiftVM
{
    public class ShiftAddUpdateViewModel
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

        public DateTime CreatedAt { get; set; }

        public double GetStartTimeInSeconds()
        {
            ShiftTime time = new ShiftTime(StartHour, StartMinute);
            return time.GetInSeconds();
        }

        public double GetEndTimeInSeconds()
        {
            ShiftTime time = new ShiftTime(EndHour, EndMinute);
            return time.GetInSeconds();
        }

        public double GetBufferTimeInSeconds()
        {
            ShiftTime time = new ShiftTime(BufferHour, BufferMinute);
            return time.GetInSeconds();
        }

        //public TimeSpan SetStartTime(double startTimeInSeconds)
        //{
        //    TimeSpan time = TimeSpan.FromSeconds(startTimeInSeconds);
        //    StartHour = time.Hours;
        //    StartMinute = time.Minutes;
        //    return time;
        //}
        public void SetStartTime(double startTimeInSeconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(startTimeInSeconds);
            StartHour = time.Hours;
            StartMinute = time.Minutes;
            //return time;
        }

        public void SetEndTime(double endTimeInSeconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(endTimeInSeconds);
            EndHour = time.Hours;
            EndMinute = time.Minutes;
            //return time;
        }

        public void SetBufferTime(double bufferTimeInSeconds)
        {
            TimeSpan time = TimeSpan.FromSeconds(bufferTimeInSeconds);
            BufferHour = time.Hours;
            BufferMinute = time.Minutes;
            //return time;
        }

        public List<SelectListItem> TimeHourSelectItem { get; set; }
        public List<SelectListItem> TimeMinuteSelectItem { get; set; }
        
        public ShiftAddUpdateViewModel()
        {
            TimeHourSelectItem = new List<SelectListItem>
            {
                new SelectListItem{Text = "Hour", Value = ""},
                new SelectListItem{Text = "00", Value = "0"},
                new SelectListItem{Text = "01", Value = "1"},
                new SelectListItem{Text = "02", Value = "2"},
                new SelectListItem{Text = "03", Value = "3"},
                new SelectListItem{Text = "04", Value = "4"},
                new SelectListItem{Text = "05", Value = "5"},
                new SelectListItem{Text = "06", Value = "6"},
                new SelectListItem{Text = "07", Value = "7"},
                new SelectListItem{Text = "08", Value = "8"},
                new SelectListItem{Text = "09", Value = "9"},
                new SelectListItem{Text = "10", Value = "10"},
                new SelectListItem{Text = "11", Value = "11"},
                new SelectListItem{Text = "12", Value = "12"},
                new SelectListItem{Text = "13", Value = "13"},
                new SelectListItem{Text = "14", Value = "14"},
                new SelectListItem{Text = "15", Value = "15"},
                new SelectListItem{Text = "16", Value = "16"},
                new SelectListItem{Text = "17", Value = "17"},
                new SelectListItem{Text = "18", Value = "18"},
                new SelectListItem{Text = "19", Value = "19"},
                new SelectListItem{Text = "20", Value = "20"},
                new SelectListItem{Text = "21", Value = "21"},
                new SelectListItem{Text = "22", Value = "22"},
                new SelectListItem{Text = "23", Value = "23"}
            };
            TimeMinuteSelectItem = new List<SelectListItem>
            {
                new SelectListItem{Text = "Minute", Value = ""},
                new SelectListItem{Text = "00", Value = "0"},
                new SelectListItem{Text = "01", Value = "1"},
                new SelectListItem{Text = "02", Value = "2"},
                new SelectListItem{Text = "03", Value = "3"},
                new SelectListItem{Text = "04", Value = "4"},
                new SelectListItem{Text = "05", Value = "5"},
                new SelectListItem{Text = "06", Value = "6"},
                new SelectListItem{Text = "07", Value = "7"},
                new SelectListItem{Text = "08", Value = "8"},
                new SelectListItem{Text = "09", Value = "9"},
                new SelectListItem{Text = "10", Value = "10"},
                new SelectListItem{Text = "11", Value = "11"},
                new SelectListItem{Text = "12", Value = "12"},
                new SelectListItem{Text = "13", Value = "13"},
                new SelectListItem{Text = "14", Value = "14"},
                new SelectListItem{Text = "15", Value = "15"},
                new SelectListItem{Text = "16", Value = "16"},
                new SelectListItem{Text = "17", Value = "17"},
                new SelectListItem{Text = "18", Value = "18"},
                new SelectListItem{Text = "19", Value = "19"},
                new SelectListItem{Text = "20", Value = "20"},
                new SelectListItem{Text = "21", Value = "21"},
                new SelectListItem{Text = "22", Value = "22"},
                new SelectListItem{Text = "23", Value = "23"},
                new SelectListItem{Text = "24", Value = "24"},
                new SelectListItem{Text = "25", Value = "25"},
                new SelectListItem{Text = "26", Value = "26"},
                new SelectListItem{Text = "27", Value = "27"},
                new SelectListItem{Text = "28", Value = "28"},
                new SelectListItem{Text = "29", Value = "29"},
                new SelectListItem{Text = "30", Value = "30"},
                new SelectListItem{Text = "31", Value = "31"},
                new SelectListItem{Text = "32", Value = "32"},
                new SelectListItem{Text = "33", Value = "33"},
                new SelectListItem{Text = "34", Value = "34"},
                new SelectListItem{Text = "35", Value = "35"},
                new SelectListItem{Text = "36", Value = "36"},
                new SelectListItem{Text = "37", Value = "37"},
                new SelectListItem{Text = "38", Value = "38"},
                new SelectListItem{Text = "39", Value = "39"},
                new SelectListItem{Text = "40", Value = "40"},
                new SelectListItem{Text = "41", Value = "41"},
                new SelectListItem{Text = "42", Value = "42"},
                new SelectListItem{Text = "43", Value = "43"},
                new SelectListItem{Text = "44", Value = "44"},
                new SelectListItem{Text = "45", Value = "45"},
                new SelectListItem{Text = "46", Value = "46"},
                new SelectListItem{Text = "47", Value = "47"},
                new SelectListItem{Text = "48", Value = "48"},
                new SelectListItem{Text = "49", Value = "49"},
                new SelectListItem{Text = "50", Value = "50"},
                new SelectListItem{Text = "51", Value = "51"},
                new SelectListItem{Text = "52", Value = "52"},
                new SelectListItem{Text = "53", Value = "53"},
                new SelectListItem{Text = "54", Value = "54"},
                new SelectListItem{Text = "55", Value = "55"},
                new SelectListItem{Text = "56", Value = "56"},
                new SelectListItem{Text = "57", Value = "57"},
                new SelectListItem{Text = "58", Value = "58"},
                new SelectListItem{Text = "59", Value = "59"},
            };
        }
    }
}
