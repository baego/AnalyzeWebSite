using AnalyzeWebSite.Models;
using AnalyzeWebSite.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SmartBreadcrumbs.Attributes;
using System;
using System.IO;

namespace AnalyzeWebSite.Controllers {
	public class TestController : Controller {

		[Breadcrumb("Тест")]
		public IActionResult Index() {

			var testService = new TestService();
			var degrs = testService.GetDegrees();
			var model = new TestModel();

			ViewBag.Degrees = degrs;

			return View(model);
		}

		[HttpPost]
		public void Submit(TestModel model) {

		}
	}
}
