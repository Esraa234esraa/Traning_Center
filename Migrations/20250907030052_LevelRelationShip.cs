using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingCenterAPI.Migrations
{
    /// <inheritdoc />
    public partial class LevelRelationShip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<Guid>(
                name: "LevelId1",
                table: "Classes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LevelId1",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Classes_LevelId1",
                table: "Classes",
                column: "LevelId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_LevelId1",
                table: "AspNetUsers",
                column: "LevelId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Level_LevelId",
                table: "AspNetUsers",
                column: "LevelId",
                principalTable: "Level",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Level_LevelId1",
                table: "AspNetUsers",
                column: "LevelId1",
                principalTable: "Level",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Level_LevelId",
                table: "Classes",
                column: "LevelId",
                principalTable: "Level",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Level_LevelId1",
                table: "Classes",
                column: "LevelId1",
                principalTable: "Level",
                principalColumn: "Id");

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
                name: "FK_AspNetUsers_Level_LevelId1",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Level_LevelId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_Level_LevelId1",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentClasses_Level_LevelId",
                table: "StudentClasses");

            migrationBuilder.DropIndex(
                name: "IX_Classes_LevelId1",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_LevelId1",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LevelId1",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "LevelId1",
                table: "AspNetUsers");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Level_LevelId",
                table: "AspNetUsers",
                column: "LevelId",
                principalTable: "Level",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_Level_LevelId",
                table: "Classes",
                column: "LevelId",
                principalTable: "Level",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StudentClasses_Level_LevelId",
                table: "StudentClasses",
                column: "LevelId",
                principalTable: "Level",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
