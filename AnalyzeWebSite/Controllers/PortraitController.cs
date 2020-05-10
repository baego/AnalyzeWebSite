using AnalyzeWebSite.Models;
using AnalyzeWebSite.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyzeWebSite.Controllers {

	public class PortraitController : Controller {
		
		[Breadcrumb("Портрет")]
		public IActionResult Index() {

			string ip = "";
			var model = new PortraitModel();

			//если нет ip, то ничего еще не готово
			if (HttpContext.Session.Keys.Contains("ipAddress")) {

				ip = HttpContext.Session.GetString("ipAddress");
				if (HttpContext.Session.Keys.Contains("sessionId")) {

					var sessionId = HttpContext.Session.GetString("sessionId");
					var portraitService = new PortraitService();

					model = portraitService.GetPortrait(ip, Guid.Parse(sessionId), model);
					model.IsModelReady = true;
				} else {

					model.IsModelReady = false;
				}
			} else {

				model.IsModelReady = false;
			}

			return View(model);
		}
	}
}
