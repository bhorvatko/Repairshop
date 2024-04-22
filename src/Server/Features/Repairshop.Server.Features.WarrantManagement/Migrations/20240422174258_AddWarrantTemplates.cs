using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repairshop.Server.Features.WarrantManagement.Migrations;

/// <inheritdoc />
public partial class AddWarrantTemplates : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "WarrantTemplates",
            schema: "wm",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_WarrantTemplates", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "WarrantTemplateSteps",
            schema: "wm",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ProcedureId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                CanBeTransitionedToByWorkshop = table.Column<bool>(type: "bit", nullable: false),
                CanBeTransitionedToByFrontOffice = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_WarrantTemplateSteps", x => x.Id);
                table.ForeignKey(
                    name: "FK_WarrantTemplateSteps_Procedures_ProcedureId",
                    column: x => x.ProcedureId,
                    principalSchema: "wm",
                    principalTable: "Procedures",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_WarrantTemplateSteps_WarrantTemplates_Id",
                    column: x => x.Id,
                    principalSchema: "wm",
                    principalTable: "WarrantTemplates",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_WarrantTemplateSteps_ProcedureId",
            schema: "wm",
            table: "WarrantTemplateSteps",
            column: "ProcedureId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "WarrantTemplateSteps",
            schema: "wm");

        migrationBuilder.DropTable(
            name: "WarrantTemplates",
            schema: "wm");
    }
}
