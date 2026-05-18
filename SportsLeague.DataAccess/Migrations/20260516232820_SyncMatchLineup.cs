using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsLeague.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class SyncMatchLineup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatchLineups_Players_PlayerId",
                table: "MatchLineups");

            migrationBuilder.DropIndex(
                name: "IX_MatchLineups_MatchId",
                table: "MatchLineups");

            migrationBuilder.AlterColumn<string>(
                name: "Position",
                table: "MatchLineups",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "MatchLineups",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "MatchLineups",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MatchLineups_MatchId_PlayerId",
                table: "MatchLineups",
                columns: new[] { "MatchId", "PlayerId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MatchLineups_Players_PlayerId",
                table: "MatchLineups",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MatchLineups_Players_PlayerId",
                table: "MatchLineups");

            migrationBuilder.DropIndex(
                name: "IX_MatchLineups_MatchId_PlayerId",
                table: "MatchLineups");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "MatchLineups");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "MatchLineups");

            migrationBuilder.AlterColumn<string>(
                name: "Position",
                table: "MatchLineups",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.CreateIndex(
                name: "IX_MatchLineups_MatchId",
                table: "MatchLineups",
                column: "MatchId");

            migrationBuilder.AddForeignKey(
                name: "FK_MatchLineups_Players_PlayerId",
                table: "MatchLineups",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
