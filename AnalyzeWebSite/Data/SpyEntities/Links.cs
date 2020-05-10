using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyzeWebSite.Data.SpyEntities {
	public class Links {

		[Key]
		public int Id { get; set; }
		public string UserId { get; set; }
		public Guid? SessionId { get; set; }
		public string Source { get; set; }
		public string Destination { get; set; }
		// типы:
		// 0 - navigation 
		// 1 - external
		// 2 - source
		public int Type { get; set; }
		public DateTime Date { get; set; }
		public string Page { get; set; }
	}
}
