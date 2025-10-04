using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingCenterAPI.Migrations
{
    /// <inheritdoc />
    public partial class updateexternalCoure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "Course");

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "ExternalCourses",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsVisible",
                table: "ExternalCourses");

            migrationBuilder.AddColumn<bool>(
                name: "IsVisible",
                table: "Course",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
