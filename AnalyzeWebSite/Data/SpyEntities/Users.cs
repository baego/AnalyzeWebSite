using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyzeWebSite.Data.SpyEntities {
	public class Users {
		[Key]
		public string IP { get; set; }
		public DateTime CreateDate { get; set; }
	}
}
