using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyzeWebSite.Controllers {
	public class TestController : Controller {

		[Breadcrumb("Тест")]
		public IActionResult Index() {
			return View();
		}
	}
}
