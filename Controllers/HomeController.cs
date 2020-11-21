using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using Highbrow.HiPower.Models;

namespace Highbrow.HiPower.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("/Home/Error/{statusCode}")]
        public IActionResult Error(int statusCode)
        {

            switch(statusCode)
            {
                case 404:
                return View("~/Views/Home/Error/404.cshtml");

                case 500:
                return View("~/Views/Home/Error/500.cshtml", statusCode);
            }

            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [Route("/Home/Error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            ViewBag.ExceptionPath = exceptionDetails.Path;
            ViewBag.ExceptionMessage = exceptionDetails.Error.Message;
            ViewBag.Stacktrace = exceptionDetails.Error.StackTrace;

            return View("Error");
        }
    }
}
