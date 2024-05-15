using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace cinemaServer.Migrations
{
    /// <inheritdoc />
    public partial class predefinedUsers3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                values: new object[,]
                {
                    { "d34e655a-19a1-49f2-88c0-230260fe804b", 0, "11cce1df-b268-40f1-98f7-01c92eb131f1", "test@user.com", true, false, null, "TEST@USER.COM", "TESTUSER", "AQAAAAIAAYagAAAAELuQbNsT63q7dBWGWD6oAEu+J8rWJpG1n7ucOr+XGoGzT/BdkXySzgkGKR4e8RVzrQ==", null, false, 0, "GDRATSDFDAXNYNQKPNMCPLLYXOZCTEBL", false, "testuser" },
                    { "d5b2df57-2a6d-4695-a796-ef02553a01cb", 0, "d2b57a07-f006-41fb-aab8-4bf56d1ada9e", "Admin@cinema.com", true, false, null, "ADMIN@CINEMA.COM", "ADMINUSER", "AQAAAAIAAYagAAAAEJ8RiW9K6/cXK2JMpb3pdbUIp+rp5HUxD5uV2KLlNxMqs4z3bXJhtFAvJ/6XQitGFQ==", null, false, 2, "OQCUZGYYKGBJDINLTFRRAQSOUXMUXYXF", false, "Adminuser" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: "d34e655a-19a1-49f2-88c0-230260fe804b");

            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: "d5b2df57-2a6d-4695-a796-ef02553a01cb");

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "role", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "ceb66d9b-aeed-4994-ab4e-3a9bb04f3d2f", 0, "fc257e65-4bdf-492d-a875-9e0094e65e9a", "test@user.com", true, false, null, "TEST@USER.COM", "TESTUSER", "AQAAAAIAAYagAAAAEHDoZnr8YlF0U5+zCO2lCvX0A3pLts8LnjocdUnJGh4mPLHUfrUzAxKd9/7EETiHWg==", null, false, 0, "", false, "testuser" },
                    { "f613b260-3b13-45ec-bb14-6a6e725c330a", 0, "0857df88-fb33-44d7-9677-3e7485e7e31e", "Admin@cinema.com", true, false, null, "ADMIN@CINEMA.COM", "ADMINUSER", "AQAAAAIAAYagAAAAEOTSZ7hzSKFln7+TC3FG0DB2Zj3OfXZG+p0Tckt7Dp84e8kiEs7Iwr+/sJmBPCNZ3A==", null, false, 2, "", false, "Adminuser" }
                });
        }
    }
}
