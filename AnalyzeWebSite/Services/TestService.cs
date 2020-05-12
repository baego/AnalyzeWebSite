using AnalyzeWebSite.Data;
using AnalyzeWebSite.Data.SiteEntites;
using AnalyzeWebSite.Data.SpyEntities;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyzeWebSite.Services {
	public class TestService {


		/// <summary>
		/// Метод для получения выбираемых пользователем степеней
		/// </summary>
		/// <returns></returns>
		public List<Degrees> GetDegrees() {

			var degrs = new List<Degrees>();
			using (SiteContext siteDb = new SiteContext()) {

				degrs = siteDb.Degrees.ToList();
			}
			return degrs;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		public List<Sciences> GetSciences() {

			var sciences = new List<Sciences>();
			using (SiteContext siteDb = new SiteContext()) {

				sciences = siteDb.Sciences.ToList();
			}
			return sciences;
		}

		/// <summary>
		/// Получить направления по науке
		/// </summary>
		public List<Directions> GetInnerRoutes(int id) {

			return GetInnerRoutesFromDb(id);
		}

		/// <summary>
		/// Метод отправляет результаты теста на обработку и возвращает список тем
		/// </summary>
		public List<string> TestResults(string ip, Guid sessionId, int degreeSelect, int degreeRouteSelect, string organizationText, int innerRouteSelect) {

			return TestResultsProcessing(ip, sessionId, degreeSelect, degreeRouteSelect, organizationText, innerRouteSelect);
		}

		/// <summary>
		/// Запись результатов формы "общая информация"
		/// </summary>
		public void WriteCommon(string ip, Guid sessionId, string yourName, string yourCity, int yourYear, int direction, int degreeSelect) {
			WriteCommonInDb(ip, sessionId, yourName, yourCity, yourYear, direction, degreeSelect);
			WriteFormFillInDb(ip, sessionId, "Common");
		}

		/// <summary>
		/// Запись результатов формы "общая информация"
		/// </summary>
		public void WriteFunctional(string ip, Guid sessionId, int average, int loadTime, int limitations, string oneThing, int somethingNew, int reccomend, int portrait, int trueData) {
			WriteFunctionalInDb(ip, sessionId, average, loadTime, limitations, oneThing, somethingNew, reccomend, portrait, trueData);
			WriteFormFillInDb(ip, sessionId, "Functional");
		}

		/// <summary>
		/// Запись результатов формы "общая информация"
		/// </summary>
		public void WriteUiux(string ip, Guid sessionId, int averageUi, int correctWork, int trust, int modern, int pageLocations, int formPreference) {
			WriteUiuxInDb(ip, sessionId, averageUi, correctWork, trust, modern, pageLocations, formPreference);
			WriteFormFillInDb(ip, sessionId, "Uiux");
		}

		/// <summary>
		/// Запись формы свободного обращения
		/// </summary>
		public void WriteFreeText(string ip, Guid sessionId, string freeText) {
			WriteFreeTextInDb(ip, sessionId, freeText);
			WriteFormFillInDb(ip, sessionId, "FreeText");

		}

		/// <summary>
		/// Записываем сам факт записи в форму
		/// </summary>
		private void WriteFormFillInDb(string ip, Guid sessionId, string formName) {

			try {

				using (var spyDB = new SpyContext()) {

					spyDB.FormFills.Add(new FormFills { UserId = ip, Date = DateTime.Now, SessionId = sessionId, FormName = formName });
					spyDB.SaveChanges();
				}

			} catch (Exception ex) {

				SiteService.WriteError(ex.Message, ex.Source, ex.StackTrace);
			}
		}

		/// <summary>
		/// Запись данных их формы с общими данными
		/// </summary>
		private void WriteCommonInDb(string ip, Guid sessionId, string yourName, string yourCity, int yourYear, int direction, int degreeSelect) {

			try {

				using (var spyDB = new SpyContext()) {

					spyDB.CommonForms.Add(new CommonForms {
						UserId = ip, Date = DateTime.Now, SessionId = sessionId,
						City = yourCity, DegreeSelect = degreeSelect, Direction = direction, Name = yourName, Year = yourYear
					});

					spyDB.SaveChanges();
				}

			} catch (Exception ex) {

				SiteService.WriteError(ex.Message, ex.Source, ex.StackTrace);
			}
		}

		/// <summary>
		/// Запись данных из формы о функциональности сайта
		/// </summary>
		private void WriteFunctionalInDb(string ip, Guid sessionId, int average, int loadTime, int limitations, string oneThing, int somethingNew, int reccomend, int portrait, int trueData) {

			try {

				using (var spyDB = new SpyContext()) {

					spyDB.FunctionalForms.Add(new FunctionalForms {
						UserId = ip, Date = DateTime.Now, SessionId = sessionId,
						Average = average, Limitations = limitations, LoadTime = loadTime, OneThing = oneThing, Portrait = portrait,
						Reccomend = reccomend, SomethingNew = somethingNew, TrueData = trueData
					});

					spyDB.SaveChanges();
				}

			} catch (Exception ex) {

				SiteService.WriteError(ex.Message, ex.Source, ex.StackTrace);
			}
		}

		/// <summary>
		/// Запись из формы о дизайне и user expirience
		/// </summary>
		private void WriteUiuxInDb(string ip, Guid sessionId, int averageUi, int correctWork, int trust, int modern, int pageLocations, int formPreference) {

			try {

				using (var spyDB = new SpyContext()) {

					spyDB.UiuxForms.Add(new UiuxForms {
						UserId = ip, Date = DateTime.Now, SessionId = sessionId,
						AverageUI = averageUi, CorrectWork = correctWork, Trust = trust,
						Modern = modern, PageLocation = pageLocations, FormPreference = formPreference
					});

					spyDB.SaveChanges();
				}

			} catch (Exception ex) {

				SiteService.WriteError(ex.Message, ex.Source, ex.StackTrace);
			}
		}

		/// <summary>
		/// Запись содержимого "свободной" формы
		/// </summary>
		private void WriteFreeTextInDb(string ip, Guid sessionId, string freeText) {

			try {

				using (var spyDB = new SpyContext()) {

					spyDB.FreeTextForms.Add(new FreeTextForms { UserId = ip, Date = DateTime.Now, SessionId = sessionId, Text = freeText });
					spyDB.SaveChanges();
				}

			} catch (Exception ex) {

				SiteService.WriteError(ex.Message, ex.Source, ex.StackTrace);
			}
		}

		/// <summary>
		/// Метод возвращает список направлений для выбранной науки
		/// </summary>
		private List<Directions> GetInnerRoutesFromDb(int id) {

			var res = new List<Directions>();
			try {

				using (var siteDB = new SiteContext()) {

					res = siteDB.Directions.Where(x => x.ScienceId == id).ToList();
				}
			} catch (Exception ex) {

				SiteService.WriteError(ex.Message, ex.Source, ex.StackTrace);
			}

			return res;
		}

		/// <summary>
		/// Метод записывает факт прохождения теста и возвращает результаты для пользователя
		/// </summary>
		private List<string> TestResultsProcessing(string ip, Guid sessionId, int degreeSelect, int degreeRouteSelect, string organizationText, int innerRouteSelect) {

			string org = "None";
			var result = new List<string>();

			if (organizationText != "" || organizationText != "нет")
				org = organizationText;

			//сначала запишем в шпионскую базу данные и отдельно сам факт заполнения
			try {
				using (var spyDB = new SpyContext()) {

					spyDB.Tests.Add(new Tests {
						Date = DateTime.Now, Degree = degreeSelect, Direction = innerRouteSelect, Organization = org,
						Science = degreeRouteSelect, SessionId = sessionId, UserId = ip
					});

					spyDB.FormFills.Add(new FormFills {
						FormName = "Test", UserId = ip, SessionId = sessionId, Date = DateTime.Now
					});

					spyDB.SaveChanges();
				}
			} catch (Exception ex) {

				SiteService.WriteError(ex.Message, ex.Source, ex.StackTrace);
			}

			//теперь создадим список результатов из базы сайта
			try {
				using (var siteDB = new SiteContext()) {

					var themes = siteDB.Themes.Where(x => x.DirectionId == innerRouteSelect).ToList();

					foreach(var theme in themes) {

						//если тема подразумевает наличие организации и сама организация заполнена
						if (!theme.Organization && org != "None" ) {

							result.Add(theme.Name);
						} else {

							result.Add(theme.Name + org + "\"");
						}
					}
				}
			} catch (Exception ex) {

				SiteService.WriteError(ex.Message, ex.Source, ex.StackTrace);
			}

			return result;
		}
	}
}