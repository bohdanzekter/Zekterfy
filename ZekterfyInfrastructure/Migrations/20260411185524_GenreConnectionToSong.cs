using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekterfyInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class GenreConnectionToSong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_songs_genre_id",
                table: "songs",
                column: "genre_id");

            migrationBuilder.AddForeignKey(
                name: "FK_songs_genres_genre_id",
                table: "songs",
                column: "genre_id",
                principalTable: "genres",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_songs_genres_genre_id",
                table: "songs");

            migrationBuilder.DropIndex(
                name: "IX_songs_genre_id",
                table: "songs");
        }
    }
}
