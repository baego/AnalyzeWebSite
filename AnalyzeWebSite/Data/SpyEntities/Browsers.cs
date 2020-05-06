using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyzeWebSite.Data.SpyEntities {
	public class Browsers {

		[Key]
		public int Id { get; set; }
		public string UserId { get; set; }
		public string Browser { get; set; }
		public DateTime Date { get; set; }
	}
}
