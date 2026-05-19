using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekterfyInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateJunctionTablesClean : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "author_albums",
                columns: table => new
                {
                    author_id = table.Column<int>(type: "integer", nullable: false),
                    album_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("author_albums_pkey", x => new { x.author_id, x.album_id });
                    table.ForeignKey(
                        name: "fk_author_albums_album",
                        column: x => x.album_id,
                        principalTable: "albums",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_author_albums_author",
                        column: x => x.author_id,
                        principalTable: "authors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "author_genres",
                columns: table => new
                {
                    genre_id = table.Column<int>(type: "integer", nullable: false),
                    author_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("author_genres_pkey", x => new { x.author_id, x.genre_id });
                    table.ForeignKey(
                        name: "fk_author_genres_author",
                        column: x => x.author_id,
                        principalTable: "authors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_author_genres_genre",
                        column: x => x.genre_id,
                        principalTable: "genres",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "song_authors",
                columns: table => new
                {
                    song_id = table.Column<int>(type: "integer", nullable: false),
                    author_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("song_authors_pkey", x => new { x.song_id, x.author_id });
                    table.ForeignKey(
                        name: "fk_song_authors_author",
                        column: x => x.author_id,
                        principalTable: "authors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_song_authors_song",
                        column: x => x.song_id,
                        principalTable: "songs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "song_genres",
                columns: table => new
                {
                    song_id = table.Column<int>(type: "integer", nullable: false),
                    genre_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("song_genres_pkey", x => new { x.song_id, x.genre_id });
                    table.ForeignKey(
                        name: "fk_song_genres_genre",
                        column: x => x.genre_id,
                        principalTable: "genres",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_song_genres_song",
                        column: x => x.song_id,
                        principalTable: "songs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_author_albums_album_id",
                table: "author_albums",
                column: "album_id");

            migrationBuilder.CreateIndex(
                name: "IX_author_genres_genre_id",
                table: "author_genres",
                column: "genre_id");

            migrationBuilder.CreateIndex(
                name: "IX_song_authors_author_id",
                table: "song_authors",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "IX_song_genres_genre_id",
                table: "song_genres",
                column: "genre_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "author_albums");

            migrationBuilder.DropTable(
                name: "author_genres");

            migrationBuilder.DropTable(
                name: "song_authors");

            migrationBuilder.DropTable(
                name: "song_genres");
        }
    }
}
