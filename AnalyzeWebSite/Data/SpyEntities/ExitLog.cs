using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyzeWebSite.Data.SpyEntities {
	public class ExitLog {

		[Key]
		public int Id { get; set; }
		public string UserId { get; set; }
		public string Page { get; set; }
		public int Time { get; set; }
		public DateTime Date { get; set; }
	}
}
