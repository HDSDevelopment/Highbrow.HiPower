using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Highbrow.HiPower.ViewModels.DesignationVM
{
    public class DesignationAddUpdateViewModel
    {
        public int Id {get; set;}

        //[Required(ErrorMessage = "Required")]
        public string DesignationName { get; set; }
        public List<SelectListItem> ApprovalLevels { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public int LeaveApprovalLevel { get; set; }

        public DesignationAddUpdateViewModel()
        {
            ApprovalLevels = new List<SelectListItem>
            {
                new SelectListItem{Text = "One", Value = "1"},
                new SelectListItem{Text = "Two", Value = "2"}
            };
        }

    }
}
