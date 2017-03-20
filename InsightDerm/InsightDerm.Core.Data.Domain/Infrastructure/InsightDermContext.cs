using System;
using InsightDerm.Core.Data.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace InsightDerm.Core.Data.Domain.Infrastructure
{
	public class InsightDermContext : DbContext
	{
		public DbSet<Doctor> Doctors { get; set; }

		public InsightDermContext(DbContextOptions<InsightDermContext> options)
			: base(options)
		{ }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doctor>().ToTable("Doctor");
        }
	}
}
