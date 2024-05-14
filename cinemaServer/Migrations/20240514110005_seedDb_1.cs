using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace cinemaServer.Migrations
{
    /// <inheritdoc />
    public partial class seedDb_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    customer_id = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    age = table.Column<int>(type: "integer", nullable: false),
                    country = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.customer_id);
                });

            migrationBuilder.CreateTable(
                name: "playable_movies",
                columns: table => new
                {
                    movie_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    runtime = table.Column<int>(type: "integer", nullable: false),
                    release_year = table.Column<int>(type: "integer", nullable: false),
                    rating = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_playable_movies", x => x.movie_id);
                });

            migrationBuilder.CreateTable(
                name: "theater_rooms",
                columns: table => new
                {
                    theater_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    capacity = table.Column<int>(type: "integer", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_theater_rooms", x => x.theater_id);
                });

            migrationBuilder.CreateTable(
                name: "movie_screenings",
                columns: table => new
                {
                    screening_id = table.Column<int>(type: "integer", nullable: false),
                    movie_id = table.Column<int>(type: "integer", nullable: false),
                    theater_id = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_movie_screenings", x => new { x.screening_id, x.movie_id, x.theater_id });
                    table.ForeignKey(
                        name: "FK_movie_screenings_playable_movies_movie_id",
                        column: x => x.movie_id,
                        principalTable: "playable_movies",
                        principalColumn: "movie_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_movie_screenings_theater_rooms_theater_id",
                        column: x => x.theater_id,
                        principalTable: "theater_rooms",
                        principalColumn: "theater_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    ticket_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    screening_id = table.Column<int>(type: "integer", nullable: false),
                    customer_id = table.Column<string>(type: "text", nullable: true),
                    column = table.Column<int>(type: "integer", nullable: false),
                    row = table.Column<int>(type: "integer", nullable: false),
                    ScreeningMovieId = table.Column<int>(type: "integer", nullable: false),
                    ScreeningTheaterId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.ticket_id);
                    table.ForeignKey(
                        name: "FK_Tickets_Customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "Customers",
                        principalColumn: "customer_id");
                    table.ForeignKey(
                        name: "FK_Tickets_movie_screenings_screening_id_ScreeningMovieId_Scre~",
                        columns: x => new { x.screening_id, x.ScreeningMovieId, x.ScreeningTheaterId },
                        principalTable: "movie_screenings",
                        principalColumns: new[] { "screening_id", "movie_id", "theater_id" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "playable_movies",
                columns: new[] { "movie_id", "description", "rating", "runtime", "title", "release_year" },
                values: new object[,]
                {
                    { 1, "A description", 1, 99, "Some Title", 1987 },
                    { 2, "A description", 0, 69, "Some Title", 2006 },
                    { 3, "A description", 1, 247, "Some Title", 1976 },
                    { 4, "A description", 2, 230, "Some Title", 1978 },
                    { 5, "A description", 3, 278, "Some Title", 2024 },
                    { 6, "A description", 3, 388, "Some Title", 2009 },
                    { 7, "A description", 0, 56, "Some Title", 1970 },
                    { 8, "A description", 1, 46, "Some Title", 2016 },
                    { 9, "A description", 0, 380, "Some Title", 1997 },
                    { 10, "A description", 4, 179, "Some Title", 1957 },
                    { 11, "A description", 1, 368, "Some Title", 2009 },
                    { 12, "A description", 4, 375, "Some Title", 2016 },
                    { 13, "A description", 1, 364, "Some Title", 1979 },
                    { 14, "A description", 3, 237, "Some Title", 1951 },
                    { 15, "A description", 4, 145, "Some Title", 2011 },
                    { 16, "A description", 4, 131, "Some Title", 1975 },
                    { 17, "A description", 1, 369, "Some Title", 2023 },
                    { 18, "A description", 0, 45, "Some Title", 1956 },
                    { 19, "A description", 3, 155, "Some Title", 1961 },
                    { 20, "A description", 0, 53, "Some Title", 1957 },
                    { 21, "A description", 1, 58, "Some Title", 1964 },
                    { 22, "A description", 2, 191, "Some Title", 1994 },
                    { 23, "A description", 0, 146, "Some Title", 2003 },
                    { 24, "A description", 1, 307, "Some Title", 1955 },
                    { 25, "A description", 1, 128, "Some Title", 1985 },
                    { 26, "A description", 0, 212, "Some Title", 2003 },
                    { 27, "A description", 1, 201, "Some Title", 2004 },
                    { 28, "A description", 0, 151, "Some Title", 1983 },
                    { 29, "A description", 3, 234, "Some Title", 2020 },
                    { 30, "A description", 3, 78, "Some Title", 1997 },
                    { 31, "A description", 1, 166, "Some Title", 2017 },
                    { 32, "A description", 4, 139, "Some Title", 1989 },
                    { 33, "A description", 1, 182, "Some Title", 2015 },
                    { 34, "A description", 3, 128, "Some Title", 2021 },
                    { 35, "A description", 4, 292, "Some Title", 1963 },
                    { 36, "A description", 3, 341, "Some Title", 2006 },
                    { 37, "A description", 2, 338, "Some Title", 2012 },
                    { 38, "A description", 1, 235, "Some Title", 1992 },
                    { 39, "A description", 3, 327, "Some Title", 2004 },
                    { 40, "A description", 3, 86, "Some Title", 2015 },
                    { 41, "A description", 0, 345, "Some Title", 1964 },
                    { 42, "A description", 0, 171, "Some Title", 1980 },
                    { 43, "A description", 4, 78, "Some Title", 2022 },
                    { 44, "A description", 4, 190, "Some Title", 1956 },
                    { 45, "A description", 3, 335, "Some Title", 1965 },
                    { 46, "A description", 1, 273, "Some Title", 1994 },
                    { 47, "A description", 0, 124, "Some Title", 2001 },
                    { 48, "A description", 4, 81, "Some Title", 1968 },
                    { 49, "A description", 1, 147, "Some Title", 2010 },
                    { 50, "A description", 3, 363, "Some Title", 1984 },
                    { 51, "A description", 4, 72, "Some Title", 1970 },
                    { 52, "A description", 3, 238, "Some Title", 1977 },
                    { 53, "A description", 3, 234, "Some Title", 1976 },
                    { 54, "A description", 3, 297, "Some Title", 2007 },
                    { 55, "A description", 3, 271, "Some Title", 1982 },
                    { 56, "A description", 1, 250, "Some Title", 2003 },
                    { 57, "A description", 0, 255, "Some Title", 1961 },
                    { 58, "A description", 0, 210, "Some Title", 2016 },
                    { 59, "A description", 4, 212, "Some Title", 2007 },
                    { 60, "A description", 3, 140, "Some Title", 1955 },
                    { 61, "A description", 2, 334, "Some Title", 1963 },
                    { 62, "A description", 4, 101, "Some Title", 1975 },
                    { 63, "A description", 4, 338, "Some Title", 1971 },
                    { 64, "A description", 1, 46, "Some Title", 1958 },
                    { 65, "A description", 4, 218, "Some Title", 1987 },
                    { 66, "A description", 4, 178, "Some Title", 1983 },
                    { 67, "A description", 0, 64, "Some Title", 2002 },
                    { 68, "A description", 1, 381, "Some Title", 1973 },
                    { 69, "A description", 1, 270, "Some Title", 1995 },
                    { 70, "A description", 3, 324, "Some Title", 1955 },
                    { 71, "A description", 1, 324, "Some Title", 1970 },
                    { 72, "A description", 2, 99, "Some Title", 2016 },
                    { 73, "A description", 0, 140, "Some Title", 1986 },
                    { 74, "A description", 2, 368, "Some Title", 2005 },
                    { 75, "A description", 0, 222, "Some Title", 1995 },
                    { 76, "A description", 1, 353, "Some Title", 2023 },
                    { 77, "A description", 1, 132, "Some Title", 1957 },
                    { 78, "A description", 1, 90, "Some Title", 1966 },
                    { 79, "A description", 4, 224, "Some Title", 2014 },
                    { 80, "A description", 3, 120, "Some Title", 1988 },
                    { 81, "A description", 1, 190, "Some Title", 1952 },
                    { 82, "A description", 4, 127, "Some Title", 1964 },
                    { 83, "A description", 3, 376, "Some Title", 1975 },
                    { 84, "A description", 4, 162, "Some Title", 1958 },
                    { 85, "A description", 4, 297, "Some Title", 1958 },
                    { 86, "A description", 3, 272, "Some Title", 1984 },
                    { 87, "A description", 3, 132, "Some Title", 2006 },
                    { 88, "A description", 4, 387, "Some Title", 1991 },
                    { 89, "A description", 0, 143, "Some Title", 2000 },
                    { 90, "A description", 0, 360, "Some Title", 1997 },
                    { 91, "A description", 4, 294, "Some Title", 1954 },
                    { 92, "A description", 3, 163, "Some Title", 1988 },
                    { 93, "A description", 2, 121, "Some Title", 1970 },
                    { 94, "A description", 3, 115, "Some Title", 2020 },
                    { 95, "A description", 4, 317, "Some Title", 2018 },
                    { 96, "A description", 0, 169, "Some Title", 1980 },
                    { 97, "A description", 2, 187, "Some Title", 2014 },
                    { 98, "A description", 3, 99, "Some Title", 2022 },
                    { 99, "A description", 2, 85, "Some Title", 1982 }
                });

            migrationBuilder.InsertData(
                table: "theater_rooms",
                columns: new[] { "theater_id", "capacity", "name" },
                values: new object[,]
                {
                    { 1, 41, "A theater name" },
                    { 2, 143, "A theater name" },
                    { 3, 81, "A theater name" },
                    { 4, 119, "A theater name" },
                    { 5, 14, "A theater name" },
                    { 6, 108, "A theater name" },
                    { 7, 89, "A theater name" },
                    { 8, 95, "A theater name" },
                    { 9, 15, "A theater name" },
                    { 10, 146, "A theater name" },
                    { 11, 63, "A theater name" },
                    { 12, 93, "A theater name" },
                    { 13, 43, "A theater name" },
                    { 14, 70, "A theater name" },
                    { 15, 33, "A theater name" },
                    { 16, 49, "A theater name" },
                    { 17, 14, "A theater name" },
                    { 18, 125, "A theater name" },
                    { 19, 149, "A theater name" }
                });

            migrationBuilder.InsertData(
                table: "movie_screenings",
                columns: new[] { "screening_id", "movie_id", "theater_id", "StartTime" },
                values: new object[,]
                {
                    { 1, 21, 13, new DateTime(2025, 12, 4, 13, 0, 0, 0, DateTimeKind.Utc) },
                    { 2, 95, 10, new DateTime(2020, 8, 15, 16, 0, 0, 0, DateTimeKind.Utc) },
                    { 3, 10, 5, new DateTime(2005, 2, 1, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 4, 98, 7, new DateTime(2023, 6, 3, 21, 0, 0, 0, DateTimeKind.Utc) },
                    { 5, 92, 5, new DateTime(2007, 12, 8, 14, 0, 0, 0, DateTimeKind.Utc) },
                    { 6, 2, 10, new DateTime(2004, 6, 2, 11, 0, 0, 0, DateTimeKind.Utc) },
                    { 7, 86, 18, new DateTime(2024, 2, 15, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 8, 83, 5, new DateTime(2000, 3, 29, 20, 0, 0, 0, DateTimeKind.Utc) },
                    { 9, 54, 18, new DateTime(2024, 1, 19, 23, 0, 0, 0, DateTimeKind.Utc) },
                    { 10, 74, 4, new DateTime(2013, 1, 9, 15, 0, 0, 0, DateTimeKind.Utc) },
                    { 11, 85, 8, new DateTime(2016, 12, 7, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { 12, 99, 7, new DateTime(2001, 7, 17, 11, 0, 0, 0, DateTimeKind.Utc) },
                    { 13, 45, 7, new DateTime(2009, 5, 27, 18, 0, 0, 0, DateTimeKind.Utc) },
                    { 14, 18, 6, new DateTime(2002, 11, 11, 14, 0, 0, 0, DateTimeKind.Utc) },
                    { 15, 74, 6, new DateTime(2016, 8, 2, 13, 0, 0, 0, DateTimeKind.Utc) },
                    { 16, 98, 5, new DateTime(2006, 12, 23, 18, 0, 0, 0, DateTimeKind.Utc) },
                    { 17, 85, 19, new DateTime(2003, 12, 20, 14, 0, 0, 0, DateTimeKind.Utc) },
                    { 18, 46, 6, new DateTime(2025, 1, 19, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 19, 49, 4, new DateTime(2012, 12, 24, 14, 0, 0, 0, DateTimeKind.Utc) },
                    { 20, 20, 12, new DateTime(2014, 5, 7, 19, 0, 0, 0, DateTimeKind.Utc) },
                    { 21, 45, 2, new DateTime(2017, 8, 21, 18, 0, 0, 0, DateTimeKind.Utc) },
                    { 22, 56, 15, new DateTime(2001, 8, 4, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { 23, 73, 2, new DateTime(2001, 11, 26, 15, 0, 0, 0, DateTimeKind.Utc) },
                    { 24, 47, 4, new DateTime(2010, 1, 13, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 25, 88, 6, new DateTime(2001, 8, 19, 21, 0, 0, 0, DateTimeKind.Utc) },
                    { 26, 54, 16, new DateTime(2017, 3, 17, 11, 0, 0, 0, DateTimeKind.Utc) },
                    { 27, 41, 14, new DateTime(2023, 3, 12, 11, 0, 0, 0, DateTimeKind.Utc) },
                    { 28, 79, 7, new DateTime(2013, 4, 4, 20, 0, 0, 0, DateTimeKind.Utc) },
                    { 29, 71, 5, new DateTime(2010, 12, 19, 16, 0, 0, 0, DateTimeKind.Utc) },
                    { 30, 39, 3, new DateTime(2015, 7, 28, 21, 0, 0, 0, DateTimeKind.Utc) },
                    { 31, 1, 4, new DateTime(2013, 2, 9, 22, 0, 0, 0, DateTimeKind.Utc) },
                    { 32, 14, 1, new DateTime(2000, 9, 26, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { 33, 74, 2, new DateTime(2021, 6, 12, 18, 0, 0, 0, DateTimeKind.Utc) },
                    { 34, 3, 4, new DateTime(2022, 9, 17, 17, 0, 0, 0, DateTimeKind.Utc) },
                    { 35, 15, 14, new DateTime(2003, 3, 23, 16, 0, 0, 0, DateTimeKind.Utc) },
                    { 36, 64, 18, new DateTime(2002, 2, 18, 16, 0, 0, 0, DateTimeKind.Utc) },
                    { 37, 42, 5, new DateTime(2002, 4, 19, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { 38, 21, 4, new DateTime(2020, 4, 19, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 39, 77, 6, new DateTime(2019, 1, 31, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { 40, 62, 8, new DateTime(2024, 4, 13, 15, 0, 0, 0, DateTimeKind.Utc) },
                    { 41, 18, 5, new DateTime(2022, 12, 12, 20, 0, 0, 0, DateTimeKind.Utc) },
                    { 42, 30, 2, new DateTime(2007, 7, 30, 11, 0, 0, 0, DateTimeKind.Utc) },
                    { 43, 23, 18, new DateTime(2023, 3, 24, 20, 0, 0, 0, DateTimeKind.Utc) },
                    { 44, 51, 19, new DateTime(2013, 4, 20, 15, 0, 0, 0, DateTimeKind.Utc) },
                    { 45, 83, 8, new DateTime(2015, 8, 27, 20, 0, 0, 0, DateTimeKind.Utc) },
                    { 46, 84, 18, new DateTime(2001, 2, 26, 22, 0, 0, 0, DateTimeKind.Utc) },
                    { 47, 23, 19, new DateTime(2020, 8, 28, 13, 0, 0, 0, DateTimeKind.Utc) },
                    { 48, 17, 7, new DateTime(2003, 7, 13, 20, 0, 0, 0, DateTimeKind.Utc) },
                    { 49, 15, 17, new DateTime(2005, 1, 19, 21, 0, 0, 0, DateTimeKind.Utc) },
                    { 50, 84, 8, new DateTime(2013, 4, 30, 16, 0, 0, 0, DateTimeKind.Utc) },
                    { 51, 64, 17, new DateTime(2017, 10, 25, 17, 0, 0, 0, DateTimeKind.Utc) },
                    { 52, 76, 13, new DateTime(2005, 2, 6, 22, 0, 0, 0, DateTimeKind.Utc) },
                    { 53, 37, 1, new DateTime(2003, 9, 4, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 54, 28, 3, new DateTime(2006, 11, 21, 17, 0, 0, 0, DateTimeKind.Utc) },
                    { 55, 15, 16, new DateTime(2000, 6, 16, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 56, 94, 4, new DateTime(2013, 10, 28, 23, 0, 0, 0, DateTimeKind.Utc) },
                    { 57, 43, 8, new DateTime(2018, 8, 6, 11, 0, 0, 0, DateTimeKind.Utc) },
                    { 58, 35, 6, new DateTime(2025, 1, 9, 17, 0, 0, 0, DateTimeKind.Utc) },
                    { 59, 51, 3, new DateTime(2001, 8, 9, 15, 0, 0, 0, DateTimeKind.Utc) },
                    { 60, 13, 15, new DateTime(2014, 1, 10, 17, 0, 0, 0, DateTimeKind.Utc) },
                    { 61, 72, 4, new DateTime(2016, 9, 28, 14, 0, 0, 0, DateTimeKind.Utc) },
                    { 62, 80, 17, new DateTime(2005, 6, 29, 18, 0, 0, 0, DateTimeKind.Utc) },
                    { 63, 56, 3, new DateTime(2010, 2, 6, 16, 0, 0, 0, DateTimeKind.Utc) },
                    { 64, 96, 6, new DateTime(2014, 5, 30, 18, 0, 0, 0, DateTimeKind.Utc) },
                    { 65, 97, 3, new DateTime(2005, 4, 25, 9, 0, 0, 0, DateTimeKind.Utc) },
                    { 66, 1, 15, new DateTime(2010, 12, 27, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 67, 19, 13, new DateTime(2013, 4, 9, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 68, 25, 15, new DateTime(2025, 3, 8, 15, 0, 0, 0, DateTimeKind.Utc) },
                    { 69, 48, 2, new DateTime(2012, 4, 30, 8, 0, 0, 0, DateTimeKind.Utc) },
                    { 70, 37, 15, new DateTime(2024, 2, 8, 16, 0, 0, 0, DateTimeKind.Utc) },
                    { 71, 81, 5, new DateTime(2003, 5, 10, 16, 0, 0, 0, DateTimeKind.Utc) },
                    { 72, 57, 19, new DateTime(2008, 8, 30, 14, 0, 0, 0, DateTimeKind.Utc) },
                    { 73, 2, 9, new DateTime(2005, 4, 16, 15, 0, 0, 0, DateTimeKind.Utc) },
                    { 74, 89, 13, new DateTime(2003, 10, 27, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 75, 22, 7, new DateTime(2009, 12, 12, 11, 0, 0, 0, DateTimeKind.Utc) },
                    { 76, 87, 13, new DateTime(2022, 4, 17, 18, 0, 0, 0, DateTimeKind.Utc) },
                    { 77, 25, 16, new DateTime(2007, 6, 26, 13, 0, 0, 0, DateTimeKind.Utc) },
                    { 78, 97, 11, new DateTime(2003, 9, 11, 18, 0, 0, 0, DateTimeKind.Utc) },
                    { 79, 28, 8, new DateTime(2007, 10, 8, 14, 0, 0, 0, DateTimeKind.Utc) },
                    { 80, 2, 17, new DateTime(2005, 10, 30, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { 81, 74, 12, new DateTime(2003, 11, 8, 13, 0, 0, 0, DateTimeKind.Utc) },
                    { 82, 48, 17, new DateTime(2004, 3, 1, 19, 0, 0, 0, DateTimeKind.Utc) },
                    { 83, 92, 2, new DateTime(2001, 12, 16, 11, 0, 0, 0, DateTimeKind.Utc) },
                    { 84, 18, 4, new DateTime(2018, 11, 26, 16, 0, 0, 0, DateTimeKind.Utc) },
                    { 85, 36, 10, new DateTime(2011, 2, 12, 23, 0, 0, 0, DateTimeKind.Utc) },
                    { 86, 80, 1, new DateTime(2015, 7, 2, 10, 0, 0, 0, DateTimeKind.Utc) },
                    { 87, 98, 9, new DateTime(2001, 3, 2, 23, 0, 0, 0, DateTimeKind.Utc) },
                    { 88, 63, 14, new DateTime(2011, 3, 14, 22, 0, 0, 0, DateTimeKind.Utc) },
                    { 89, 13, 7, new DateTime(2025, 12, 31, 20, 0, 0, 0, DateTimeKind.Utc) },
                    { 90, 19, 8, new DateTime(2009, 8, 10, 12, 0, 0, 0, DateTimeKind.Utc) },
                    { 91, 83, 4, new DateTime(2007, 4, 21, 19, 0, 0, 0, DateTimeKind.Utc) },
                    { 92, 26, 4, new DateTime(2000, 11, 18, 16, 0, 0, 0, DateTimeKind.Utc) },
                    { 93, 45, 5, new DateTime(2017, 12, 27, 19, 0, 0, 0, DateTimeKind.Utc) },
                    { 94, 48, 1, new DateTime(2022, 12, 26, 17, 0, 0, 0, DateTimeKind.Utc) },
                    { 95, 46, 14, new DateTime(2013, 12, 29, 14, 0, 0, 0, DateTimeKind.Utc) },
                    { 96, 1, 10, new DateTime(2011, 5, 3, 19, 0, 0, 0, DateTimeKind.Utc) },
                    { 97, 44, 13, new DateTime(2025, 3, 25, 14, 0, 0, 0, DateTimeKind.Utc) },
                    { 98, 82, 3, new DateTime(2019, 5, 25, 18, 0, 0, 0, DateTimeKind.Utc) },
                    { 99, 44, 14, new DateTime(2014, 9, 19, 14, 0, 0, 0, DateTimeKind.Utc) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_movie_screenings_movie_id",
                table: "movie_screenings",
                column: "movie_id");

            migrationBuilder.CreateIndex(
                name: "IX_movie_screenings_theater_id",
                table: "movie_screenings",
                column: "theater_id");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_customer_id",
                table: "Tickets",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_screening_id_ScreeningMovieId_ScreeningTheaterId",
                table: "Tickets",
                columns: new[] { "screening_id", "ScreeningMovieId", "ScreeningTheaterId" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "movie_screenings");

            migrationBuilder.DropTable(
                name: "playable_movies");

            migrationBuilder.DropTable(
                name: "theater_rooms");
        }
    }
}
