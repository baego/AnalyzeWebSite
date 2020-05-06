using AnalyzeWebSite.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace AnalyzeWebSite.Controllers {

	public class SpyController : Controller {

		/// <summary>
		/// Получение IP нового пользователя и запись его в БД
		/// </summary>
		/// <param name="ip"></param>
		/// <returns></returns>
		[HttpPost]
		public void GetIp(string ip) {

			var spyService = new SpyService();
			spyService.CheckIp(ip);
		}

		/// <summary>
		/// Получение геоданных пользователя и запись в БД
		/// </summary>
		/// <param name="geo"></param>
		[HttpPost]
		public void GetGeo(string geo, string ip) {

			var spyService = new SpyService();
			spyService.WriteGeo(geo, ip);
		}

		/// <summary>
		/// Получение и запись данных о браузере пользователя
		/// </summary>
		[HttpPost]
		public void GetBrowser(string browser, string ip) {

			var spyService = new SpyService();
			spyService.WriteBrowser(browser, ip);
		}

		/// <summary>
		/// Логгирование покидания страницы и времени, проведенного на ней
		/// </summary>
		/// <param name="time"></param>
		/// <param name="page"></param>
		/// <param name="ip"></param>
		[HttpPost]
		public void ExitLog(int time, string page, string ip) {

			var spyService = new SpyService();
			spyService.WriteExit(time, page, ip);
		}
	}
}
