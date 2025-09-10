using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingCenterAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddOtpVerificationTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VerifyCodeExpiry",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "VerificationCode",
                table: "AspNetUsers",
                newName: "VerifyCode");

            migrationBuilder.CreateTable(
                name: "OtpVerifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OtpHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OtpSalt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AttemptCount = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastSentAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtpVerifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OtpVerifications_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OtpVerifications_UserId",
                table: "OtpVerifications",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OtpVerifications");

            migrationBuilder.RenameColumn(
                name: "VerifyCode",
                table: "AspNetUsers",
                newName: "VerificationCode");

            migrationBuilder.AddColumn<DateTime>(
                name: "VerifyCodeExpiry",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);
        }
    }
}
