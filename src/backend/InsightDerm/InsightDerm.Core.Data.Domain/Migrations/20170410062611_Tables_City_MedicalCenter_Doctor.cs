using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InsightDerm.Core.Data.Domain.Migrations
{
    public partial class Tables_City_MedicalCenter_Doctor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Doctor",
                maxLength: 255,
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CellPhone",
                table: "Doctor",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Doctor",
                maxLength: 255,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Identification",
                table: "Doctor",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Doctor",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicalCenter",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CityId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 150, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalCenter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalCenter_City_CityId",
                        column: x => x.CityId,
                        principalTable: "City",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalCenter_CityId",
                table: "MedicalCenter",
                column: "CityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedicalCenter");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropColumn(
                name: "CellPhone",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "Identification",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Doctor");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Doctor",
                nullable: true,
                oldClrType: typeof(string),
                oldMaxLength: 255,
                oldNullable: true);
        }
    }
}
