using InsightDerm.Core.Data.Domain.Model;
using Microsoft.EntityFrameworkCore;

namespace InsightDerm.Core.Data.Domain.Infrastructure
{
    public class InsightDermContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<MedicalCenter> MedicalCenters { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<MedicalHistory> MedicalHistories { get; set; }
        public DbSet<Antecedent> Antecedents { get; set; }
        public DbSet<PatientAntecedent> PatientAntecedents { get; set; }
        public DbSet<CurrentIllness> CurrentIllnesses { get; set; }
        public DbSet<TreatmentPlan> TreatmentPlans { get; set; }
        public DbSet<PhysicalExam> PhysicalExams { get; set; }
        public DbSet<Reason> Reasons { get; set; }
        public DbSet<Cie10> Cie10 { get; set; }
        public DbSet<Diagnostic> Diagnostics { get; set; }

        public InsightDermContext(DbContextOptions<InsightDermContext> options)
			: base(options)
		{ }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
		    modelBuilder.EnableAutoHistory();
            modelBuilder.Entity<City>().ToTable("City");
            modelBuilder.Entity<Doctor>().ToTable("Doctor");
            modelBuilder.Entity<MedicalCenter>().ToTable("MedicalCenter");
            modelBuilder.Entity<Patient>().ToTable("Patient");
            modelBuilder.Entity<MedicalHistory>().ToTable("MedicalHistory");
            modelBuilder.Entity<Antecedent>().ToTable("Antecedent");
            modelBuilder.Entity<PatientAntecedent>()
                .HasKey(x => new { x.MedicalHistoryId, x.AntecedentId });
            modelBuilder.Entity<PatientAntecedent>().ToTable("PatientAntecedent");
            modelBuilder.Entity<CurrentIllness>().ToTable("CurrentIllness");
            modelBuilder.Entity<TreatmentPlan>().ToTable("TreatmentPlan");
            modelBuilder.Entity<PhysicalExam>().ToTable("PhysicalExam");
            modelBuilder.Entity<Reason>().ToTable("Reason");
            modelBuilder.Entity<Cie10>().ToTable("Cie10");
            modelBuilder.Entity<Diagnostic>()
                .HasKey(x => new { x.MedicalHistoryId, x.Cie10Id });
            modelBuilder.Entity<Diagnostic>().ToTable("Diagnostic");
        }
	}
}
