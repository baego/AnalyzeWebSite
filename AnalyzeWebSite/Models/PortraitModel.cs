using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyzeWebSite.Models {
	public class PortraitModel {

		//если модель пока нельзя подготовить
		public bool IsModelReady { get; set; }
		
		//общие данные
		public string Ip { get; set; }
		public string Browser { get; set; }
		public List<string> PreviousBrowsers { get; set; }
		public string Geolocation { get; set; }
		public int SpendedTime { get; set; }
		public List<HistoryClass> History { get; set; }
		public double? AverageLoadTime { get; set; }
		public int FocusLostCount { get; set; }
		public int Focus { get; set; }
		public int PicsViewed { get; set; }
		public int ExternalLinks { get; set; }
		public int PreviousSessions { get; set; }


		//данные с форм

		public PortraitModel() {

			History = new List<HistoryClass>();
			PreviousBrowsers = new List<string>();
		}
	}

	public class HistoryClass {

		public string Page { get; set; }
		public int Time { get; set; }
	}
}
