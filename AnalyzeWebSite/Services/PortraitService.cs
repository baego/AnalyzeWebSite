using AnalyzeWebSite.Data;
using AnalyzeWebSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyzeWebSite.Services {
	public class PortraitService {

		/// <summary>
		/// Публичный метод, возвращающий портрет пользователя
		/// </summary>
		/// <param name="ip"></param>
		/// <param name="sessionId"></param>
		public PortraitModel GetPortrait(string ip, Guid sessionId, PortraitModel model) {

			return CreatePortrait(ip, sessionId, model);
		}

		/// <summary>
		/// Приватный метод, работающий над заполнением модели портрета пользователя
		/// </summary>
		private PortraitModel CreatePortrait(string ip, Guid sessionId, PortraitModel model) {


			try {

				//ip-адрес
				model.Ip = ip;

				using (var spyDB = new SpyContext()) {

					//смотрим текущий браузер
					try {

						var browser = spyDB.Browsers.Where(x => x.UserId == ip)
							.OrderByDescending(x => x.Date)
							.ToList();

						if (browser != null && browser.Count() != 0) {

							model.Browser = browser[0].Browser;
						}
					} catch (Exception ex) {

						SiteService.WriteError(ex.Message, ex.Source, ex.StackTrace);
					}


					//предыдущие браузеры
					try {

						var previousBrowsers = spyDB.Browsers.Where(x => x.UserId == ip).ToList();
						previousBrowsers.ForEach(x => model.PreviousBrowsers.Add(x.Browser));

					} catch (Exception ex) {

						SiteService.WriteError(ex.Message, ex.Source, ex.StackTrace);
					}


					//геолокация
					try {

						var currentGeolocation = spyDB.Geolocations.Where(x => x.UserId == ip).OrderByDescending(x => x.Date).ToList();
						if (currentGeolocation != null && currentGeolocation.Count() != 0)
							model.Geolocation = currentGeolocation[0].Location;

					} catch (Exception ex) {

						SiteService.WriteError(ex.Message, ex.Source, ex.StackTrace);
					}

					//время данного сеанса
					try {

						var thisSessionHistory = spyDB.ExitLog.Where(x => x.UserId == ip && x.SessionId == sessionId).ToList();

						//и сразу историю сеанса
						foreach (var page in thisSessionHistory) {

							model.SpendedTime += page.Time;
							thisSessionHistory.OrderBy(x => x.Date);
							model.History.Add(new HistoryClass { Page = page.Page, Time = page.Time });
						}
					} catch (Exception ex) {

						SiteService.WriteError(ex.Message, ex.Source, ex.StackTrace);
					}

					//среднее время загрузки страниц
					try {
						var loads = spyDB.PageLoadTimeLog.Where(x => x.UserId == ip && x.SessionId == sessionId).ToList();
						int totalLoadTime = 0;

						loads.ForEach(x => totalLoadTime += x.Time);
						model.AverageLoadTime = totalLoadTime / loads.Count;

					} catch (Exception ex) {

						SiteService.WriteError(ex.Message, ex.Source, ex.StackTrace);
					}

					//количество потерь фокуса
					try {
						var losts = spyDB.PageLoadTimeLog.Where(x => x.UserId == ip && x.SessionId == sessionId).ToList();

						model.FocusLostCount = losts.Count;

					} catch (Exception ex) {

						SiteService.WriteError(ex.Message, ex.Source, ex.StackTrace);
					}
					
					//количество просмотренных картинок
					try {
						var views = spyDB.Pics.Where(x => x.UserId == ip && x.SessionId == sessionId).ToList();

						model.PicsViewed = views.Count;

					} catch (Exception ex) {

						SiteService.WriteError(ex.Message, ex.Source, ex.StackTrace);
					}
					
					//количество переходов по внешним ссылкам
					try {
						
						var externals = spyDB.Links.Where(x => x.UserId == ip && x.SessionId == sessionId 
														//тип ссылки 1 это внешняя ссылка, тип 2 это ссылка на источник в статье
														&& (x.Type == 1 || x.Type == 2)).ToList();

						model.ExternalLinks = externals.Count;

					} catch (Exception ex) {

						SiteService.WriteError(ex.Message, ex.Source, ex.StackTrace);
					}

					//количество предыдущих сессий
					try {
						
						var previousSessions = spyDB.Sessions.Where(x => x.UserId == ip).ToList();

						model.PreviousSessions = previousSessions.Count;

					} catch (Exception ex) {

						SiteService.WriteError(ex.Message, ex.Source, ex.StackTrace);
					}
				}
			} catch (Exception ex) {

				SiteService.WriteError(ex.Message, ex.Source, ex.StackTrace);

			}

			return model;
		}
	}
}
