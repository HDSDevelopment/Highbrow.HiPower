using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Highbrow.HiPower.Controllers
{
	public class AuthenticationController : Controller
	{
		public IActionResult Signin()
		{
			return View();
		}
		public IActionResult Signup()
		{
			return View();
		}
		public IActionResult ForgotPassword()
		{
			return View();
		}
	}
}
