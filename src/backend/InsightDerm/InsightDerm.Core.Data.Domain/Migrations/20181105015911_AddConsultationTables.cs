using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace InsightDerm.Core.Data.Domain.Migrations
{
    public partial class AddConsultationTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Consultations",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    MedicalBackground = table.Column<string>(maxLength: 4000, nullable: false),
                    PatientId = table.Column<Guid>(nullable: false),
                    PhysicalExam = table.Column<string>(maxLength: 1000, nullable: true),
                    Reason = table.Column<string>(maxLength: 1000, nullable: false),
                    RequestedById = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Consultations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Consultations_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Consultations_Doctors_RequestedById",
                        column: x => x.RequestedById,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicalLaboratoryTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Notes = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalLaboratoryTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicalLaboratories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ConsultationId = table.Column<Guid>(nullable: false),
                    RequestedById = table.Column<Guid>(nullable: false),
                    RequestedDate = table.Column<DateTime>(nullable: false),
                    TypeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalLaboratories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalLaboratories_Consultations_ConsultationId",
                        column: x => x.ConsultationId,
                        principalTable: "Consultations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_MedicalLaboratories_Doctors_RequestedById",
                        column: x => x.RequestedById,
                        principalTable: "Doctors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicalLaboratories_MedicalLaboratoryTypes_TypeId",
                        column: x => x.TypeId,
                        principalTable: "MedicalLaboratoryTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Consultations_PatientId",
                table: "Consultations",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Consultations_RequestedById",
                table: "Consultations",
                column: "RequestedById");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalLaboratories_ConsultationId",
                table: "MedicalLaboratories",
                column: "ConsultationId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalLaboratories_RequestedById",
                table: "MedicalLaboratories",
                column: "RequestedById");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalLaboratories_TypeId",
                table: "MedicalLaboratories",
                column: "TypeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalLaboratories");

            migrationBuilder.DropTable(
                name: "Consultations");

            migrationBuilder.DropTable(
                name: "MedicalLaboratoryTypes");
        }
    }
}
