using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FilmFinder.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Countries",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CountryName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Films",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    KinopoiskId = table.Column<int>(type: "INTEGER", nullable: false),
                    KinopoiskHDId = table.Column<string>(type: "TEXT", nullable: true),
                    ImdbId = table.Column<string>(type: "TEXT", nullable: true),
                    NameRu = table.Column<string>(type: "TEXT", nullable: true),
                    NameEn = table.Column<string>(type: "TEXT", nullable: true),
                    NameOriginal = table.Column<string>(type: "TEXT", nullable: true),
                    PosterUrl = table.Column<string>(type: "TEXT", nullable: false),
                    PosterUrlPreview = table.Column<string>(type: "TEXT", nullable: false),
                    CoverUrl = table.Column<string>(type: "TEXT", nullable: true),
                    LogoUrl = table.Column<string>(type: "TEXT", nullable: true),
                    ReviewsCount = table.Column<int>(type: "INTEGER", nullable: false),
                    RatingGoodReview = table.Column<double>(type: "REAL", nullable: true),
                    RatingGoodReviewVoteCount = table.Column<int>(type: "INTEGER", nullable: true),
                    RatingKinopoisk = table.Column<double>(type: "REAL", nullable: true),
                    RatingKinopoiskVoteCount = table.Column<int>(type: "INTEGER", nullable: true),
                    RatingImdb = table.Column<double>(type: "REAL", nullable: true),
                    RatingImdbVoteCount = table.Column<int>(type: "INTEGER", nullable: true),
                    RatingFilmCritics = table.Column<double>(type: "REAL", nullable: true),
                    RatingFilmCriticsVoteCount = table.Column<int>(type: "INTEGER", nullable: true),
                    RatingAwait = table.Column<double>(type: "REAL", nullable: true),
                    RatingAwaitCount = table.Column<int>(type: "INTEGER", nullable: true),
                    RatingRfCritics = table.Column<double>(type: "REAL", nullable: true),
                    RatingRfCriticsVoteCount = table.Column<int>(type: "INTEGER", nullable: true),
                    WebUrl = table.Column<string>(type: "TEXT", nullable: false),
                    Year = table.Column<int>(type: "INTEGER", nullable: true),
                    FilmLength = table.Column<int>(type: "INTEGER", nullable: true),
                    Slogan = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    ShortDescription = table.Column<string>(type: "TEXT", nullable: true),
                    EditorAnnotation = table.Column<string>(type: "TEXT", nullable: true),
                    IsTicketsAvailable = table.Column<bool>(type: "INTEGER", nullable: false),
                    ProductionStatus = table.Column<string>(type: "TEXT", nullable: true),
                    Type = table.Column<string>(type: "TEXT", nullable: false),
                    RatingMpaa = table.Column<string>(type: "TEXT", nullable: true),
                    RatingAgeLimits = table.Column<string>(type: "TEXT", nullable: true),
                    HasImax = table.Column<bool>(type: "INTEGER", nullable: true),
                    Has3D = table.Column<bool>(type: "INTEGER", nullable: true),
                    LastSync = table.Column<string>(type: "TEXT", nullable: false),
                    StartYear = table.Column<int>(type: "INTEGER", nullable: true),
                    EndYear = table.Column<int>(type: "INTEGER", nullable: true),
                    Serial = table.Column<bool>(type: "INTEGER", nullable: true),
                    ShortFilm = table.Column<bool>(type: "INTEGER", nullable: true),
                    Completed = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Films", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GenreName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CountryFilm",
                columns: table => new
                {
                    CountriesId = table.Column<int>(type: "INTEGER", nullable: false),
                    FilmId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CountryFilm", x => new { x.CountriesId, x.FilmId });
                    table.ForeignKey(
                        name: "FK_CountryFilm_Countries_CountriesId",
                        column: x => x.CountriesId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CountryFilm_Films_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Films",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                name: "IX_CountryFilm_FilmId",
                table: "CountryFilm",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_FilmGenre_GenresId",
                table: "FilmGenre",
                column: "GenresId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CountryFilm");

            migrationBuilder.DropTable(
                name: "FilmGenre");

            migrationBuilder.DropTable(
                name: "Countries");

            migrationBuilder.DropTable(
                name: "Films");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
