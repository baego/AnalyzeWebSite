using AnalyzeWebSite.Data;
using AnalyzeWebSite.Data.SiteEntites;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyzeWebSite.Services {
	public  class TestService  {
		

		/// <summary>
		/// Метод для получения выбираемых пользователем степеней
		/// </summary>
		/// <returns></returns>
		public SelectList GetDegrees() {
			SiteContext siteDb = new SiteContext();

				var degrs = siteDb.Degrees;
				return new SelectList(degrs, "Id", "Name");
		}
	}
}
