using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddChannelToVerificationCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TelegramRequestId",
                table: "VerificationCodes",
                newName: "ProviderRequestId");

            migrationBuilder.AddColumn<int>(
                name: "Channel",
                table: "VerificationCodes",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Channel",
                table: "VerificationCodes");

            migrationBuilder.RenameColumn(
                name: "ProviderRequestId",
                table: "VerificationCodes",
                newName: "TelegramRequestId");
        }
    }
}
