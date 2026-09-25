using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSeasonOverride : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SeasonOverrides",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Season = table.Column<int>(type: "integer", nullable: true),
                    SetByUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    SetAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExpiresAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeasonOverrides", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeasonOverrides_Users_SetByUserId",
                        column: x => x.SetByUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SeasonOverrides_SetAtUtc",
                table: "SeasonOverrides",
                column: "SetAtUtc",
                descending: new bool[0]);

            migrationBuilder.CreateIndex(
                name: "IX_SeasonOverrides_SetByUserId",
                table: "SeasonOverrides",
                column: "SetByUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SeasonOverrides");
        }
    }
}
