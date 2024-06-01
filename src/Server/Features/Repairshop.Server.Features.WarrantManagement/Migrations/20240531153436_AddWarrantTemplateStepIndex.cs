using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repairshop.Server.Features.WarrantManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddWarrantTemplateStepIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Index",
                schema: "wm",
                table: "WarrantTemplateSteps",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Index",
                schema: "wm",
                table: "WarrantTemplateSteps");
        }
    }
}
