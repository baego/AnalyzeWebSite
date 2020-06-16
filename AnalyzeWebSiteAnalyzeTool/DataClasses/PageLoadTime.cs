using System;
using System.Collections.Generic;
using System.Text;

namespace AnalyzeWebSiteAnalyzeTool.DataClasses {

	public class PageLoadTime {

		public string PageName { get; set; }
		public int Time { get; set; }
		public int AverageLoads { get; set; }
	}
}
