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


		private Guid CheckSession(string ip) {

			var spyService = new SpyService();
			Guid sessionId = new Guid();

			if (!HttpContext.Session.Keys.Contains("sessionId")) {

				sessionId = Guid.Parse(HttpContext.Session.Id);
				HttpContext.Session.SetString("sessionId", HttpContext.Session.Id);
				spyService.CreateSession(ip, sessionId);
			} else {

				sessionId = Guid.Parse(HttpContext.Session.GetString("sessionId"));
			}

			if (!HttpContext.Session.Keys.Contains("ipAddress") &&
				ip != null) {

				HttpContext.Session.SetString("ipAddress", ip);
			} 

			return sessionId;
		}

		/// <summary>
		/// Получение IP нового пользователя и запись его в БД
		/// </summary>
		/// <param name="ip"></param>
		/// <returns></returns>
		[HttpPost]
		public void GetIp(string ip) {

			var spyService = new SpyService();
			var sessionId = CheckSession(ip);

			spyService.CheckIp(ip);
		}

		/// <summary>
		/// Получение геоданных пользователя и запись в БД
		/// </summary>
		/// <param name="geo"></param>
		[HttpPost]
		public void GetGeo(string geo, string ip) {

			var spyService = new SpyService();
			var sessionId = CheckSession(ip);

			spyService.WriteGeo(geo, ip, sessionId);
		}

		/// <summary>
		/// Получение и запись данных о браузере пользователя
		/// </summary>
		[HttpPost]
		public void GetBrowser(string browser, string ip) {

			var spyService = new SpyService();
			var sessionId = CheckSession(ip);

			spyService.WriteBrowser(browser, ip, sessionId);
		}

		[HttpPost]
		public void GetReferer(string referer, string ip) {

			var spyService = new SpyService();
			var sessionId = CheckSession(ip);

			spyService.WriteReferer(referer, ip, sessionId);
		}

		/// <summary>
		/// Логгирование покидания страницы и времени, проведенного на ней
		/// </summary>
		[HttpPost]
		public void ExitLog(int time, string page, string ip) {

			var spyService = new SpyService();
			var sessionId = CheckSession(ip);

			spyService.WriteExit(time, page, ip, sessionId);
		}

		/// <summary>
		/// Запись времени загрузки страницы
		/// </summary>
		[HttpPost]
		public void LoadTime(string ip, int time, string page) {

			var spyService = new SpyService();
			var sessionId = CheckSession(ip);

			spyService.WriteLoad(time, page, ip, sessionId);
		}
		
		/// <summary>
		/// Запись события потери фокуса окна
		/// </summary>
		[HttpPost]
		public void FocusLost(string ip, string page) {
			var spyService = new SpyService();
			var sessionId = CheckSession(ip);

			spyService.WriteFocusLost(page, ip, sessionId);
		}
		
		/// <summary>
		/// Запись события согласия на куки
		/// </summary>
		[HttpPost]
		public void CookieAgree(string ip) {
			var spyService = new SpyService();
			var sessionId = CheckSession(ip);

			spyService.CookieAgree(ip, sessionId);
		}
		
		/// <summary>
		/// Запись события согласия на куки
		/// </summary>
		[HttpPost]
		public void Links(string ip, string page, string source, string destination, int type) {
			var spyService = new SpyService();
			var sessionId = CheckSession(ip);

			spyService.Links(ip, page, source, destination, type, sessionId);
		}
		
		/// <summary>
		/// Запись нажатия на картинку
		/// </summary>
		[HttpPost]
		public void Pics(string ip, string name) {

			var spyService = new SpyService();
			var sessionId = CheckSession(ip);

			spyService.Pics(ip, name, sessionId);
		}
	}
}
