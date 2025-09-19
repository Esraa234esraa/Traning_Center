using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingCenterAPI.Migrations
{
    /// <inheritdoc />
    public partial class edditclasss4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrentStudentClasses_levels_LevelId",
                table: "CurrentStudentClasses");

            migrationBuilder.DropIndex(
                name: "IX_CurrentStudentClasses_LevelId",
                table: "CurrentStudentClasses");

            migrationBuilder.DropColumn(
                name: "LevelId",
                table: "CurrentStudentClasses");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LevelId",
                table: "CurrentStudentClasses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_CurrentStudentClasses_LevelId",
                table: "CurrentStudentClasses",
                column: "LevelId");

            migrationBuilder.AddForeignKey(
                name: "FK_CurrentStudentClasses_levels_LevelId",
                table: "CurrentStudentClasses",
                column: "LevelId",
                principalTable: "levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
