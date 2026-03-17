using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekterfyInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBirthdateToAuthor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "albums",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying", nullable: false),
                    author_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("albums_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "authors",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    pseudonym = table.Column<string>(type: "character varying", nullable: true),
                    birthdate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("authors_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "genres",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "character varying", nullable: false),
                    info = table.Column<string>(type: "character varying", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("genres_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    password = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: false),
                    folowers_count = table.Column<int>(type: "integer", nullable: true),
                    folows_count = table.Column<int>(type: "integer", nullable: true),
                    prefered_genre_id = table.Column<int>(type: "integer", nullable: true),
                    listening = table.Column<bool>(type: "boolean", nullable: true),
                    is_admin = table.Column<bool>(type: "boolean", nullable: true),
                    avatar_url = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("users_pkey", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "songs",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    lenght = table.Column<int>(type: "integer", nullable: false),
                    num_of_streams = table.Column<int>(type: "integer", nullable: true),
                    name = table.Column<string>(type: "character varying", nullable: false),
                    album_id = table.Column<int>(type: "integer", nullable: true),
                    genre_name = table.Column<string>(type: "character varying", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("songs_pkey", x => x.id);
                    table.ForeignKey(
                        name: "fk_album_id",
                        column: x => x.album_id,
                        principalTable: "albums",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "author_albums",
                columns: table => new
                {
                    author_id = table.Column<int>(type: "integer", nullable: false),
                    album_id = table.Column<int>(type: "integer", nullable: false)
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
                    genre_id = table.Column<int>(type: "integer", nullable: true),
                    author_id = table.Column<int>(type: "integer", nullable: true)
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
                name: "favorites",
                columns: table => new
                {
                    added = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    user_id = table.Column<int>(type: "integer", nullable: true),
                    song_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "followers",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: true),
                    author_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "fk_author_id",
                        column: x => x.author_id,
                        principalTable: "authors",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "history",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    song_id = table.Column<int>(type: "integer", nullable: true),
                    played_at = table.Column<DateOnly>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("history_pkey", x => x.user_id);
                    table.ForeignKey(
                        name: "fk_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "queue",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: true),
                    song_id = table.Column<int>(type: "integer", nullable: true),
                    position = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "reports",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: true),
                    song_id = table.Column<int>(type: "integer", nullable: true),
                    reason = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    status = table.Column<bool>(type: "boolean", nullable: true),
                    id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "fk_song",
                        column: x => x.song_id,
                        principalTable: "songs",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_user",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "song_authors",
                columns: table => new
                {
                    song_id = table.Column<int>(type: "integer", nullable: true),
                    author_id = table.Column<int>(type: "integer", nullable: true)
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
                    song_id = table.Column<int>(type: "integer", nullable: true),
                    genre_id = table.Column<int>(type: "integer", nullable: true)
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
                name: "IX_favorites_user_id",
                table: "favorites",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_followers_author_id",
                table: "followers",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "IX_followers_user_id",
                table: "followers",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "history_user_id_key",
                table: "history",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_queue_user_id",
                table: "queue",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_reports_song_id",
                table: "reports",
                column: "song_id");

            migrationBuilder.CreateIndex(
                name: "IX_reports_user_id",
                table: "reports",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "reports_report_id_key",
                table: "reports",
                column: "id",
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_songs_album_id",
                table: "songs",
                column: "album_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "author_albums");

            migrationBuilder.DropTable(
                name: "author_genres");

            migrationBuilder.DropTable(
                name: "favorites");

            migrationBuilder.DropTable(
                name: "followers");

            migrationBuilder.DropTable(
                name: "history");

            migrationBuilder.DropTable(
                name: "queue");

            migrationBuilder.DropTable(
                name: "reports");

            migrationBuilder.DropTable(
                name: "song_authors");

            migrationBuilder.DropTable(
                name: "song_genres");

            migrationBuilder.DropTable(
                name: "users");

            migrationBuilder.DropTable(
                name: "authors");

            migrationBuilder.DropTable(
                name: "genres");

            migrationBuilder.DropTable(
                name: "songs");

            migrationBuilder.DropTable(
                name: "albums");
        }
    }
}
