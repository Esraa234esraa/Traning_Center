using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingCenterAPI.Migrations
{
    /// <inheritdoc />
    public partial class edditclasss2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_TeacherDetails_TeacherId",
                table: "Classes");

            migrationBuilder.AlterColumn<Guid>(
                name: "TeacherId",
                table: "Classes",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_TeacherDetails_TeacherId",
                table: "Classes",
                column: "TeacherId",
                principalTable: "TeacherDetails",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_TeacherDetails_TeacherId",
                table: "Classes");

            migrationBuilder.AlterColumn<Guid>(
                name: "TeacherId",
                table: "Classes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_TeacherDetails_TeacherId",
                table: "Classes",
                column: "TeacherId",
                principalTable: "TeacherDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
