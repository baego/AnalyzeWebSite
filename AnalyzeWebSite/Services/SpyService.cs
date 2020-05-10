using AnalyzeWebSite.Data;
using AnalyzeWebSite.Data.SpyEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AnalyzeWebSite.Services {

	public class SpyService {

		/// <summary>
		/// Метод проверяет наличие данного IP в базе
		/// </summary>
		/// <param name="ip"></param>
		public void CheckIp(string ip) {

			CheckIpInDb(ip);
		}

		/// <summary>
		/// Метод записывает текущее местоположение пользователя в базу
		/// </summary>
		public void WriteGeo(string geo, string ip, Guid sessionId) {

			WriteGeoInDb(geo, ip, sessionId);
		}

		/// <summary>
		/// Метод записывает браузер пользователя в базу
		/// </summary>
		public void WriteBrowser(string browser, string ip, Guid sessionId) {

			WriteBrowserInDb(browser, ip, sessionId);
		}

		/// <summary>
		/// Логирование покидания страницы
		/// </summary>
		public void WriteExit(int time, string page, string ip, Guid sessionId) {

			WriteExitInDb(time, page, ip, sessionId);
		}

		/// <summary>
		/// Логирование времени загрузки страницы
		/// </summary>
		public void WriteLoad(int time, string page, string ip, Guid sessionId) {

			WriteLoadInDb(time, page, ip, sessionId);
		}

		/// <summary>
		/// Создание сессии 
		/// </summary>
		public void CreateSession(string ip, Guid sessionId) {

			CreateSessionInDb(ip, sessionId);
		}
		
		/// <summary>
		/// Логирование времени загрузки страницы
		/// </summary>
		public void WriteFocusLost(string page, string ip, Guid sessionId) {

			WriteFocusLostInDb(page, ip, sessionId);
		}
		
		/// <summary>
		/// Логирование времени загрузки страницы
		/// </summary>
		public void CookieAgree(string ip, Guid sessionId) {

			WriteCookieAgreeInDb(ip, sessionId);
		}
		
		/// <summary>
		/// Логирование времени загрузки страницы
		/// </summary>
		public void Links(string ip, string page, string source, string destination, int type, Guid sessionId) {

			WriteLinksInDb(ip, page, source, destination, type, sessionId);
		}
		
		/// <summary>
		/// Логирование времени загрузки страницы
		/// </summary>
		public void Pics(string ip, string name, Guid sessionId) {

			WritePicsInDb(ip, name, sessionId);
		}

		/// <summary>
		/// Приватный метод записи браузера в базу
		/// </summary>
		private void WriteBrowserInDb(string browser, string ip, Guid sessionId) {

			try {

				using (var spyDB = new SpyContext()) {



					spyDB.Browsers.Add(new Browsers { Browser = browser, UserId = ip, Date = DateTime.Now, SessionId = sessionId });
					spyDB.SaveChanges();
				}

			} catch (Exception ex) {

				SiteService.WriteError(ex.Message, ex.Source, ex.StackTrace);
			}
		}

		/// <summary>
		/// Приватный метод записи геолокации в БД
		/// </summary>
		private void WriteGeoInDb(string geo, string ip, Guid sessionId) {

			try {

				using (var spyDB = new SpyContext()) {

					spyDB.Geolocations.Add(new Geolocations { Location = geo, UserId = ip, Date = DateTime.Now, SessionId = sessionId });
					spyDB.SaveChanges();
				}

			} catch (Exception ex) {

				SiteService.WriteError(ex.Message, ex.Source, ex.StackTrace);
			}
		}

		/// <summary>
		/// Приватный метод проверки IP в БД
		/// </summary>
		private void CheckIpInDb(string ip) {

			try {
				using (var spyDB = new SpyContext()) {
					var user = spyDB.Users.Where(x => x.IP == ip).ToList();

					if (user.Count() == 0) {

						WriteIpInDb(ip, spyDB);
					}
				}
			} catch (Exception ex) {

				SiteService.WriteError(ex.Message, ex.Source, ex.StackTrace);
			}
		}

		/// <summary>
		/// Запись IP в БД
		/// </summary>
		private void WriteIpInDb(string ip, SpyContext spyDB) {
			try {
				spyDB.Users.Add(new Users { IP = ip, CreateDate = DateTime.Now });
				spyDB.SaveChanges();
			} catch (Exception ex) {

				SiteService.WriteError(ex.Message, ex.Source, ex.StackTrace);

			}
		}

		/// <summary>
		/// Запись события покидания страницы в БД
		/// </summary>
		private void WriteExitInDb(int time, string page, string ip, Guid sessionId) {
			try {
				using (var spyDB = new SpyContext()) {

					decimal timeDec = time / 1000;

					time = Convert.ToInt32(Math.Round(timeDec));
					spyDB.ExitLog.Add(new ExitLog { Page = page, UserId = ip, Date = DateTime.Now, Time = time, SessionId = sessionId });
					spyDB.SaveChanges();
				}
			} catch (Exception ex) {

				SiteService.WriteError(ex.Message, ex.Source, ex.StackTrace);

			}
		}

		/// <summary>
		/// Запись времени загрузки страницы в БД
		/// </summary>
		private void WriteLoadInDb(int time, string page, string ip, Guid sessionId) {
			try {
				using (var spyDB = new SpyContext()) {

					spyDB.PageLoadTimeLog.Add(new PageLoadTimeLog { Page = page, UserId = ip, Date = DateTime.Now, Time = time, SessionId = sessionId });
					spyDB.SaveChanges();
				}
			} catch (Exception ex) {

				SiteService.WriteError(ex.Message, ex.Source, ex.StackTrace);

			}
		}

		/// <summary>
		/// Запись времени загрузки страницы в БД
		/// </summary>
		private void CreateSessionInDb(string ip, Guid sessionId) {
			try {
				using (var spyDB = new SpyContext()) {

					spyDB.Sessions.Add(new Sessions { Id = sessionId, UserId = ip, Date = DateTime.Now });
					spyDB.SaveChanges();
				}
			} catch (Exception ex) {

				SiteService.WriteError(ex.Message, ex.Source, ex.StackTrace);

			}
		}
		
		/// <summary>
		/// Запись события потери фокуса
		/// </summary>
		private void WriteFocusLostInDb(string page, string ip, Guid sessionId) {
			try {
				using (var spyDB = new SpyContext()) {

					spyDB.FocusLost.Add(new FocusLost { Page = page, SessionId = sessionId, UserId = ip, Date = DateTime.Now });
					spyDB.SaveChanges();
				}
			} catch (Exception ex) {

				SiteService.WriteError(ex.Message, ex.Source, ex.StackTrace);

			}
		}


		/// <summary>
		/// Запись согласия на куки в базу
		/// </summary>
		/// <param name="ip"></param>
		/// <param name="sessionId"></param>
		private void WriteCookieAgreeInDb(string ip, Guid sessionId) {

			try {
				using (var spyDB = new SpyContext()) {

					spyDB.CookieAgree.Add(new CookieAgree { SessionId = sessionId, UserId = ip, Date = DateTime.Now });
					spyDB.SaveChanges();
				}
			} catch (Exception ex) {

				SiteService.WriteError(ex.Message, ex.Source, ex.StackTrace);

			}
		}

		/// <summary>
		/// Логгирование переходов по ссылкам в базе
		/// </summary>
		/// <param name="ip"></param>
		/// <param name="page"></param>
		/// <param name="source"></param>
		/// <param name="destination"></param>
		/// <param name="type"></param>
		/// <param name="sessionId"></param>
		private void WriteLinksInDb(string ip, string page, string source, string destination, int type, Guid sessionId) {

			try {
				using (var spyDB = new SpyContext()) {

					spyDB.Links.Add(new Links {Page = page, Source = source,
								Type = type, Destination = destination,
								SessionId = sessionId, UserId = ip,
								Date = DateTime.Now });
					spyDB.SaveChanges();
				}
			} catch (Exception ex) {

				SiteService.WriteError(ex.Message, ex.Source, ex.StackTrace);

			}
		}

		/// <summary>
		/// Запись клика по картинке в базу данных
		/// </summary>
		/// <param name="ip"></param>
		/// <param name="page"></param>
		/// <param name="source"></param>
		/// <param name="destination"></param>
		/// <param name="type"></param>
		/// <param name="sessionId"></param>
		private void WritePicsInDb(string ip, string name, Guid sessionId) {

			try {
				using (var spyDB = new SpyContext()) {

					spyDB.Pics.Add(new Pics { Name = name,
								SessionId = sessionId, UserId = ip,
								Date = DateTime.Now });
					spyDB.SaveChanges();
				}
			} catch (Exception ex) {

				SiteService.WriteError(ex.Message, ex.Source, ex.StackTrace);

			}
		}
	}

}
