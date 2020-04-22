using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyzeWebSite.Controllers {
	public class HistoryController : Controller {
		[Breadcrumb("История")]
		public IActionResult Index() {
			return View();
		}

		[Breadcrumb("В мире", FromAction = "Index")]
		public IActionResult BolognaWorld() {
			return View();
		}

		[Breadcrumb("В России", FromAction = "Index")]
		public IActionResult BolognaRussia() {
			return View();
		}

		[Breadcrumb("Защита диссертации", FromAction = "Index")]
		public IActionResult Dissertation() {
			return View();
		}
	}
}
