using InsightDerm.Core.Data.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace InsightDerm.Core.Data.Domain.Infrastructure
{
	public class InsightDermContext : DbContext
	{
		public DbSet<Doctor> Doctors { get; set; }

        public DbSet<City> Cities { get; set; }
		
		public DbSet<MedicalCenter> MedicalCenters { get; set; }
		
		public DbSet<MaritalStatus> MaritalStatuses { get; set; }
		
		public DbSet<Patient> Patients { get; set; }

		public InsightDermContext(DbContextOptions<InsightDermContext> options)
			: base(options)
		{
			
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
		    modelBuilder.EnableAutoHistory();
            modelBuilder.Entity<City>().ToTable("Cities");
            modelBuilder.Entity<Doctor>().ToTable("Doctors");
            modelBuilder.Entity<MedicalCenter>().ToTable("MedicalCenters");
			modelBuilder.Entity<Patient>().ToTable("Patients");
			modelBuilder.Entity<MaritalStatus>().ToTable("MaritalStatuses");
        }
	}
}
