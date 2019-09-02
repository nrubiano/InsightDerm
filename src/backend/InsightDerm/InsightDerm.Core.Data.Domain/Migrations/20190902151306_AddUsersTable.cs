using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace InsightDerm.Core.Data.Domain.Migrations
{
    public partial class AddUsersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "By",
                table: "ConsultationTreatments",
                newName: "ById");

            migrationBuilder.RenameColumn(
                name: "By",
                table: "ConsultationDiagnosis",
                newName: "ById");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Doctors",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Password = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    Token = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_UserId",
                table: "Doctors",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultationTreatments_ById",
                table: "ConsultationTreatments",
                column: "ById");

            migrationBuilder.CreateIndex(
                name: "IX_ConsultationDiagnosis_ById",
                table: "ConsultationDiagnosis",
                column: "ById");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsultationDiagnosis_Doctors_ById",
                table: "ConsultationDiagnosis",
                column: "ById",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ConsultationTreatments_Doctors_ById",
                table: "ConsultationTreatments",
                column: "ById",
                principalTable: "Doctors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_User_UserId",
                table: "Doctors",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsultationDiagnosis_Doctors_ById",
                table: "ConsultationDiagnosis");

            migrationBuilder.DropForeignKey(
                name: "FK_ConsultationTreatments_Doctors_ById",
                table: "ConsultationTreatments");

            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_User_UserId",
                table: "Doctors");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_Doctors_UserId",
                table: "Doctors");

            migrationBuilder.DropIndex(
                name: "IX_ConsultationTreatments_ById",
                table: "ConsultationTreatments");

            migrationBuilder.DropIndex(
                name: "IX_ConsultationDiagnosis_ById",
                table: "ConsultationDiagnosis");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Doctors");

            migrationBuilder.RenameColumn(
                name: "ById",
                table: "ConsultationTreatments",
                newName: "By");

            migrationBuilder.RenameColumn(
                name: "ById",
                table: "ConsultationDiagnosis",
                newName: "By");
        }
    }
}
