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
		/// <param name="geo"></param>
		/// <param name="ip"></param>
		public void WriteGeo(string geo, string ip) {

			WriteGeoInDb(geo, ip);
		}

		/// <summary>
		/// Метод записывает браузер пользователя в базу
		/// </summary>
		/// <param name="browser"></param>
		/// <param name="ip"></param>
		public void WriteBrowser(string browser, string ip) {

			WriteBrowserInDb(browser, ip);
		}

		/// <summary>
		/// Логирование покидания страницы
		/// </summary>
		/// <param name="time"></param>
		/// <param name="page"></param>
		/// <param name="ip"></param>
		public void WriteExit(int time, string page, string ip) {

			WriteExitInDb(time, page, ip);
		}

		/// <summary>
		/// Приватный метод записи браузера в базу
		/// </summary>
		/// <param name="browser"></param>
		/// <param name="ip"></param>
		private void WriteBrowserInDb(string browser, string ip) {

			try {

				using (var spyDB = new SpyContext()) {

					//достанем предыдущий браузер этого пользователя
					var previousBrowsers = spyDB.Browsers
																			 .Where(x => x.UserId == ip)
																			 //отсорутируем по убыванию даты
																			 .OrderByDescending(y => y.Date)
																			 .ToList();

					//если видим этот IP впервые или браузер не совпадает с предыдущим, то запишем
					if (previousBrowsers.Count == 0 || previousBrowsers[0].Browser != browser) {

						spyDB.Browsers.Add(new Browsers { Browser = browser, UserId = ip, Date = DateTime.Now });
						spyDB.SaveChanges();
					}
				}

			} catch (Exception ex) {

				SiteService.WriteError(ex.Message, ex.Source, ex.StackTrace);
			}
		}

		/// <summary>
		/// Приватный метод записи геолокации в БД
		/// </summary>
		/// <param name="geo"></param>
		/// <param name="ip"></param>
		private void WriteGeoInDb(string geo, string ip) {

			try {

				using (var spyDB = new SpyContext()) {

					//достанем предыдущее местонахождение этого пользователя
					var previousLocations = spyDB.Geolocations
																			 .Where(x => x.UserId == ip)
																			 //отсорутируем по убыванию даты
																			 .OrderByDescending(y => y.Date)
																			 .ToList();

					//если видим этот IP впервые или местоположение не совпадает с предыдущим, то запишем
					if (previousLocations.Count== 0 || previousLocations[0].Location != geo) {

						spyDB.Geolocations.Add(new Geolocations { Location = geo, UserId = ip, Date = DateTime.Now });
						spyDB.SaveChanges();
					}
				}

			} catch (Exception ex) {

				SiteService.WriteError(ex.Message, ex.Source, ex.StackTrace);
			}
		}

		/// <summary>
		/// Приватный метод проверки IP в БД
		/// </summary>
		/// <param name="ip"></param>
		/// <returns></returns>
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
		/// <param name="ip"></param>
		private void WriteIpInDb(string ip, SpyContext spyDB) {
			try {
				spyDB.Users.Add(new Users { IP = ip, CreateDate = DateTime.Now });
				spyDB.SaveChanges();
			} catch (Exception ex) {

				SiteService.WriteError(ex.Message, ex.Source, ex.StackTrace);

			}
		}

		/// <summary>
		/// Запись IP в БД
		/// </summary>
		/// <param name="ip"></param>
		private void WriteExitInDb(int time, string page, string ip) {
			try {
				using (var spyDB = new SpyContext()) {

					decimal timeDec = time / 1000;

					time = Convert.ToInt32(Math.Round(timeDec));
					spyDB.ExitLog.Add(new ExitLog { Page = page, UserId = ip, Date = DateTime.Now, Time = time });
					spyDB.SaveChanges();
				}
			} catch (Exception ex) {

				SiteService.WriteError(ex.Message, ex.Source, ex.StackTrace);

			}
		}
	}

}
