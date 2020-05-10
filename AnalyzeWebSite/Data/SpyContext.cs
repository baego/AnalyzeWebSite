﻿using AnalyzeWebSite.Data.SpyEntities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AnalyzeWebSite.Data {
	public class SpyContext : DbContext {

		public DbSet<Pics> Pics { get; set; }
		public DbSet<Users> Users { get; set; }
		public DbSet<Links> Links { get; set; }
		public DbSet<ExitLog> ExitLog { get; set; }
		public DbSet<Sessions> Sessions { get; set; }
		public DbSet<Browsers> Browsers { get; set; }
		public DbSet<FocusLost> FocusLost { get; set; }
		public DbSet<CookieAgree> CookieAgree { get; set; }
		public DbSet<Geolocations> Geolocations { get; set; }
		public DbSet<PageLoadTimeLog> PageLoadTimeLog { get; set; }

		public SpyContext() {
			Database.EnsureCreated();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
		}
	}
}
