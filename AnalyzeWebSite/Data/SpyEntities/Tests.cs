using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyzeWebSite.Data.SpyEntities {
	public class Tests {
		[Key]
		public int Id { get; set; }
		public string UserId { get; set; }
		public Guid SessionId { get; set; }
		public int Degree { get; set; }
		public int Science { get; set; }
		public int Direction { get; set; }
		public string Organization { get; set; }
		public DateTime Date { get; set; }
	}
}
