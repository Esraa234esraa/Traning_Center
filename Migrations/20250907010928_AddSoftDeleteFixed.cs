using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingCenterAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddSoftDeleteFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentClasses_Classes_ClassId",
                table: "StudentClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_WaitingList_AspNetUsers_StudentId",
                table: "WaitingList");

            migrationBuilder.DropForeignKey(
                name: "FK_WaitingList_Classes_ClassId",
                table: "WaitingList");

            migrationBuilder.AddColumn<Guid>(
                name: "ApplicationUserId",
                table: "WaitingList",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "WaitingList",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "TeacherDetails",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "StudentClasses",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "Classes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WaitingList_ApplicationUserId",
                table: "WaitingList",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentClasses_Classes_ClassId",
                table: "StudentClasses",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WaitingList_AspNetUsers_ApplicationUserId",
                table: "WaitingList",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_WaitingList_AspNetUsers_StudentId",
                table: "WaitingList",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WaitingList_Classes_ClassId",
                table: "WaitingList",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentClasses_Classes_ClassId",
                table: "StudentClasses");

            migrationBuilder.DropForeignKey(
                name: "FK_WaitingList_AspNetUsers_ApplicationUserId",
                table: "WaitingList");

            migrationBuilder.DropForeignKey(
                name: "FK_WaitingList_AspNetUsers_StudentId",
                table: "WaitingList");

            migrationBuilder.DropForeignKey(
                name: "FK_WaitingList_Classes_ClassId",
                table: "WaitingList");

            migrationBuilder.DropIndex(
                name: "IX_WaitingList_ApplicationUserId",
                table: "WaitingList");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "WaitingList");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "WaitingList");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "TeacherDetails");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "StudentClasses");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "Classes");

            migrationBuilder.AddForeignKey(
     name: "FK_StudentClasses_Classes_ClassId",
     table: "StudentClasses",
     column: "ClassId",
     principalTable: "Classes",
     principalColumn: "Id",
     onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WaitingList_AspNetUsers_StudentId",
                table: "WaitingList",
                column: "StudentId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_WaitingList_Classes_ClassId",
                table: "WaitingList",
                column: "ClassId",
                principalTable: "Classes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

        }
    }
}
