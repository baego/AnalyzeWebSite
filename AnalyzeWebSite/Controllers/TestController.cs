using AnalyzeWebSite.Models;
using AnalyzeWebSite.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using SmartBreadcrumbs.Attributes;
using System;
using System.IO;
using System.Linq;

namespace AnalyzeWebSite.Controllers {
	public class TestController : Controller {

		[Breadcrumb("Тест")]
		public IActionResult Index() {

			var testService = new TestService();
			var degrs = testService.GetDegrees();
			var sciences = testService.GetSciences();
			var model = new TestModel();

			ViewBag.Degrees = degrs;
			ViewBag.Sciences = sciences;

			return View(model);
		}

		[HttpPost]
		public JsonResult GetInnerRoute(int degreeRoute) {

			var testService = new TestService();
			var innerRoutes = testService.GetInnerRoutes(degreeRoute);

			return new JsonResult(innerRoutes);
		}


		[HttpPost]
		[Breadcrumb("Результаты теста", FromAction = "Index")]
		public IActionResult Result(int degreeSelect, int degreeRouteSelect, string organizationText, int innerRouteSelect) {

			string ip = "non defined ip";
			Guid sessionId = new Guid();
			var testService = new TestService();
			var spyService = new SpyService();

			if (HttpContext.Session.Keys.Contains("ipAddress")) {

				ip = HttpContext.Session.GetString("ipAddress");
				if (!HttpContext.Session.Keys.Contains("sessionId")) {

					HttpContext.Session.SetString("sessionId", HttpContext.Session.Id);
					spyService.CreateSession(ip, sessionId);
				} else {
					sessionId = Guid.Parse(HttpContext.Session.Id);

				}
			}

			ViewBag.Results = testService.TestResults(ip, sessionId, degreeSelect, degreeRouteSelect, organizationText, innerRouteSelect);
			return View();
		}
	}
}
