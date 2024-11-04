using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmFinder.Migrations
{
    /// <inheritdoc />
    public partial class AddCountryFilmAndGenreFilm : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CountryFilm_Countries_CountriesId",
                table: "CountryFilm");

            migrationBuilder.DropTable(
                name: "FilmGenre");

            migrationBuilder.RenameColumn(
                name: "CountriesId",
                table: "CountryFilm",
                newName: "CountryId");

            migrationBuilder.AddColumn<int>(
                name: "FilmId",
                table: "Genres",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FilmId",
                table: "Countries",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GenreFilm",
                columns: table => new
                {
                    GenreId = table.Column<int>(type: "INTEGER", nullable: false),
                    FilmId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreFilm", x => new { x.GenreId, x.FilmId });
                    table.ForeignKey(
                        name: "FK_GenreFilm_Films_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GenreFilm_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Genres_FilmId",
                table: "Genres",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_Countries_FilmId",
                table: "Countries",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_GenreFilm_FilmId",
                table: "GenreFilm",
                column: "FilmId");

            migrationBuilder.AddForeignKey(
                name: "FK_Countries_Films_FilmId",
                table: "Countries",
                column: "FilmId",
                principalTable: "Films",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CountryFilm_Countries_CountryId",
                table: "CountryFilm",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Genres_Films_FilmId",
                table: "Genres",
                column: "FilmId",
                principalTable: "Films",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Countries_Films_FilmId",
                table: "Countries");

            migrationBuilder.DropForeignKey(
                name: "FK_CountryFilm_Countries_CountryId",
                table: "CountryFilm");

            migrationBuilder.DropForeignKey(
                name: "FK_Genres_Films_FilmId",
                table: "Genres");

            migrationBuilder.DropTable(
                name: "GenreFilm");

            migrationBuilder.DropIndex(
                name: "IX_Genres_FilmId",
                table: "Genres");

            migrationBuilder.DropIndex(
                name: "IX_Countries_FilmId",
                table: "Countries");

            migrationBuilder.DropColumn(
                name: "FilmId",
                table: "Genres");

            migrationBuilder.DropColumn(
                name: "FilmId",
                table: "Countries");

            migrationBuilder.RenameColumn(
                name: "CountryId",
                table: "CountryFilm",
                newName: "CountriesId");

            migrationBuilder.CreateTable(
                name: "FilmGenre",
                columns: table => new
                {
                    FilmId = table.Column<int>(type: "INTEGER", nullable: false),
                    GenresId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FilmGenre", x => new { x.FilmId, x.GenresId });
                    table.ForeignKey(
                        name: "FK_FilmGenre_Films_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FilmGenre_Genres_GenresId",
                        column: x => x.GenresId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FilmGenre_GenresId",
                table: "FilmGenre",
                column: "GenresId");

            migrationBuilder.AddForeignKey(
                name: "FK_CountryFilm_Countries_CountriesId",
                table: "CountryFilm",
                column: "CountriesId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
