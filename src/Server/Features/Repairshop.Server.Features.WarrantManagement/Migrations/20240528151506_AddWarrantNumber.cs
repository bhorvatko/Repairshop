using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repairshop.Server.Features.WarrantManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddWarrantNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Number",
                schema: "wm",
                table: "Warrants",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                schema: "wm",
                table: "Warrants");
        }
    }
}
