using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using InsightDerm.Core.Data.Domain.Infrastructure;

namespace InsightDerm.Core.Data.Domain.Migrations
{
    [DbContext(typeof(InsightDermContext))]
    [Migration("20170515054234_RenameCie10")]
    partial class RenameCie10
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("InsightDerm.Core.Data.Domain.Model.Antecedent", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Antecedent");
                });

            modelBuilder.Entity("InsightDerm.Core.Data.Domain.Model.Cie10", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasMaxLength(150);

                    b.Property<int>("Ref");

                    b.HasKey("Id");

                    b.ToTable("Cie10");
                });

            modelBuilder.Entity("InsightDerm.Core.Data.Domain.Model.City", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasMaxLength(150);

                    b.HasKey("Id");

                    b.ToTable("City");
                });

            modelBuilder.Entity("InsightDerm.Core.Data.Domain.Model.CurrentIllness", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(255);

                    b.Property<Guid>("MedicalHistoryId");

                    b.HasKey("Id");

                    b.HasIndex("MedicalHistoryId");

                    b.ToTable("CurrentIllness");
                });

            modelBuilder.Entity("InsightDerm.Core.Data.Domain.Model.Diagnostic", b =>
                {
                    b.Property<Guid>("MedicalHistoryId");

                    b.Property<Guid>("Cie10Id");

                    b.Property<string>("Description")
                        .HasMaxLength(255);

                    b.Property<DateTime>("DiagnosticDate");

                    b.Property<int>("DoctorId");

                    b.HasKey("MedicalHistoryId", "Cie10Id");

                    b.HasAlternateKey("Cie10Id", "MedicalHistoryId");

                    b.ToTable("Diagnostic");
                });

            modelBuilder.Entity("InsightDerm.Core.Data.Domain.Model.Doctor", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CellPhone")
                        .HasMaxLength(50);

                    b.Property<string>("Email")
                        .HasMaxLength(255);

                    b.Property<string>("Identification")
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .HasMaxLength(255);

                    b.Property<string>("Phone")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Doctor");
                });

            modelBuilder.Entity("InsightDerm.Core.Data.Domain.Model.MedicalCenter", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("CityId");

                    b.Property<string>("Name")
                        .HasMaxLength(150);

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.ToTable("MedicalCenter");
                });

            modelBuilder.Entity("InsightDerm.Core.Data.Domain.Model.MedicalHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("PatientId");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("MedicalHistory");
                });

            modelBuilder.Entity("InsightDerm.Core.Data.Domain.Model.Patient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .HasMaxLength(255);

                    b.Property<DateTime>("BirthDate");

                    b.Property<string>("CellPhone")
                        .HasMaxLength(50);

                    b.Property<string>("Email")
                        .HasMaxLength(255);

                    b.Property<string>("Identification")
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .HasMaxLength(255);

                    b.Property<string>("Occupation")
                        .HasMaxLength(255);

                    b.Property<string>("Phone")
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Patient");
                });

            modelBuilder.Entity("InsightDerm.Core.Data.Domain.Model.PatientAntecedent", b =>
                {
                    b.Property<Guid>("MedicalHistoryId");

                    b.Property<Guid>("AntecedentId");

                    b.Property<DateTime>("AntecedentDate");

                    b.Property<string>("Description")
                        .HasMaxLength(255);

                    b.HasKey("MedicalHistoryId", "AntecedentId");

                    b.HasAlternateKey("AntecedentId", "MedicalHistoryId");

                    b.ToTable("PatientAntecedent");
                });

            modelBuilder.Entity("InsightDerm.Core.Data.Domain.Model.PhysicalExam", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(255);

                    b.Property<DateTime>("ExamDate");

                    b.Property<double>("Height");

                    b.Property<Guid>("MedicalHistoryId");

                    b.Property<double>("Temperature");

                    b.Property<double>("Weight");

                    b.HasKey("Id");

                    b.HasIndex("MedicalHistoryId");

                    b.ToTable("PhysicalExam");
                });

            modelBuilder.Entity("InsightDerm.Core.Data.Domain.Model.Reason", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("MedicalHistoryId");

                    b.Property<DateTime>("ReasonDate");

                    b.HasKey("Id");

                    b.HasIndex("MedicalHistoryId");

                    b.ToTable("Reason");
                });

            modelBuilder.Entity("InsightDerm.Core.Data.Domain.Model.TreatmentPlan", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasMaxLength(255);

                    b.Property<DateTime>("End");

                    b.Property<Guid>("MedicalHistoryId");

                    b.Property<DateTime>("Start");

                    b.HasKey("Id");

                    b.HasIndex("MedicalHistoryId");

                    b.ToTable("TreatmentPlan");
                });

            modelBuilder.Entity("Microsoft.EntityFrameworkCore.Internal.AutoHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Changed")
                        .HasMaxLength(2048);

                    b.Property<DateTime>("Created");

                    b.Property<int>("Kind");

                    b.Property<string>("RowId")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("TableName")
                        .IsRequired()
                        .HasMaxLength(128);

                    b.HasKey("Id");

                    b.ToTable("AutoHistory");
                });

            modelBuilder.Entity("InsightDerm.Core.Data.Domain.Model.CurrentIllness", b =>
                {
                    b.HasOne("InsightDerm.Core.Data.Domain.Model.MedicalHistory", "MedicalHistory")
                        .WithMany()
                        .HasForeignKey("MedicalHistoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InsightDerm.Core.Data.Domain.Model.Diagnostic", b =>
                {
                    b.HasOne("InsightDerm.Core.Data.Domain.Model.Cie10", "Cie10")
                        .WithMany()
                        .HasForeignKey("Cie10Id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InsightDerm.Core.Data.Domain.Model.MedicalHistory", "MedicalHistory")
                        .WithMany()
                        .HasForeignKey("MedicalHistoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InsightDerm.Core.Data.Domain.Model.MedicalCenter", b =>
                {
                    b.HasOne("InsightDerm.Core.Data.Domain.Model.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InsightDerm.Core.Data.Domain.Model.MedicalHistory", b =>
                {
                    b.HasOne("InsightDerm.Core.Data.Domain.Model.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InsightDerm.Core.Data.Domain.Model.PatientAntecedent", b =>
                {
                    b.HasOne("InsightDerm.Core.Data.Domain.Model.Antecedent", "Antecedent")
                        .WithMany()
                        .HasForeignKey("AntecedentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InsightDerm.Core.Data.Domain.Model.MedicalHistory", "MedicalHistory")
                        .WithMany()
                        .HasForeignKey("MedicalHistoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InsightDerm.Core.Data.Domain.Model.PhysicalExam", b =>
                {
                    b.HasOne("InsightDerm.Core.Data.Domain.Model.MedicalHistory", "MedicalHistory")
                        .WithMany()
                        .HasForeignKey("MedicalHistoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InsightDerm.Core.Data.Domain.Model.Reason", b =>
                {
                    b.HasOne("InsightDerm.Core.Data.Domain.Model.MedicalHistory", "MedicalHistory")
                        .WithMany()
                        .HasForeignKey("MedicalHistoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InsightDerm.Core.Data.Domain.Model.TreatmentPlan", b =>
                {
                    b.HasOne("InsightDerm.Core.Data.Domain.Model.MedicalHistory", "MedicalHistory")
                        .WithMany()
                        .HasForeignKey("MedicalHistoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
