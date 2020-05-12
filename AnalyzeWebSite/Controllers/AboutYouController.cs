using AnalyzeWebSite.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace AnalyzeWebSite.Controllers {
	public class AboutYouController : Controller {

		private bool CheckSession() {

			var spyService = new SpyService();
			Guid sessionId = new Guid();

			if (HttpContext.Session.Keys.Contains("ipAddress")) {

				var ip = HttpContext.Session.GetString("ipAddress");
				if (!HttpContext.Session.Keys.Contains("sessionId")) {

					sessionId = Guid.Parse(HttpContext.Session.Id);
					HttpContext.Session.SetString("sessionId", HttpContext.Session.Id);
					spyService.CreateSession(ip, sessionId);
				}
			} else {

				return false;
			}

			return true;
		}

		[Breadcrumb("О вас")]
		public IActionResult Index() {

			return View();
		}

		[HttpPost]
		[Breadcrumb("Спасибо за участие", FromAction = "Index")]
		public IActionResult Common(string yourName, string yourCity, int yourYear, int direction, int degreeSelect) {

			if (CheckSession()) {

				var service = new TestService();

				var ip = HttpContext.Session.GetString("ipAddress");
				var sessionId = Guid.Parse(HttpContext.Session.GetString("sessionId"));
				service.WriteCommon(ip, sessionId, yourName, yourCity, yourYear, direction, degreeSelect);
			}
			return View();
		}

		[HttpPost]
		[Breadcrumb("Спасибо за участие", FromAction = "Index")]
		public IActionResult Functional(int average, int loadTime, int limitations, string oneThing, int somethingNew, int reccomend, int portrait, int trueData) {


			if (CheckSession()) {

				var service = new TestService();

				var ip = HttpContext.Session.GetString("ipAddress");
				var sessionId = Guid.Parse(HttpContext.Session.GetString("sessionId"));
				service.WriteFunctional(ip, sessionId, average, loadTime, limitations, oneThing, somethingNew,
					reccomend, portrait, trueData);

			}
			return View();
		}

		[HttpPost]
		[Breadcrumb("Спасибо за участие", FromAction = "Index")]
		public IActionResult Uiux(int averageUi, int correctWork, int trust, int modern, int pageLocations, int formPreference) {


			if (CheckSession()) {

				var service = new TestService();

				var ip = HttpContext.Session.GetString("ipAddress");
				var sessionId = Guid.Parse(HttpContext.Session.GetString("sessionId"));
				service.WriteUiux(ip, sessionId, averageUi, correctWork, trust, modern, pageLocations, formPreference);

			}
			return View();

		}

		[HttpPost]
		[Breadcrumb("Спасибо за участие", FromAction = "Index")]
		public IActionResult FreeForm(string freeText) {
			if (CheckSession()) {

				var service = new TestService();

				var ip = HttpContext.Session.GetString("ipAddress");
				var sessionId = Guid.Parse(HttpContext.Session.GetString("sessionId"));
				service.WriteFreeText(ip, sessionId, freeText);

			}
			return View();
		}
	}
}
