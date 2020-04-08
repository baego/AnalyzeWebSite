using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AnalyzeWebSite.Models;

namespace AnalyzeWebSite.Controllers {
	public class HomeController : Controller {

		public IActionResult Index() {
			return View();
		}

		public IActionResult History() {
			return View();
		}

		public IActionResult Test() {
			return View();
		}

		public IActionResult AboutYou() {
			return View();
		}
		public IActionResult Author() {
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error() {
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
