using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZekterfyInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeGenreNameToGenreIdInDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "genre_name",
                table: "songs");

            migrationBuilder.AddColumn<int>(
                name: "genre_id",
                table: "songs",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "genre_id",
                table: "songs");

            migrationBuilder.AddColumn<string>(
                name: "genre_name",
                table: "songs",
                type: "character varying",
                nullable: false,
                defaultValue: "");
        }
    }
}
