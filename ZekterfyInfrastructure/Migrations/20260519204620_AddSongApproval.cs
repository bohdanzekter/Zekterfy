using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekterfyInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSongApproval : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_history_songs_song_id",
                table: "history");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "songs",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "fk_history_song",
                table: "history",
                column: "song_id",
                principalTable: "songs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_history_song",
                table: "history");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "songs");

            migrationBuilder.AddForeignKey(
                name: "FK_history_songs_song_id",
                table: "history",
                column: "song_id",
                principalTable: "songs",
                principalColumn: "id");
        }
    }
}
