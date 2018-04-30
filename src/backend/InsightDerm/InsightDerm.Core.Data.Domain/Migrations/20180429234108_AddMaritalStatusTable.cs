using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace InsightDerm.Core.Data.Domain.Migrations
{
    public partial class AddMaritalStatusTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "MaritalStatusId",
                table: "Patients",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "MaritalStatuses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Description = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaritalStatuses", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patients_MaritalStatusId",
                table: "Patients",
                column: "MaritalStatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_MaritalStatuses_MaritalStatusId",
                table: "Patients",
                column: "MaritalStatusId",
                principalTable: "MaritalStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_MaritalStatuses_MaritalStatusId",
                table: "Patients");

            migrationBuilder.DropTable(
                name: "MaritalStatuses");

            migrationBuilder.DropIndex(
                name: "IX_Patients_MaritalStatusId",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "MaritalStatusId",
                table: "Patients");
        }
    }
}
