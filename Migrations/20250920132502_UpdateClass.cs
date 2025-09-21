using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingCenterAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_TeacherDetails_TeacherId",
                table: "Classes");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_TeacherDetails_TeacherId",
                table: "Classes",
                column: "TeacherId",
                principalTable: "TeacherDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_TeacherDetails_TeacherId",
                table: "Classes");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_TeacherDetails_TeacherId",
                table: "Classes",
                column: "TeacherId",
                principalTable: "TeacherDetails",
                principalColumn: "Id");
        }
    }
}
