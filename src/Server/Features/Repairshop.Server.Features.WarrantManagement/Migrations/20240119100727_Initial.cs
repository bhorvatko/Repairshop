using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repairshop.Server.Features.WarrantManagement.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "wm");

        migrationBuilder.CreateTable(
            name: "Procedures",
            schema: "wm",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Color = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Procedures", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Procedures",
            schema: "wm");
    }
}
