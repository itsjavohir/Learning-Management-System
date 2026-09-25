using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AboutMe",
                table: "Students",
                type: "character varying(2000)",
                maxLength: 2000,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Students",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "EnrollDate",
                table: "Students",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "GithubUrl",
                table: "Students",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Students",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Students",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Students",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TelegramUsername",
                table: "Students",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GithubUrl",
                table: "Mentors",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "HireDate",
                table: "Mentors",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Mentors",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "LinkedInUrl",
                table: "Mentors",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone",
                table: "Mentors",
                type: "character varying(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "LeftAt",
                table: "GroupStudents",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RemoveReason",
                table: "GroupStudents",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TransferredFromGroupStudentId",
                table: "GroupStudents",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "TransferredToGroupStudentId",
                table: "GroupStudents",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    WeekNumber = table.Column<int>(type: "integer", nullable: false),
                    LessonDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Title = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    HomeworkDescription = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    MaterialUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lessons_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Profiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    AvatarUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    Phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Address = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: true),
                    TelegramUsername = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    LinkedInUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    GithubUrl = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    AboutMe = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Profiles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupStudents_TransferredFromGroupStudentId",
                table: "GroupStudents",
                column: "TransferredFromGroupStudentId");

            migrationBuilder.CreateIndex(
                name: "IX_GroupStudents_TransferredToGroupStudentId",
                table: "GroupStudents",
                column: "TransferredToGroupStudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_GroupId_WeekNumber",
                table: "Lessons",
                columns: new[] { "GroupId", "WeekNumber" });

            migrationBuilder.CreateIndex(
                name: "IX_Profiles_UserId",
                table: "Profiles",
                column: "UserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupStudents_GroupStudents_TransferredFromGroupStudentId",
                table: "GroupStudents",
                column: "TransferredFromGroupStudentId",
                principalTable: "GroupStudents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_GroupStudents_GroupStudents_TransferredToGroupStudentId",
                table: "GroupStudents",
                column: "TransferredToGroupStudentId",
                principalTable: "GroupStudents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupStudents_GroupStudents_TransferredFromGroupStudentId",
                table: "GroupStudents");

            migrationBuilder.DropForeignKey(
                name: "FK_GroupStudents_GroupStudents_TransferredToGroupStudentId",
                table: "GroupStudents");

            migrationBuilder.DropTable(
                name: "Lessons");

            migrationBuilder.DropTable(
                name: "Profiles");

            migrationBuilder.DropIndex(
                name: "IX_GroupStudents_TransferredFromGroupStudentId",
                table: "GroupStudents");

            migrationBuilder.DropIndex(
                name: "IX_GroupStudents_TransferredToGroupStudentId",
                table: "GroupStudents");

            migrationBuilder.DropColumn(
                name: "AboutMe",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "EnrollDate",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "GithubUrl",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "TelegramUsername",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "GithubUrl",
                table: "Mentors");

            migrationBuilder.DropColumn(
                name: "HireDate",
                table: "Mentors");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Mentors");

            migrationBuilder.DropColumn(
                name: "LinkedInUrl",
                table: "Mentors");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "Mentors");

            migrationBuilder.DropColumn(
                name: "LeftAt",
                table: "GroupStudents");

            migrationBuilder.DropColumn(
                name: "RemoveReason",
                table: "GroupStudents");

            migrationBuilder.DropColumn(
                name: "TransferredFromGroupStudentId",
                table: "GroupStudents");

            migrationBuilder.DropColumn(
                name: "TransferredToGroupStudentId",
                table: "GroupStudents");
        }
    }
}
