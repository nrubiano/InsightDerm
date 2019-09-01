using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace InsightDerm.Core.Data.Domain.Migrations
{
    public partial class AddDiagnosticsAndTreatmentsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalLaboratories_Consultations_ConsultationId",
                table: "MedicalLaboratories");

            migrationBuilder.RenameColumn(
                name: "ConsultationId",
                table: "MedicalLaboratories",
                newName: "ConsultationDiagnosisId");

            migrationBuilder.RenameIndex(
                name: "IX_MedicalLaboratories_ConsultationId",
                table: "MedicalLaboratories",
                newName: "IX_MedicalLaboratories_ConsultationDiagnosisId");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Consultations",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ConsultationDiagnosis",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    By = table.Column<Guid>(nullable: false),
                    ConsultationId = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    UpdateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultationDiagnosis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsultationDiagnosis_Consultations_ConsultationId",
                        column: x => x.ConsultationId,
                        principalTable: "Consultations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConsultationTreatments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    By = table.Column<Guid>(nullable: false),
                    ConsultationDiagnosisId = table.Column<Guid>(nullable: false),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsultationTreatments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConsultationTreatments_ConsultationDiagnosis_ConsultationDiagnosisId",
                        column: x => x.ConsultationDiagnosisId,
                        principalTable: "ConsultationDiagnosis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsultationDiagnosis_ConsultationId",
                table: "ConsultationDiagnosis",
                column: "ConsultationId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultationTreatments_ConsultationDiagnosisId",
                table: "ConsultationTreatments",
                column: "ConsultationDiagnosisId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalLaboratories_ConsultationDiagnosis_ConsultationDiagnosisId",
                table: "MedicalLaboratories",
                column: "ConsultationDiagnosisId",
                principalTable: "ConsultationDiagnosis",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalLaboratories_ConsultationDiagnosis_ConsultationDiagnosisId",
                table: "MedicalLaboratories");

            migrationBuilder.DropTable(
                name: "ConsultationTreatments");

            migrationBuilder.DropTable(
                name: "ConsultationDiagnosis");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Consultations");

            migrationBuilder.RenameColumn(
                name: "ConsultationDiagnosisId",
                table: "MedicalLaboratories",
                newName: "ConsultationId");

            migrationBuilder.RenameIndex(
                name: "IX_MedicalLaboratories_ConsultationDiagnosisId",
                table: "MedicalLaboratories",
                newName: "IX_MedicalLaboratories_ConsultationId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalLaboratories_Consultations_ConsultationId",
                table: "MedicalLaboratories",
                column: "ConsultationId",
                principalTable: "Consultations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
