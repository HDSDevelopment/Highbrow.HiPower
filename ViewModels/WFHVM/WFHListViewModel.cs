using System.Collections.Generic;
using Highbrow.HiPower.Models;

namespace Highbrow.HiPower.ViewModels.WFHVM
{
    public class WFHListViewModel
    {
        public List<WFH> ActiveWFHs {get; set;}

        public List<WFH> InactiveWFHs { get; set; }

        public int ActiveCount {get; set;}

        public int InactiveCount { get; set; }
    }
}