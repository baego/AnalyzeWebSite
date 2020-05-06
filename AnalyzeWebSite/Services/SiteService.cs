using AnalyzeWebSite.Data;
using AnalyzeWebSite.Data.SiteEntites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyzeWebSite.Services {
	public class SiteService {

		/// <summary>
		/// Запись ошибки 
		/// </summary>
		/// <param name="message"></param>
		/// <param name="source"></param>
		/// <param name="trace"></param>
		public static void WriteError(string message, string source, string trace) {

			WriteErrorInDb( message, source, trace);
		}

		/// <summary>
		/// Приватное логирование ошибки в базе
		/// </summary>
		private static void WriteErrorInDb(string message, string source, string trace) {

			try {
				using (var siteDB = new SiteContext()) {

					siteDB.ErrorLog.Add(new ErrorLog { Message = message, Date = DateTime.Now, Source = source, Trace = trace });
					siteDB.SaveChanges();
				}
			} catch (Exception ex) {
				//не стоит логировать и тем самым создавать рекурсию, просто замнем
			}
		}
	}
}
