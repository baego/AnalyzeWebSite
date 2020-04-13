using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyzeWebSite.Controllers {
	public class AboutYouController : Controller  {

		public IActionResult AboutYou() {

			return View();
		}
	}
}
