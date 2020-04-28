using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AnalyzeWebSite.Controllers {

	public class SpyController : Controller {

		[HttpPost]
		public string GetIp(List<string> val) {

			var res = val[2].Remove(0, 3);
			return res;
		}

	}

	
}
