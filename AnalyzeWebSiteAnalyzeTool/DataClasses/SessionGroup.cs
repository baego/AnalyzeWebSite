using System;
using System.Collections.Generic;
using System.Text;

namespace AnalyzeWebSiteAnalyzeTool.DataClasses {
	public class SessionGroup {

		public List<SessionDetails> Sessions { get; set; }

		public SessionGroup() {

			Sessions = new List<SessionDetails>();
		}
	}
}
