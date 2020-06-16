using System;
using System.Collections.Generic;
using System.Text;

namespace AnalyzeWebSiteAnalyzeTool.DataClasses {

  public class CommonResult {

		public DateTime Date { get; set; }
		public Guid SessionId { get; set; }
		public string Name { get; set; }
		public string City { get; set; }
		public int Year { get; set; }
		public string Degree { get; set; }
		public string Direction { get; set; }
	}

	public class FreeTextResult {

		public string UserId { get; set; }
		public Guid SessionId { get; set; }
		public DateTime Date { get; set; }
		public string Text { get; set; }
	}

	public class FunctionalResult {

		public Guid SessionId { get; set; }
		public string UserId { get; set; }
		public DateTime Date { get; set; }
		public int Average { get; set; }
		public int LoadTime { get; set; }
		public int Limitations { get; set; }
		public string OneThing { get; set; }
		public int SomethingNew { get; set; }
		public int Reccomend { get; set; }
		public int Portrai { get; set; }
	  public int TrueData { get; set; }
	}

	public class UiuxResult {

		public Guid SessionId { get; set; }
		public string UserId { get; set; }
		public DateTime Date { get; set; }
		public int AverageUI { get; set; }
		public int CorrectWork { get; set; }
		public int Trust { get; set; }
		public int Modern { get; set; }
		public int PageLocation { get; set; }
		public int FormPreference { get; set; }
	}

	public class TestResult {

		public Guid SessionId { get; set; }
		public string UserId { get; set; }
		public DateTime Date { get; set; }
		public string Degree { get; set; }
		public string Science { get; set; }
		public string Direction { get; set; }
		public string Organization { get; set; }
	}

	public class FormsResult {

		public List<TestResult> Test { get; set; }
		public List<UiuxResult> Uiux { get; set; }
		public List<FunctionalResult> Functional { get; set; }
		public List<FreeTextResult> FreeText { get; set; }
		public List<CommonResult> Common { get; set; }
	}
}
