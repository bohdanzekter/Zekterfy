using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekterfyInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ForgetOldTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "author_albums",
                columns: table => new
                {
                    album_id = table.Column<int>(type: "integer", nullable: false),
                    author_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "fk_album",
                        column: x => x.album_id,
                        principalTable: "albums",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_author",
                        column: x => x.author_id,
                        principalTable: "authors",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "author_genres",
                columns: table => new
                {
                    author_id = table.Column<int>(type: "integer", nullable: true),
                    genre_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "author_id",
                        column: x => x.author_id,
                        principalTable: "authors",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "genre_id",
                        column: x => x.genre_id,
                        principalTable: "genres",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "song_authors",
                columns: table => new
                {
                    author_id = table.Column<int>(type: "integer", nullable: true),
                    song_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "author_id",
                        column: x => x.author_id,
                        principalTable: "authors",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "song_id",
                        column: x => x.song_id,
                        principalTable: "songs",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "song_genres",
                columns: table => new
                {
                    genre_id = table.Column<int>(type: "integer", nullable: true),
                    song_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "genre_id",
                        column: x => x.genre_id,
                        principalTable: "genres",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "song_id",
                        column: x => x.song_id,
                        principalTable: "songs",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_author_albums_album_id",
                table: "author_albums",
                column: "album_id");

            migrationBuilder.CreateIndex(
                name: "IX_author_albums_author_id",
                table: "author_albums",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "IX_author_genres_author_id",
                table: "author_genres",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "IX_author_genres_genre_id",
                table: "author_genres",
                column: "genre_id");

            migrationBuilder.CreateIndex(
                name: "IX_song_authors_author_id",
                table: "song_authors",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "IX_song_authors_song_id",
                table: "song_authors",
                column: "song_id");

            migrationBuilder.CreateIndex(
                name: "IX_song_genres_genre_id",
                table: "song_genres",
                column: "genre_id");

            migrationBuilder.CreateIndex(
                name: "IX_song_genres_song_id",
                table: "song_genres",
                column: "song_id");
        }
    }
}
