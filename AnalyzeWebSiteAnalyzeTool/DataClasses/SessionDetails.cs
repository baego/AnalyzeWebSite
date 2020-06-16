using System;
using System.Collections.Generic;
using System.Text;

namespace AnalyzeWebSiteAnalyzeTool.DataClasses {
	public class SessionDetails {

		public Guid SessionId { get; set; }
		public DateTime Date { get; set; }
		public string UserId { get; set; }
		public List<VisitedPage> VisitedPages { get; set; }
		
		public SessionDetails() {
			VisitedPages = new List<VisitedPage>();
		}
	}

	public class VisitedPage {

		public int Time { get; set; }
		public DateTime Date { get; set; }
		public string Name { get; set; }
	}
	
}
