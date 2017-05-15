using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InsightDerm.Core.Data.Domain.Migrations
{
    public partial class Tables_AllMedicalHistoryArea : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Antecedent",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Antecedent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicalHistory",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PatientId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalHistory_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CurrentIllness",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    MedicalHistoryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CurrentIllness", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CurrentIllness_MedicalHistory_MedicalHistoryId",
                        column: x => x.MedicalHistoryId,
                        principalTable: "MedicalHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientAntecedent",
                columns: table => new
                {
                    MedicalHistoryId = table.Column<Guid>(nullable: false),
                    AntecedentId = table.Column<Guid>(nullable: false),
                    AntecedentDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientAntecedent", x => new { x.MedicalHistoryId, x.AntecedentId });
                    table.UniqueConstraint("AK_PatientAntecedent_AntecedentId_MedicalHistoryId", x => new { x.AntecedentId, x.MedicalHistoryId });
                    table.ForeignKey(
                        name: "FK_PatientAntecedent_Antecedent_AntecedentId",
                        column: x => x.AntecedentId,
                        principalTable: "Antecedent",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientAntecedent_MedicalHistory_MedicalHistoryId",
                        column: x => x.MedicalHistoryId,
                        principalTable: "MedicalHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PhysicalExam",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    ExamDate = table.Column<DateTime>(nullable: false),
                    Height = table.Column<double>(nullable: false),
                    MedicalHistoryId = table.Column<Guid>(nullable: false),
                    Temperature = table.Column<double>(nullable: false),
                    Weight = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhysicalExam", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhysicalExam_MedicalHistory_MedicalHistoryId",
                        column: x => x.MedicalHistoryId,
                        principalTable: "MedicalHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reason",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MedicalHistoryId = table.Column<Guid>(nullable: false),
                    ReasonDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reason", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reason_MedicalHistory_MedicalHistoryId",
                        column: x => x.MedicalHistoryId,
                        principalTable: "MedicalHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TreatmentPlan",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(maxLength: 255, nullable: true),
                    End = table.Column<DateTime>(nullable: false),
                    MedicalHistoryId = table.Column<Guid>(nullable: false),
                    Start = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentPlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TreatmentPlan_MedicalHistory_MedicalHistoryId",
                        column: x => x.MedicalHistoryId,
                        principalTable: "MedicalHistory",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CurrentIllness_MedicalHistoryId",
                table: "CurrentIllness",
                column: "MedicalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalHistory_PatientId",
                table: "MedicalHistory",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PhysicalExam_MedicalHistoryId",
                table: "PhysicalExam",
                column: "MedicalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Reason_MedicalHistoryId",
                table: "Reason",
                column: "MedicalHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TreatmentPlan_MedicalHistoryId",
                table: "TreatmentPlan",
                column: "MedicalHistoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CurrentIllness");

            migrationBuilder.DropTable(
                name: "PatientAntecedent");

            migrationBuilder.DropTable(
                name: "PhysicalExam");

            migrationBuilder.DropTable(
                name: "Reason");

            migrationBuilder.DropTable(
                name: "TreatmentPlan");

            migrationBuilder.DropTable(
                name: "Antecedent");

            migrationBuilder.DropTable(
                name: "MedicalHistory");
        }
    }
}
