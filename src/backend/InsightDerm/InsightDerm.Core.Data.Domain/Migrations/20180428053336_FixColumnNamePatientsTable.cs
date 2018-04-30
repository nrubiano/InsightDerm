using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace InsightDerm.Core.Data.Domain.Migrations
{
    public partial class FixColumnNamePatientsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentificatioNumber",
                table: "Patients");

            migrationBuilder.AddColumn<string>(
                name: "IdentificationNumber",
                table: "Patients",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IdentificationNumber",
                table: "Patients");

            migrationBuilder.AddColumn<string>(
                name: "IdentificatioNumber",
                table: "Patients",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }
    }
}
