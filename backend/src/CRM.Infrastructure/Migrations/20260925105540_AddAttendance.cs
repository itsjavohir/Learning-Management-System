using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CRM.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAttendance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attendances",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LessonId = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    AbsenceReason = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    MentorNote = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    MarkedByMentorId = table.Column<Guid>(type: "uuid", nullable: true),
                    MarkedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    LateMinutes = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendances", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendances_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attendances_Mentors_MarkedByMentorId",
                        column: x => x.MarkedByMentorId,
                        principalTable: "Mentors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attendances_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LessonScore",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LessonId = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<Guid>(type: "uuid", nullable: false),
                    Score = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    MentorFeedback = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    ScoredByMentorId = table.Column<Guid>(type: "uuid", nullable: true),
                    ScoredAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LessonScore", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LessonScore_Lessons_LessonId",
                        column: x => x.LessonId,
                        principalTable: "Lessons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LessonScore_Mentors_ScoredByMentorId",
                        column: x => x.ScoredByMentorId,
                        principalTable: "Mentors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LessonScore_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Schedule",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    DayOfWeek = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Room = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    RecurringFrom = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    RecurringTo = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Schedule_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_LessonId_StudentId",
                table: "Attendances",
                columns: new[] { "LessonId", "StudentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_MarkedByMentorId",
                table: "Attendances",
                column: "MarkedByMentorId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendances_StudentId",
                table: "Attendances",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonScore_LessonId_StudentId",
                table: "LessonScore",
                columns: new[] { "LessonId", "StudentId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_LessonScore_ScoredByMentorId",
                table: "LessonScore",
                column: "ScoredByMentorId");

            migrationBuilder.CreateIndex(
                name: "IX_LessonScore_StudentId",
                table: "LessonScore",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Schedule_GroupId_DayOfWeek",
                table: "Schedule",
                columns: new[] { "GroupId", "DayOfWeek" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attendances");

            migrationBuilder.DropTable(
                name: "LessonScore");

            migrationBuilder.DropTable(
                name: "Schedule");
        }
    }
}
