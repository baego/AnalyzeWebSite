using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyzeWebSite.Data {
	public class SpyContext : DbContext {

		//public DbSet<test> tests { get; set; }

		public SpyContext() {
			Database.EnsureCreated();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
			//sssssss
		}
	}
}
