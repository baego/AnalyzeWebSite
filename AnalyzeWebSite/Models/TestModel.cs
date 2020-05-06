using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyzeWebSite.Models {

	public class TestModel {

		public int Id { get; set; }
		public int? DegreeId { get; set; }
		public int? DirectionId { get; set; }
		public int? ScienceId { get; set; }
		public string City { get; set; }
		public string Area { get; set; }
		public bool AnotherCountry { get; set; }
	}
}
