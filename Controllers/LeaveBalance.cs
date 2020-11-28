using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Highbrow.HiPower.Controllers
{
	public class LeaveBalanceController : Controller
	{
		public IActionResult Index()
		{
			return View("List");
		}
		public IActionResult List()
		{
			return View();
		}
		[HttpGet, ActionName("Add")]
		public IActionResult AddGet()
		{
			return View();
		}
		[HttpGet, ActionName("Update")]
		public IActionResult UpdateGet()
		{
			return View();
		}
		public IActionResult DeletePost()
		{
			return View();
		}
		public IActionResult BulkUpload()
		{
			return View();
		}
	}
}
