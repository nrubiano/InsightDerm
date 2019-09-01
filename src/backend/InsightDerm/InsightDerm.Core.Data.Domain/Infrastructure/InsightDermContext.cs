using InsightDerm.Core.Data.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace InsightDerm.Core.Data.Domain.Infrastructure
{
	public class InsightDermContext : DbContext
	{
        public DbSet<City> Cities { get; set; }

	    public DbSet<Consultation> Consultations { get; set; }

        public DbSet<ConsultationDiagnosis> ConsultationDiagnoses { get; set; }

        public DbSet<ConsultationTreatment> ConsultationTreatments { get; set; }

        public DbSet<DiagnosticImage> DiagnosticImages { get; set; }

        public DbSet<Doctor> Doctors { get; set; }

		public DbSet<MaritalStatus> MaritalStatuses { get; set; }

	    public DbSet<MedicalCenter> MedicalCenters { get; set; }

	    public DbSet<MedicalLaboratory> MedicalLaboratories { get; set; }

	    public DbSet<MedicalLaboratoryType> MedicalLaboratoryTypes { get; set; }

        public DbSet<Patient> Patients { get; set; }
		
		public DbSet<Speciality> Specialities { get; set; }

		public InsightDermContext(DbContextOptions<InsightDermContext> options)
			: base(options)
		{
			
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.EnableAutoHistory(10);
            modelBuilder.Entity<City>().ToTable("Cities");
			modelBuilder.Entity<Doctor>()
				.ToTable("Doctors")
				.HasOne(x => x.Speciality)				
				.WithMany(x => x.Doctors);
			
            modelBuilder.Entity<MedicalCenter>().ToTable("MedicalCenters");
			modelBuilder.Entity<Patient>().ToTable("Patients");
			modelBuilder.Entity<MaritalStatus>().ToTable("MaritalStatuses");

			modelBuilder.Entity<Speciality>()
				.ToTable("Specialities")
				.HasMany(x => x.Doctors)
				.WithOne(x => x.Speciality)
				.OnDelete(DeleteBehavior.SetNull);

		    modelBuilder.Entity<Consultation>()
		        .ToTable("Consultations")
		        .HasMany(x => x.ConsultationDiagnoses)
                .WithOne(x => x.Consultation)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Consultation>()
                .ToTable("Consultations")
                .HasMany(x => x.DiagnosticImages)
                .WithOne(x => x.Consultation)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ConsultationDiagnosis>()
                .ToTable("ConsultationDiagnosis")
                .HasMany(x => x.MedicalLaboratories)
                .WithOne(x => x.ConsultationDiagnosis)
                .OnDelete(DeleteBehavior.Cascade);
        }
	}
}
