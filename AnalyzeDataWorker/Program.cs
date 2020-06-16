using System;

namespace AnalyzeDataWorker {
	class Program {

		static void Main(string[] args) {

			Console.WriteLine("Начинаем работу!");

			WatchFormResult();
			Console.ReadKey();
			
		}

		static void WatchSessions() {
			var worker = new Worker();
			var visits = worker.SessionDetailsProcessing();
			foreach (var visit in visits) {

				try {
					if (visits[visits.IndexOf(visit) - 1] != null)
						if (visits[visits.IndexOf(visit) - 1].Date.Date != visit.Date.Date)
							Console.WriteLine("\n" + visit.Date.Date);
				} catch (Exception ex) {

				}
				Console.WriteLine("\n ================================== \n");
				visit.VisitedPages.ForEach(x => Console.WriteLine(x.Name + " " + x.Time));
			}

			visits.ForEach(x => Console.WriteLine(x));
			Console.ReadKey();
		}

		static void WatchSessionsWithDeep() {
			var worker = new Worker();
			var visits = worker.SessionDetailsProcessing();

			Console.WriteLine("Всего групп " + visits.Count);
			foreach (var sss in visits) {
				Console.WriteLine("===============" + "\nКоличество сессий с " + sss.Sessions[0].VisitedPages.Count
													+ " посещений равно " + sss.Sessions.Count + "\n===============");
				foreach (var session in sss.Sessions) {

					Console.WriteLine("\nНачало сессии");

					session.VisitedPages.ForEach(y => Console.WriteLine(y.Name + "  " + y.Time));
					Console.WriteLine("Конец сессии");

				}
			}
			Console.ReadKey();
		}

		static void WatchLinks() {
			var worker = new Worker();
			var ss = worker.ReturnLinks();

			ss.ForEach(x => Console.WriteLine("тип: " + x.Type + " количество " + x.Number + " источник: " + x.Source + " пункт назначения: " + x.Destination ));
			Console.ReadKey();
		}

		static void WatchFormResult() {

			var worker = new Worker();
			var forms = worker.ReturnFormsResults();

			Console.WriteLine("\n==============" + "Общая форма" + "\n==============");
			foreach(var common in forms.Common) {

				Console.WriteLine(common.Name + "  " + common.City + "   " + common.Degree + "   " + common.Direction + "  " + common.Year + "\n");

			}
			Console.WriteLine("\n==============" + "Функциональная форма" + "\n==============");
			foreach (var func in forms.Functional) {

				Console.WriteLine(func.Average + "  " + func.Limitations + "   " + func.LoadTime + "   " + func.OneThing + "  "
					+ func.Portrai + "  " + func.Reccomend + "  " + func.SomethingNew + "  " + func.TrueData + "\n");

			}

			Console.WriteLine("\n==============" + "UIUX" + "\n==============");
			foreach (var uiux in forms.Uiux) {

				Console.WriteLine(uiux.AverageUI + "  " + uiux.CorrectWork + "   " + uiux.FormPreference + "   " + uiux.Modern + "  "
					+ uiux.PageLocation + "  " + uiux.Trust + "\n");

			}
			Console.WriteLine("\n==============" + "Фри" + "\n==============");
			foreach (var uiux in forms.FreeText) {

				Console.WriteLine(uiux.Text + "\n");

			}

			Console.WriteLine("\n==============" + "Test" + "\n==============");
			foreach (var test in forms.Test) {

				Console.WriteLine(test.Science + "   " + test.Degree + "  " + test.Direction + "   " + test.Organization + "   " + test.Organization + "\n");

			}
		}
	}
}
