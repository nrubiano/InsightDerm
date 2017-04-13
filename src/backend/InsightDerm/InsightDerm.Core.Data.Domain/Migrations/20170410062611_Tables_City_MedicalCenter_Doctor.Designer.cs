using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using InsightDerm.Core.Data.Domain.Infrastructure;

namespace InsightDerm.Core.Data.Domain.Migrations
{
    [DbContext(typeof(InsightDermContext))]
    [Migration("20170410062611_Tables_City_MedicalCenter_Doctor")]
    partial class Tables_City_MedicalCenter_Doctor
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("InsightDerm.Core.Data.Domain.Model.City", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .HasMaxLength(150);

                    b.HasKey("Id");

                    b.ToTable("City");
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

            modelBuilder.Entity("InsightDerm.Core.Data.Domain.Model.MedicalCenter", b =>
                {
                    b.HasOne("InsightDerm.Core.Data.Domain.Model.City", "City")
                        .WithMany()
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
