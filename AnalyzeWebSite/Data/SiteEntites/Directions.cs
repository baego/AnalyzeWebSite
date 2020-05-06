using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyzeWebSite.Data.SiteEntites {
	public class Directions {

		public int Id { get; set; }
		public int DirectionId { get; set; }
		public int ScienceId { get; set; }
		public string Name { get; set; }
	}
}
