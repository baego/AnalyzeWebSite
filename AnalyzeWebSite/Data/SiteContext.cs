using AnalyzeWebSite.Data.SiteEntites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyzeWebSite.Data {
	public class SiteContext : DbContext {

		public DbSet<Directions> Directions { get; set; }
		public DbSet<ErrorLog> ErrorLog { get; set; }
		public DbSet<Sciences> Sciences { get; set; }
		public DbSet<Degrees> Degrees { get; set; }
		public DbSet<Themes> Themes { get; set; }

		public SiteContext() {
			Database.EnsureCreated();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
		}
	}
}
