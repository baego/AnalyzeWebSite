using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyzeWebSite.Data.SpyEntities {
	public class UiuxForms {
		[Key]
		public int Id { get; set; }
		public string UserId { get; set; }
		public Guid SessionId { get; set; }
		public int AverageUI { get; set; }
		public int CorrectWork { get; set; }
		public int Trust { get; set; }
		public int Modern { get; set; }
		public int PageLocation { get; set; }
		public int FormPreference { get; set; }
		public DateTime Date { get; set; }


	}
}
