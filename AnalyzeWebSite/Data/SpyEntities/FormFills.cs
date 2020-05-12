using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyzeWebSite.Data.SpyEntities {
	public class FormFills {
		[Key]
		public int Id { get; set; }
		public string UserId { get; set; }
		public DateTime Date { get; set; }
		public string FormName { get; set; }
		public Guid SessionId { get; set; }
	}
}
