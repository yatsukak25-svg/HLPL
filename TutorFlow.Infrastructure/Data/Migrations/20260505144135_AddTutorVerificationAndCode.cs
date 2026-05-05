using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TutorFlow.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddTutorVerificationAndCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "TutorProfiles",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "TutorCode",
                table: "TutorProfiles",
                type: "TEXT",
                maxLength: 10,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "TutorStudentRelations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TutorId = table.Column<int>(type: "INTEGER", nullable: false),
                    StudentId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TutorStudentRelations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TutorStudentRelations_StudentProfiles_StudentId",
                        column: x => x.StudentId,
                        principalTable: "StudentProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TutorStudentRelations_TutorProfiles_TutorId",
                        column: x => x.TutorId,
                        principalTable: "TutorProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TutorStudentRelations_StudentId",
                table: "TutorStudentRelations",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_TutorStudentRelations_TutorId_StudentId",
                table: "TutorStudentRelations",
                columns: new[] { "TutorId", "StudentId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TutorStudentRelations");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "TutorProfiles");

            migrationBuilder.DropColumn(
                name: "TutorCode",
                table: "TutorProfiles");
        }
    }
}
