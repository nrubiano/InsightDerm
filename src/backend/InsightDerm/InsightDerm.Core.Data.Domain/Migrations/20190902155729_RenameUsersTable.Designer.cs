﻿// <auto-generated />
using InsightDerm.Core.Data.Domain.Infrastructure;
using InsightDerm.Core.Data.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace InsightDerm.Core.Data.Domain.Migrations
{
    [DbContext(typeof(InsightDermContext))]
    [Migration("20190902155729_RenameUsersTable")]
    partial class RenameUsersTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("InsightDerm.Core.Data.Domain.Model.City", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasMaxLength(150);

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("InsightDerm.Core.Data.Domain.Model.Consultation", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("MedicalBackground")
                        .IsRequired()
                        .HasMaxLength(4000);

                    b.Property<Guid>("PatientId");

                    b.Property<string>("PhysicalExam")
                        .HasMaxLength(1000);

                    b.Property<string>("Reason")
                        .IsRequired()
                        .HasMaxLength(1000);

                    b.Property<Guid>("RequestedById");

                    b.Property<int>("Status");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.HasIndex("RequestedById");

                    b.ToTable("Consultations");
                });

            modelBuilder.Entity("InsightDerm.Core.Data.Domain.Model.ConsultationDiagnosis", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ById");

                    b.Property<Guid>("ConsultationId");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.Property<DateTime>("UpdateDate");

                    b.HasKey("Id");

                    b.HasIndex("ById");

                    b.HasIndex("ConsultationId");

                    b.ToTable("ConsultationDiagnosis");
                });

            modelBuilder.Entity("InsightDerm.Core.Data.Domain.Model.ConsultationTreatment", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ById");

                    b.Property<Guid>("ConsultationDiagnosisId");

                    b.Property<DateTime>("CreationDate");

                    b.Property<string>("Description")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ById");

                    b.HasIndex("ConsultationDiagnosisId")
                        .IsUnique();

                    b.ToTable("ConsultationTreatments");
                });

            modelBuilder.Entity("InsightDerm.Core.Data.Domain.Model.DiagnosticImage", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ConsultationId");

                    b.Property<string>("Image")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ConsultationId");

                    b.ToTable("DiagnosticImages");
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

                    b.Property<Guid>("MedicalCenterId");

                    b.Property<string>("Name")
                        .HasMaxLength(255);

                    b.Property<string>("Phone")
                        .HasMaxLength(50);

                    b.Property<Guid>("SpecialityId");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("MedicalCenterId");

                    b.HasIndex("SpecialityId");

                    b.HasIndex("UserId");

                    b.ToTable("Doctors");
                });

            modelBuilder.Entity("InsightDerm.Core.Data.Domain.Model.MaritalStatus", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("MaritalStatuses");
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

                    b.ToTable("MedicalCenters");
                });

            modelBuilder.Entity("InsightDerm.Core.Data.Domain.Model.MedicalLaboratory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("ConsultationDiagnosisId");

                    b.Property<Guid>("RequestedById");

                    b.Property<DateTime>("RequestedDate");

                    b.Property<Guid>("TypeId");

                    b.HasKey("Id");

                    b.HasIndex("ConsultationDiagnosisId");

                    b.HasIndex("RequestedById");

                    b.HasIndex("TypeId");

                    b.ToTable("MedicalLaboratories");
                });

            modelBuilder.Entity("InsightDerm.Core.Data.Domain.Model.MedicalLaboratoryType", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Notes")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("MedicalLaboratoryTypes");
                });

            modelBuilder.Entity("InsightDerm.Core.Data.Domain.Model.Patient", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<DateTime>("BornDate")
                        .HasMaxLength(255);

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasMaxLength(12);

                    b.Property<string>("IdentificationNumber")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("IdentificationType")
                        .IsRequired()
                        .HasMaxLength(12);

                    b.Property<string>("Mail")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<Guid>("MaritalStatusId");

                    b.Property<string>("MobilePhone")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("Occupation")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("Phone")
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("MaritalStatusId");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("InsightDerm.Core.Data.Domain.Model.Speciality", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("Specialities");
                });

            modelBuilder.Entity("InsightDerm.Core.Data.Domain.Model.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Password");

                    b.Property<string>("Role");

                    b.Property<string>("Token");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Microsoft.EntityFrameworkCore.AutoHistory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Changed")
                        .HasMaxLength(10);

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

            modelBuilder.Entity("InsightDerm.Core.Data.Domain.Model.Consultation", b =>
                {
                    b.HasOne("InsightDerm.Core.Data.Domain.Model.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InsightDerm.Core.Data.Domain.Model.Doctor", "RequestedBy")
                        .WithMany()
                        .HasForeignKey("RequestedById")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InsightDerm.Core.Data.Domain.Model.ConsultationDiagnosis", b =>
                {
                    b.HasOne("InsightDerm.Core.Data.Domain.Model.Doctor", "By")
                        .WithMany()
                        .HasForeignKey("ById")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InsightDerm.Core.Data.Domain.Model.Consultation", "Consultation")
                        .WithMany("ConsultationDiagnoses")
                        .HasForeignKey("ConsultationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InsightDerm.Core.Data.Domain.Model.ConsultationTreatment", b =>
                {
                    b.HasOne("InsightDerm.Core.Data.Domain.Model.Doctor", "By")
                        .WithMany()
                        .HasForeignKey("ById")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InsightDerm.Core.Data.Domain.Model.ConsultationDiagnosis", "Diagnosis")
                        .WithOne("Treatment")
                        .HasForeignKey("InsightDerm.Core.Data.Domain.Model.ConsultationTreatment", "ConsultationDiagnosisId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InsightDerm.Core.Data.Domain.Model.DiagnosticImage", b =>
                {
                    b.HasOne("InsightDerm.Core.Data.Domain.Model.Consultation", "Consultation")
                        .WithMany("DiagnosticImages")
                        .HasForeignKey("ConsultationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InsightDerm.Core.Data.Domain.Model.Doctor", b =>
                {
                    b.HasOne("InsightDerm.Core.Data.Domain.Model.MedicalCenter", "MedicalCenter")
                        .WithMany()
                        .HasForeignKey("MedicalCenterId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InsightDerm.Core.Data.Domain.Model.Speciality", "Speciality")
                        .WithMany("Doctors")
                        .HasForeignKey("SpecialityId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.HasOne("InsightDerm.Core.Data.Domain.Model.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InsightDerm.Core.Data.Domain.Model.MedicalCenter", b =>
                {
                    b.HasOne("InsightDerm.Core.Data.Domain.Model.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InsightDerm.Core.Data.Domain.Model.MedicalLaboratory", b =>
                {
                    b.HasOne("InsightDerm.Core.Data.Domain.Model.ConsultationDiagnosis", "ConsultationDiagnosis")
                        .WithMany("MedicalLaboratories")
                        .HasForeignKey("ConsultationDiagnosisId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InsightDerm.Core.Data.Domain.Model.Doctor", "RequestedBy")
                        .WithMany()
                        .HasForeignKey("RequestedById")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InsightDerm.Core.Data.Domain.Model.MedicalLaboratoryType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InsightDerm.Core.Data.Domain.Model.Patient", b =>
                {
                    b.HasOne("InsightDerm.Core.Data.Domain.Model.MaritalStatus", "MaritalStatus")
                        .WithMany()
                        .HasForeignKey("MaritalStatusId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
