using System;
using System.Collections.Generic;
using System.Text;

namespace AnalyzeDataWorker.DataClasses {

	public class DayVisits {

		public int Number { get; set; }
		public List<string> UserIds { get; set; }
		public DateTime Date { get; set; }

		public DayVisits() {

			UserIds = new List<string>();
		}

	}

}
