using System;
using System.Collections.Generic;
using System.Text;

namespace AnalyzeWebSiteAnalyzeTool.DataClasses {
	public class AttentionFocus {
		public string Name { get; set; }
		public string Focus { get; set; }
	}

	public class PageNameChecker {


		public int[] ReturnPageName(string name) {

			int[] res = new int[2];
			switch (name) {
				case "Главная":
					res[0] = 85;
					res[1] = 142;
					break;
				case "Историческая справка":
					res[0] = 25;
					res[1] = 42;
					break;
				case "История болонской системы":
					res[0] = 134;
					res[1] = 224;
					break;
				case "Болонская система в России":
					res[0] = 103;
					res[1] = 174;
					break;
				case "Процесс защиты диссертации":
					res[0] = 91;
					res[1] = 153;
					break;
				case "Требования к магистерской диссертации":
					res[0] = 15;
					res[1] = 25;
					break;
				case "Требования по оформлению":
					res[0] = 37;
					res[1] = 61;
					break;
				case "Требования к содержанию":
					res[0] = 41;
					res[1] = 69;
					break;
				case "Полезные ссылки":
					res[0] = 10;
					res[1] = 16;
					break;
				case "Об авторе и проекте":
					res[0] = 47;
					res[1] = 78;
					break;
				case "Cookie":
					res[0] = 25;
					res[1] = 42;
					break;
				case "Politics":
					res[0] = 52;
					res[1] = 87;
					break;
			}

			return res;
		}
	}

	enum MinimumTime {
		Main = 85,
		History = 25,
		BolognaWorld = 134,
		BolognaRussia = 103,
		Dissertation = 91,
		Requirements = 15,
		FormalizationRequirements = 37,
		ContentRequirements = 41,
		UsefulLinks = 10,
		Author = 47,
		Cookie = 25,
		Politics = 52
	}

	enum MaximumTime {
		Main = 142,
		History = 42,
		BolognaWorld = 224,
		BolognaRussia = 174,
		Dissertation = 153,
		Requirements = 25,
		FormalizationRequirements = 61,
		ContentRequirements = 69,
		UsefulLinks = 16,
		Author = 78,
		Cookie = 42,
		Politics = 87
	}
}
