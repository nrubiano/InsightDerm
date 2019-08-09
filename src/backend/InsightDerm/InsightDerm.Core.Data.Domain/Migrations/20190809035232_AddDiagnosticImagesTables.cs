using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace InsightDerm.Core.Data.Domain.Migrations
{
    public partial class AddDiagnosticImagesTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalLaboratories_Consultations_ConsultationId",
                table: "MedicalLaboratories");

            migrationBuilder.CreateTable(
                name: "DiagnosticImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ConsultationId = table.Column<Guid>(nullable: false),
                    Image = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiagnosticImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiagnosticImages_Consultations_ConsultationId",
                        column: x => x.ConsultationId,
                        principalTable: "Consultations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiagnosticImages_ConsultationId",
                table: "DiagnosticImages",
                column: "ConsultationId");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalLaboratories_Consultations_ConsultationId",
                table: "MedicalLaboratories",
                column: "ConsultationId",
                principalTable: "Consultations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalLaboratories_Consultations_ConsultationId",
                table: "MedicalLaboratories");

            migrationBuilder.DropTable(
                name: "DiagnosticImages");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalLaboratories_Consultations_ConsultationId",
                table: "MedicalLaboratories",
                column: "ConsultationId",
                principalTable: "Consultations",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
