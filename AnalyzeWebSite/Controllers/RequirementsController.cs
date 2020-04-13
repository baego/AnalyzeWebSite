using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyzeWebSite.Controllers {
	public class RequirementsController : Controller {

		public IActionResult Requirements() {
			return View();
		}
	}
}
