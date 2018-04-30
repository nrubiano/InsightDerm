using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace InsightDerm.Core.Data.Domain.Migrations
{
    public partial class AddTablePatient : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalCenter_City_CityId",
                table: "MedicalCenter");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MedicalCenter",
                table: "MedicalCenter");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Doctor",
                table: "Doctor");

            migrationBuilder.DropPrimaryKey(
                name: "PK_City",
                table: "City");

            migrationBuilder.RenameTable(
                name: "MedicalCenter",
                newName: "MedicalCenters");

            migrationBuilder.RenameTable(
                name: "Doctor",
                newName: "Doctors");

            migrationBuilder.RenameTable(
                name: "City",
                newName: "Cities");

            migrationBuilder.RenameIndex(
                name: "IX_MedicalCenter_CityId",
                table: "MedicalCenters",
                newName: "IX_MedicalCenters_CityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MedicalCenters",
                table: "MedicalCenters",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Doctors",
                table: "Doctors",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cities",
                table: "Cities",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Address = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: false),
                    BornDate = table.Column<DateTime>(type: "timestamp", maxLength: 255, nullable: false),
                    IdentificatioNumber = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Mail = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false),
                    MobilePhone = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Occupation = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Phone = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalCenters_Cities_CityId",
                table: "MedicalCenters",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MedicalCenters_Cities_CityId",
                table: "MedicalCenters");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MedicalCenters",
                table: "MedicalCenters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Doctors",
                table: "Doctors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cities",
                table: "Cities");

            migrationBuilder.RenameTable(
                name: "MedicalCenters",
                newName: "MedicalCenter");

            migrationBuilder.RenameTable(
                name: "Doctors",
                newName: "Doctor");

            migrationBuilder.RenameTable(
                name: "Cities",
                newName: "City");

            migrationBuilder.RenameIndex(
                name: "IX_MedicalCenters_CityId",
                table: "MedicalCenter",
                newName: "IX_MedicalCenter_CityId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MedicalCenter",
                table: "MedicalCenter",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Doctor",
                table: "Doctor",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_City",
                table: "City",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_MedicalCenter_City_CityId",
                table: "MedicalCenter",
                column: "CityId",
                principalTable: "City",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
