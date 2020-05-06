using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AnalyzeWebSite.Models;
using SmartBreadcrumbs.Attributes;
using AnalyzeWebSite.Data;
using AnalyzeWebSite.Services;
using Microsoft.AspNetCore.Http;
//using AnalyzeWebSite.Data.Entities;

namespace AnalyzeWebSite.Controllers {
	public class HomeController : Controller {

		

		[DefaultBreadcrumb("Главная")]
		public IActionResult Index() {

			return View();
		}

		[Breadcrumb("Куки", FromAction = "Index")]
		public IActionResult Cookies() {

			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error() {
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
