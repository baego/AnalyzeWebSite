using AnalyzeDataWorker.DataClasses;
using AnalyzeWebSite.Data;
using AnalyzeWebSite.Data.SpyEntities;
using AnalyzeWebSiteAnalyzeTool.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnalyzeDataWorker {

	public class Worker {

		/// <summary>
		/// Возвращает список айдишников всех пользователей
		/// </summary>
		public List<string> ReturnUsers() {

			List<string> usrs = new List<string>();

			using (var spyDb = new SpyContext()) {

				foreach (var usr in spyDb.Users) {

					usrs.Add(usr.IP);
				}
			}

			return usrs;
		}

		/// <summary>
		/// Возвращает список количества посещений по дням со списком IDшников
		/// </summary>
		public List<DayVisits> ReturnVisits() {

			var result = new List<DayVisits>();
			int? pos = null;

			try {

				using (var spyDb = new SpyContext()) {

					var tempIP = new List<string>();
					var temp = new List<DayVisits>();

					// добавим пользователей по дате первого посещения
					foreach (var user in spyDb.Users) {
						tempIP = new List<string>();
						temp = result.Where(x => x.Date.Date == user.CreateDate.Date).ToList();

						if (temp.Any()) {

							pos = result.IndexOf(temp[0]);

							if (pos != null && !result[pos.Value].UserIds.Contains(user.IP)) {

								result[pos.Value].Number = result[pos.Value].Number + 1;
								result[pos.Value].UserIds.Add(user.IP);
							}

							pos = null;

						} else {

							var newVisit = new DayVisits {

								Date = user.CreateDate.Date,
								Number = 1,
								UserIds = new List<string>()
							};

							newVisit.UserIds.Add(user.IP);
							result.Add(newVisit);
						}

						temp = new List<DayVisits>();
					}


					// потом дополним из истории посещений
					foreach (var pageVisit in spyDb.ExitLog) {

						//temp = result.Where(x => x.Date.Date == pageVisit.Date.Date).ToList();

						//if (temp.Any()) {

						//	pos = result.IndexOf(temp[0]);

						//	if (pos != null && !result[pos.Value].UserIds.Contains(pageVisit.UserId)) {

						//		result[pos.Value].Number = result[pos.Value].Number + 1;
						//		result[pos.Value].UserIds.Add(pageVisit.UserId);
						//	}

						//	pos = null;

						//} else {

						//	var newVisit = new DayVisits {

						//		Date = pageVisit.Date.Date,
						//		Number = 1,
						//		UserIds = new List<string>()
						//	};

						//	newVisit.UserIds.Add(pageVisit.UserId);
						//	result.Add(newVisit);

						//}

						//temp = new List<DayVisits>();
					}
				}

			} catch (Exception ex) {

			}

			result = result.OrderBy(x => x.Date).ToList();
			return result;
		}

		/// <summary>
		/// Возвращает список браузеров
		/// </summary>
		public List<BrowserUsers> ReturnBrowsers() {

			List<BrowserUsers> results = new List<BrowserUsers>();

			try {
				using (var spyDb = new SpyContext()) {

					var temp = new List<BrowserUsers>();
					int? pos = null;

					foreach (var brwsr in spyDb.Browsers) {

						temp = results.Where(x => x.Name == brwsr.Browser).ToList();

						if (temp.Any()) {

							pos = results.IndexOf(temp[0]);
							results[pos.Value].Number += 1;

							pos = null;

						} else {

							results.Add(new BrowserUsers {
								Name = brwsr.Browser,
								Number = 1
							});
						}

						temp = new List<BrowserUsers>();
					}
				}
			} catch (Exception ex) {


			}

			return results;
		}

		/// <summary>
		/// Возвращает список геолокаций
		/// </summary>
		public List<string> ReturnGeolocation() {

			List<string> locs = new List<string>();

			using (var spyDb = new SpyContext()) {

				foreach (var usr in spyDb.Geolocations) {

					locs.Add(usr.Location);
				}
			}

			return locs;
		}

		/// <summary>
		/// Возвращает сведения о времени загрузки
		/// </summary>
		public List<PageLoadTime> ReturnPageLoadTime() {

			var results = new List<PageLoadTime>();

			try {
				using (var spyDb = new SpyContext()) {

					var temp = new List<PageLoadTime>();
					int? pos = null;

					foreach (var load in spyDb.PageLoadTimeLog) {

						temp = results.Where(x => x.PageName == load.Page).ToList();

						if (temp.Any()) {

							pos = results.IndexOf(temp[0]);
							results[pos.Value].AverageLoads += 1;
							results[pos.Value].Time += load.Time;

							pos = null;

						} else {

							results.Add(new PageLoadTime {
								PageName = load.Page,
								AverageLoads = 1,
								Time = load.Time
							});
						}

						temp = new List<PageLoadTime>();
					}
				}
			} catch (Exception ex) {

			}

			results.ForEach(x => x.Time = x.Time / x.AverageLoads);
			results = results.OrderByDescending(x => x.AverageLoads).ToList();

			return results;
		}

		/// <summary>
		/// Возвращает количество посещений страниц сайта
		/// </summary>
		public List<Pages> ReturnPopularPages() {

			List<Pages> results = new List<Pages>();

			try {
				using (var spyDb = new SpyContext()) {

					var temp = new List<Pages>();
					int? pos = null;

					foreach (var page in spyDb.ExitLog) {

						temp = results.Where(x => x.Name == page.Page).ToList();

						if (temp.Any()) {

							pos = results.IndexOf(temp[0]);
							results[pos.Value].Number += 1;

							pos = null;

						} else {

							results.Add(new Pages {
								Name = page.Page,
								Number = 1
							});
						}


						temp = new List<Pages>();
					}


				}
			} catch (Exception ex) {


			}
			results = results.OrderByDescending(x => x.Number).ToList();

			return results;
		}

		/// <summary>
		/// Список согласий на куки
		/// </summary>
		public List<AnalyzeWebSiteAnalyzeTool.DataClasses.CookieAgree> ReturnCookieAgreeUsers() {

			List<AnalyzeWebSiteAnalyzeTool.DataClasses.CookieAgree> results = new List<AnalyzeWebSiteAnalyzeTool.DataClasses.CookieAgree>();

			try {
				using (var spyDb = new SpyContext()) {

					var temp = new List<AnalyzeWebSiteAnalyzeTool.DataClasses.CookieAgree>();
					int? pos = null;

					foreach (var agree in spyDb.CookieAgree) {

						temp = results.Where(x => x.UserId == agree.UserId).ToList();

						if (temp.Any()) {

							pos = results.IndexOf(temp[0]);
							results[pos.Value].Count += 1;

							pos = null;

						} else {

							results.Add(new AnalyzeWebSiteAnalyzeTool.DataClasses.CookieAgree {
								Count = 1,
								UserId = agree.UserId
							});
						}


						temp = new List<AnalyzeWebSiteAnalyzeTool.DataClasses.CookieAgree>();
					}


				}
			} catch (Exception ex) {


			}
			results = results.OrderByDescending(x => x.Count).ToList();

			return results;
		}

		/// <summary>
		/// Возвращает детализированный список сеансов
		/// </summary>
		public List<SessionDetails> ReturnDetailedSessions() {

			var result = new List<SessionDetails>();
			var pages = new List<ExitLog>();

			try {

				using (var spyDb = new SpyContext()) {

					foreach (var session in spyDb.Sessions) {

						result.Add(new SessionDetails {
							UserId = session.UserId,
							SessionId = session.Id,
							Date = session.Date
						});
					}

					result = result.OrderBy(x => x.Date).ToList();

					foreach (var session in result) {

						pages = spyDb.ExitLog.Where(x => x.SessionId == session.SessionId).ToList();

						foreach (var page in pages) {

							session.VisitedPages.Add(new VisitedPage {

								Date = page.Date,
								Name = page.Page,
								Time = page.Time
							});
						}
						session.VisitedPages = session.VisitedPages.OrderBy(x => x.Date).ToList();

						pages = new List<ExitLog>();
					}


				}

			} catch (Exception ex) {

			}

			return result;
		}

		/// <summary>
		/// Возвращает глубину просмотра
		/// </summary>
		public int ReturnViewsDeep() {

			var sessions = new List<Sessions>();
			var visits = 0;
			try {

				using (var spyDb = new SpyContext()) {

					var ss = spyDb.Sessions.ToList();
					sessions = spyDb.Sessions.Where(x => x.Date >= Convert.ToDateTime("12.05.2020")).ToList();

					foreach (var session in sessions) {

						if (sessions.IndexOf(session) > 0) {

							try {

								if (sessions[sessions.IndexOf(session) - 1].Date.Second == session.Date.Second)
									sessions.Remove(session);

							} catch (Exception ex) {

							}
						}
					}
				}

			} catch (Exception ex) {

			}
			var pages = ReturnPopularPages();
			pages.ForEach(x => visits += x.Number);
			return visits / sessions.Count;
		}

		/// <summary>
		/// Возвращает среднюю длину сессии
		/// </summary>
		public List<int> ReturnMiddleTime() {

			var sessions = ReturnDetailedSessions();
			var result = new List<int>();
			var validSessions = new List<int>();
			int sessionLength = 0;

			//считаем со всеми сеансами
			foreach (var session in sessions) {

				if (session.VisitedPages.Count != 0) {

					sessionLength = 0;
					session.VisitedPages.ForEach(x => sessionLength += x.Time);

					validSessions.Add(sessionLength);
				}
			}

			sessionLength = 0;
			validSessions.ForEach(x => sessionLength += x);
			result.Add(sessionLength / sessions.Count);
			validSessions = new List<int>();

			//считаем по гугловски
			foreach (var session in sessions) {

				if (session.VisitedPages.Count > 1) {

					sessionLength = 0;
					session.VisitedPages.ForEach(x => sessionLength += x.Time);

					validSessions.Add(sessionLength);
				}
			}

			sessionLength = 0;
			validSessions.ForEach(x => sessionLength += x);
			result.Add(sessionLength / sessions.Count);
			validSessions = new List<int>();

			//считаем по яндексовски
			foreach (var session in sessions) {

				if (session.VisitedPages.Count != 0) {

					sessionLength = 0;

					if (session.VisitedPages.Count > 1 && session.VisitedPages[0].Time > 15)
						session.VisitedPages.ForEach(x => sessionLength += x.Time);

					validSessions.Add(sessionLength);
				}
			}

			sessionLength = 0;
			validSessions.ForEach(x => sessionLength += x);
			result.Add(sessionLength / sessions.Count);

			return result;
		}

		/// <summary>
		/// Считает показатель отказов
		/// </summary>
		public List<double> ReturnBounceRate() {

			var result = new List<double>();
			var bounceSessions = 0;
			var sessions = ReturnDetailedSessions();

			//методу гугла
			foreach (var session in sessions) {

				if (session.VisitedPages.Count == 1)
					bounceSessions += 1;
			}
			result.Add(bounceSessions * 100 / sessions.Count);

			bounceSessions = 0;

			//метод яндекса
			foreach (var session in sessions) {

				if (session.VisitedPages.Count == 1 && session.VisitedPages[0] != null
						&& session.VisitedPages[0].Time < 15)
					bounceSessions += 1;
			}

			result.Add(bounceSessions * 100 / sessions.Count);

			return result;
		}

		/// <summary>
		/// Возвращает список нажатий на изображения
		/// </summary>
		public List<Pics> ReturnPics() {

			var pics = new List<Pics>();

			using (var spyDb = new SpyContext()) {

				pics = spyDb.Pics.ToList();
				pics.ForEach(x => Console.WriteLine(x.Date + "   " + x.Name));
			}
			return pics;
		}

		/// <summary>
		/// Возвращает использование ссылок
		/// </summary>
		public List<LinksDetails> ReturnLinks() {

			var links = new List<Links>();
			var details = new List<LinksDetails>();
			var isNorm = false;

			using (var spyDb = new SpyContext()) {

				links = spyDb.Links.ToList();
				links = links.OrderBy(x => x.Date).ToList();

				foreach(var link in links) {

					foreach (var det in details) {

						if (link.Destination == det.Destination && link.Source == det.Source && link.Type == det.Type) {
							det.Number += 1;
							isNorm = true;
						}
					}

					if (!isNorm) {
						details.Add(new LinksDetails {
							Destination = link.Destination,
							Number = 1,
							Source = link.Source,
							Type = link.Type
						});
					}

					isNorm = false;
				}

			}
			return details;
		}

		/// <summary>
		/// Возвращает результаты из форм
		/// </summary>
		public FormsResult ReturnFormsResults() {

			var result = new FormsResult();
			var test = new List<TestResult>();
			var uiux = new List<UiuxResult>();
			var free = new List<FreeTextResult>();
			var common = new List<CommonResult>();
			var func = new List<FunctionalResult>();

			using (var spyDb = new SpyContext()) {

				foreach (var form in spyDb.CommonForms) {

					common.Add(new CommonResult {
						City = form.City,
						SessionId = form.SessionId,
						Date = form.Date,
						Degree = ReturnDegree(form.DegreeSelect),
						Direction = ReturnDirection(form.Direction),
						Name = form.Name,
						Year = form.Year
					});
					result.Common = common;
				}

				foreach (var form in spyDb.Tests) {

					test.Add(new TestResult {
						SessionId = form.SessionId,
						Date = form.Date,
						Degree = ReturnDegree(form.Degree),
						Direction = ReturnDegree(form.Direction),
						Organization = form.Organization,
						Science = ReturnDegree(form.Science),
						UserId = form.UserId
					});
					result.Test = test;
				}

				foreach (var form in spyDb.UiuxForms) {
					uiux.Add(new UiuxResult {
						SessionId = form.SessionId,
						Date = form.Date,
						UserId = form.UserId,
						AverageUI = form.AverageUI,
						CorrectWork = form.CorrectWork,
						FormPreference = form.FormPreference,
						Modern = form.Modern,
						PageLocation = form.PageLocation,
						Trust = form.Trust
					});

					result.Uiux = uiux;
				}

				foreach (var form in spyDb.FreeTextForms) {
					free.Add(new FreeTextResult {
						SessionId = form.SessionId,
						Date = form.Date,
						UserId = form.UserId,
						Text = form.Text
					});

					result.FreeText = free;
				}

				foreach (var form in spyDb.FunctionalForms) {
					func.Add(new FunctionalResult {
						SessionId = form.SessionId,
						Date = form.Date,
						UserId = form.UserId,
						Average = form.Average,
						Limitations = form.Limitations,
						LoadTime = form.LoadTime,
						OneThing = form.OneThing,
						Portrai = form.Portrait,
						Reccomend = form.Reccomend,
						SomethingNew = form.SomethingNew,
						TrueData = form.TrueData
					});

					result.Functional = func;
				}
			}

			return result;
		}

		/// <summary>
		/// Обработка посещаемости в сессиях
		/// </summary>
		public List<SessionGroup> SessionDetailsProcessing() {

			var visitsNumberGroup = new List<SessionGroup>();
			var unprocessedSessions = ReturnDetailedSessions();
			var sessions = new List<SessionDetails>();
			var newList = new List<SessionDetails>();
			var isFind = false;

			//foreach (var session in unprocessedSessions) {

			//	if (session.VisitedPages.Count != 0)
			//		sessions.Add(session);
			//}

			foreach (var session in unprocessedSessions) {

				foreach (var res in visitsNumberGroup) {

					if (res.Sessions[0].VisitedPages.Count == session.VisitedPages.Count && res.Sessions[0].Date >= Convert.ToDateTime("12.05.2020") ) {

						isFind = true;
						res.Sessions.Add(session);
					} 
				}

				if (!isFind) {

					newList = new List<SessionDetails>();
					newList.Add(session);

					visitsNumberGroup.Add(new SessionGroup {
						Sessions = newList
					});
				}

				isFind = false;
			}

			visitsNumberGroup = visitsNumberGroup.OrderByDescending(x => x.Sessions.Count).ToList();

			return visitsNumberGroup;
		}

		/// <summary>
		/// Считает фокус внимания
		/// </summary>
		public void AttentionFocus() {

		}

		/// <summary>
		/// Добывает из БД сайта название степени по айдишнику
		/// </summary>
		private string ReturnDegree(int degree) {

			using (var siteDb = new SiteContext()) {
				var degr = siteDb.Degrees.Where(x => x.Id == degree).ToList();
				if (degr.Count != 0)
					return degr[0].Name;
				else return "бакалавр";
			}
		}

		/// <summary>
		/// Добывает из БД сайта название направления по айдишнику
		/// </summary>
		private string ReturnDirection(int direction) {


			using (var siteDb = new SiteContext()) {
				var degr = siteDb.Directions.Where(x => x.Id == direction).ToList();
				if (degr.Count != 0)
					return degr[0].Name;
				else return "Технические науки";
			}
		}

		/// <summary>
		/// Добывает из БД сайта название научной области по айдишнику
		/// </summary>
		private string ReturnScience(int science) {

			using (var siteDb = new SiteContext()) {

				return siteDb.Sciences.Where(x => x.Id == science).ToList()[0].Name;
			}
		}
	}
}
