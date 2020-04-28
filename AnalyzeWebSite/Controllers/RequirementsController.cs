using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Nodes;
using SmartBreadcrumbs.Extensions;
using SmartBreadcrumbs.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyzeWebSite.Controllers {
	public class RequirementsController : Controller {

		[Breadcrumb("Требования")]
		public IActionResult Index() {
			return View();
		}

		[Breadcrumb("Оформление", FromAction = "Index")]
		public IActionResult FormalizationRequirements() {
			return View();
		}

		[Breadcrumb("Содержание", FromAction = "Index")]
		public IActionResult ContentRequirements() {
			return View();
		}

		[Breadcrumb("Полезные ссылки", FromAction = "Index")]
		public IActionResult UsefulLinks() {
			return View();
		}
	}
}
