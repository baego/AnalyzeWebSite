using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyzeWebSite.Data.SiteEntites {
	public class ErrorLog {

		[Key]
		public int Id { get; set; }
		public DateTime Date { get; set; }
		public string Message { get; set; }
		public string Trace { get; set; }
		public string Source { get; set; }
	}
}
