using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyzeWebSite.Controllers {
	public class AboutYouController : Controller  {

		
		[Breadcrumb("О вас")]
		public IActionResult Index() {

			return View();
		}

		[HttpPost]
		public string Common(string yourName, string yourCity, string direction, int yourYear, int degreeSelect, string yourOpinion) {

			return "hello there";
		}

		[HttpPost]
		public string Tedsdsd(int average, int uiux, int loadTime, int usability, int trust, int errors) {

			return "hello there";
		}
	}
}
