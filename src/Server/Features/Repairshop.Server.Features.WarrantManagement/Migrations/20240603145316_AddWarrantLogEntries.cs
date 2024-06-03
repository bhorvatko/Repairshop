using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repairshop.Server.Features.WarrantManagement.Migrations
{
    /// <inheritdoc />
    public partial class AddWarrantLogEntries : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WarrantLogEntries",
                schema: "wm",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WarrantNumber = table.Column<int>(type: "int", nullable: false),
                    PreviousState = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TechnicianName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    WarrantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WarrantLogEntries", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WarrantLogEntries_Warrants_WarrantId",
                        column: x => x.WarrantId,
                        principalSchema: "wm",
                        principalTable: "Warrants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_WarrantLogEntries_WarrantId",
                schema: "wm",
                table: "WarrantLogEntries",
                column: "WarrantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WarrantLogEntries",
                schema: "wm");
        }
    }
}
