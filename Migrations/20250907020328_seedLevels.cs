using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TrainingCenterAPI.Migrations
{
    /// <inheritdoc />
    public partial class seedLevels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "Classes");

            // 🟢 إنشاء جدول المستويات
            migrationBuilder.CreateTable(
                name: "Level",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LevelNumber = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Level", x => x.Id);
                });

            // 🟢 إدخال المستويات 1 - 7
            migrationBuilder.InsertData(
                table: "Level",
                columns: new[] { "Id", "LevelNumber", "Name" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), 1, "المستوى الأول" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), 2, "المستوى الثاني" },
                    { new Guid("33333333-3333-3333-3333-333333333333"), 3, "المستوى الثالث" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), 4, "المستوى الرابع" },
                    { new Guid("55555555-5555-5555-5555-555555555555"), 5, "المستوى الخامس" },
                    { new Guid("66666666-6666-6666-6666-666666666666"), 6, "المستوى السادس" },
                    { new Guid("77777777-7777-7777-7777-777777777777"), 7, "المستوى السابع" }
                });

            // 🟢 إضافة LevelId للجداول وربطه بالمستوى الأول كـ Default
            migrationBuilder.AddColumn<Guid>(
                name: "LevelId",
                table: "StudentClasses",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.AddColumn<Guid>(
                name: "LevelId",
                table: "Classes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.AddColumn<Guid>(
                name: "LevelId",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("11111111-1111-1111-1111-111111111111"));

            // 🟢 إنشاء الفهارس
            migrationBuilder.CreateIndex(
                name: "IX_StudentClasses_LevelId",
                table: "StudentClasses",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_LevelId",
                table: "Classes",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_LevelId",
                table: "AspNetUsers",
                column: "LevelId");

            // 🟢 إضافة العلاقات
            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Level_LevelId",
                table: "AspNetUsers",
                column: "LevelId",
                principalTable: "Level",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Level_LevelId",
                table: "Classes",
                column: "LevelId",
                principalTable: "Level",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentClasses_Level_LevelId",
                table: "StudentClasses",
                column: "LevelId",
                principalTable: "Level",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Level_LevelId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Level_LevelId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentClasses_Level_LevelId",
                table: "StudentClasses");

            migrationBuilder.DropTable(
                name: "Level");

            migrationBuilder.DropIndex(
                name: "IX_StudentClasses_LevelId",
                table: "StudentClasses");

            migrationBuilder.DropIndex(
                name: "IX_Classes_LevelId",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_LevelId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LevelId",
                table: "StudentClasses");

            migrationBuilder.DropColumn(
                name: "LevelId",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "LevelId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Classes",
                type: "int",
                nullable: true);
        }
    }
}
