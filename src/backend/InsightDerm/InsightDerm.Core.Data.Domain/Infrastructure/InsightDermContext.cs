﻿using InsightDerm.Core.Data.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace InsightDerm.Core.Data.Domain.Infrastructure
{
	public class InsightDermContext : DbContext
	{
		public DbSet<Doctor> Doctors { get; set; }

        public DbSet<City> Cities { get; set; }

        public InsightDermContext(DbContextOptions<InsightDermContext> options)
			: base(options)
		{ }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
		    modelBuilder.EnableAutoHistory();
            modelBuilder.Entity<City>().ToTable("City");
            modelBuilder.Entity<Doctor>().ToTable("Doctor");
            modelBuilder.Entity<MedicalCenter>().ToTable("MedicalCenter");
        }
	}
}
