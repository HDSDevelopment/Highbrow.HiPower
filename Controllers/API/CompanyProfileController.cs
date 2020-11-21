using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Highbrow.HiPower.Models;
using Highbrow.HiPower.Data;
using Microsoft.EntityFrameworkCore;

namespace Highbrow.HiPower.Controllers.API.Settings
{
    [ApiController]    
    public class CompanyProfileController : ControllerBase
    {
        private HiPowerContext _context;
        
        public CompanyProfileController(HiPowerContext context)
        {
        _context = context;
        }       

        [Route("api/CompanyProfile/get")]
        [HttpGet]
        public CompanyProfile Get()
        {
            return _context.CompanyProfiles.AsNoTracking().FirstOrDefault();
        }
    }
}
