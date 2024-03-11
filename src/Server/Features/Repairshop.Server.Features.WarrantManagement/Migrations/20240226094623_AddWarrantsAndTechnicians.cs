using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repairshop.Server.Features.WarrantManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddWarrantsAndTechnicians : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Technicians",
                schema: "wm",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Technicians", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Warrants",
                schema: "wm",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsUrgent = table.Column<bool>(type: "bit", nullable: false),
                    CurrentStepId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TechnicianId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warrants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Warrants_Technicians_TechnicianId",
                        column: x => x.TechnicianId,
                        principalSchema: "wm",
                        principalTable: "Technicians",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "WarrantSteps",
                schema: "wm",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProcedureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarrantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarrantSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarrantSteps_Procedures_ProcedureId",
                        column: x => x.ProcedureId,
                        principalSchema: "wm",
                        principalTable: "Procedures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WarrantSteps_Warrants_WarrantId",
                        column: x => x.WarrantId,
                        principalSchema: "wm",
                        principalTable: "Warrants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WarrantStepTransitions",
                schema: "wm",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PreviousStepId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NextStepId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CanBePerformedByFrontOffice = table.Column<bool>(type: "bit", nullable: false),
                    CanBePerformedByWorkshop = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarrantStepTransitions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarrantStepTransitions_WarrantSteps_NextStepId",
                        column: x => x.NextStepId,
                        principalSchema: "wm",
                        principalTable: "WarrantSteps",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_WarrantStepTransitions_WarrantSteps_PreviousStepId",
                        column: x => x.PreviousStepId,
                        principalSchema: "wm",
                        principalTable: "WarrantSteps",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Warrants_CurrentStepId",
                schema: "wm",
                table: "Warrants",
                column: "CurrentStepId",
                unique: true,
                filter: "[CurrentStepId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Warrants_TechnicianId",
                schema: "wm",
                table: "Warrants",
                column: "TechnicianId");

            migrationBuilder.CreateIndex(
                name: "IX_WarrantSteps_ProcedureId",
                schema: "wm",
                table: "WarrantSteps",
                column: "ProcedureId");

            migrationBuilder.CreateIndex(
                name: "IX_WarrantSteps_WarrantId",
                schema: "wm",
                table: "WarrantSteps",
                column: "WarrantId");

            migrationBuilder.CreateIndex(
                name: "IX_WarrantStepTransitions_NextStepId",
                schema: "wm",
                table: "WarrantStepTransitions",
                column: "NextStepId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WarrantStepTransitions_PreviousStepId",
                schema: "wm",
                table: "WarrantStepTransitions",
                column: "PreviousStepId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Warrants_WarrantSteps_CurrentStepId",
                schema: "wm",
                table: "Warrants",
                column: "CurrentStepId",
                principalSchema: "wm",
                principalTable: "WarrantSteps",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Warrants_Technicians_TechnicianId",
                schema: "wm",
                table: "Warrants");

            migrationBuilder.DropForeignKey(
                name: "FK_Warrants_WarrantSteps_CurrentStepId",
                schema: "wm",
                table: "Warrants");

            migrationBuilder.DropTable(
                name: "WarrantStepTransitions",
                schema: "wm");

            migrationBuilder.DropTable(
                name: "Technicians",
                schema: "wm");

            migrationBuilder.DropTable(
                name: "WarrantSteps",
                schema: "wm");

            migrationBuilder.DropTable(
                name: "Warrants",
                schema: "wm");
        }
    }
}
