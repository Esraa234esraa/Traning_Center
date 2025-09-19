using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingCenterAPI.Migrations
{
    /// <inheritdoc />
    public partial class edditclass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Classes_levels_LevelId",
                table: "Classes");

            migrationBuilder.DropForeignKey(
                name: "FK_Classes_levels_LevelId1",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_LevelId",
                table: "Classes");

            migrationBuilder.DropIndex(
                name: "IX_Classes_LevelId1",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "LevelId",
                table: "Classes");

            migrationBuilder.DropColumn(
                name: "LevelId1",
                table: "Classes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "LevelId",
                table: "Classes",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "LevelId1",
                table: "Classes",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Classes_LevelId",
                table: "Classes",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_LevelId1",
                table: "Classes",
                column: "LevelId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_levels_LevelId",
                table: "Classes",
                column: "LevelId",
                principalTable: "levels",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Classes_levels_LevelId1",
                table: "Classes",
                column: "LevelId1",
                principalTable: "levels",
                principalColumn: "Id");
        }
    }
}
