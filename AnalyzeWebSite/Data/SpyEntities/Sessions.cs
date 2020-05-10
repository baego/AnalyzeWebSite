using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyzeWebSite.Data.SpyEntities {
	public class Sessions {

		[Key]
		public Guid Id { get; set; }
		public string UserId { get; set; }
		public DateTime Date { get; set; }
	}
}
