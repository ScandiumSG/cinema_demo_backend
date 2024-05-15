using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace cinemaServer.Migrations
{
    /// <inheritdoc />
    public partial class predefinedUsers2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: "d83bec77-fd53-4daa-ae27-6c0fe3de5dfb");

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "role", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "ceb66d9b-aeed-4994-ab4e-3a9bb04f3d2f", 0, "fc257e65-4bdf-492d-a875-9e0094e65e9a", "test@user.com", true, false, null, "TEST@USER.COM", "TESTUSER", "AQAAAAIAAYagAAAAEHDoZnr8YlF0U5+zCO2lCvX0A3pLts8LnjocdUnJGh4mPLHUfrUzAxKd9/7EETiHWg==", null, false, 0, "", false, "testuser" },
                    { "f613b260-3b13-45ec-bb14-6a6e725c330a", 0, "0857df88-fb33-44d7-9677-3e7485e7e31e", "Admin@cinema.com", true, false, null, "ADMIN@CINEMA.COM", "ADMINUSER", "AQAAAAIAAYagAAAAEOTSZ7hzSKFln7+TC3FG0DB2Zj3OfXZG+p0Tckt7Dp84e8kiEs7Iwr+/sJmBPCNZ3A==", null, false, 2, "", false, "Adminuser" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: "ceb66d9b-aeed-4994-ab4e-3a9bb04f3d2f");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: "f613b260-3b13-45ec-bb14-6a6e725c330a");

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "role", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d83bec77-fd53-4daa-ae27-6c0fe3de5dfb", 0, "290e9f85-5c67-4be6-9369-fe75270c122a", null, false, false, null, null, null, null, null, false, 0, "d3758524-fb4a-4b41-b004-7551957c175a", false, null });
        }
    }
}
