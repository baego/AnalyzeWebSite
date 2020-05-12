using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyzeWebSite.Data.SpyEntities {
	public class FunctionalForms {
		[Key]
		public int Id { get; set; }
		public string UserId { get; set; }
		public Guid SessionId { get; set; }
		public int Average { get; set; }
		public int LoadTime { get; set; }
		public int Limitations { get; set; }
		public string OneThing { get; set; }
		public int SomethingNew { get; set; }
		public int Reccomend { get; set; }
		public int Portrait { get; set; }
		public int TrueData { get; set; }
		public DateTime Date { get; set; }
	}
}
