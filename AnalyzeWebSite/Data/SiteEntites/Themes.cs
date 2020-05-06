using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyzeWebSite.Data.SiteEntites {
	public class Themes {
		public int Id { get; set; }
		public int DirectionId { get; set; }
		public bool Organization {get; set;}
		public string Text { get; set; }
	}
}
