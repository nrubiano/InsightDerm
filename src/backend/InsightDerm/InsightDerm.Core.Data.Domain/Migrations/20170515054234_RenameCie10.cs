using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InsightDerm.Core.Data.Domain.Migrations
{
    public partial class RenameCie10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diagnostic_CIE10_CIE10Id",
                table: "Diagnostic");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Diagnostic_CIE10Id_MedicalHistoryId",
                table: "Diagnostic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CIE10",
                table: "CIE10");

            migrationBuilder.RenameTable(
                name: "CIE10",
                newName: "Cie10");

            migrationBuilder.RenameColumn(
                name: "CIE10Id",
                table: "Diagnostic",
                newName: "Cie10Id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Diagnostic_Cie10Id_MedicalHistoryId",
                table: "Diagnostic",
                columns: new[] { "Cie10Id", "MedicalHistoryId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cie10",
                table: "Cie10",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Diagnostic_Cie10_Cie10Id",
                table: "Diagnostic",
                column: "Cie10Id",
                principalTable: "Cie10",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Diagnostic_Cie10_Cie10Id",
                table: "Diagnostic");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Diagnostic_Cie10Id_MedicalHistoryId",
                table: "Diagnostic");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cie10",
                table: "Cie10");

            migrationBuilder.RenameTable(
                name: "Cie10",
                newName: "CIE10");

            migrationBuilder.RenameColumn(
                name: "Cie10Id",
                table: "Diagnostic",
                newName: "CIE10Id");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Diagnostic_CIE10Id_MedicalHistoryId",
                table: "Diagnostic",
                columns: new[] { "CIE10Id", "MedicalHistoryId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CIE10",
                table: "CIE10",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Diagnostic_CIE10_CIE10Id",
                table: "Diagnostic",
                column: "CIE10Id",
                principalTable: "CIE10",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
