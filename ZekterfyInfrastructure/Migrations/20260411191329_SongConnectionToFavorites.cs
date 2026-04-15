using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekterfyInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SongConnectionToFavorites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_favorites_song_id",
                table: "favorites",
                column: "song_id");

            migrationBuilder.AddForeignKey(
                name: "FK_favorites_songs_song_id",
                table: "favorites",
                column: "song_id",
                principalTable: "songs",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_favorites_songs_song_id",
                table: "favorites");

            migrationBuilder.DropIndex(
                name: "IX_favorites_song_id",
                table: "favorites");
        }
    }
}
