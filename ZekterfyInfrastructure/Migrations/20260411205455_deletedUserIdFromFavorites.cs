using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekterfyInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class deletedUserIdFromFavorites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "user_id",
                table: "favorites");

            migrationBuilder.DropIndex(
                name: "IX_favorites_user_id",
                table: "favorites");

            migrationBuilder.AlterColumn<DateTime>(
                name: "added",
                table: "favorites",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "added",
                table: "favorites",
                type: "timestamp without time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_favorites_user_id",
                table: "favorites",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "user_id",
                table: "favorites",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id");
        }
    }
}
