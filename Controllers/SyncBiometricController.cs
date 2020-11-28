using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Highbrow.HiPower.Controllers
{
	public class SyncBiometricController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
	}
}
