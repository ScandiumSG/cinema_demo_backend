using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cinemaServer.Migrations
{
    /// <inheritdoc />
    public partial class predefinedUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "role", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "d83bec77-fd53-4daa-ae27-6c0fe3de5dfb", 0, "290e9f85-5c67-4be6-9369-fe75270c122a", null, false, false, null, null, null, null, null, false, 0, "d3758524-fb4a-4b41-b004-7551957c175a", false, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "users",
                keyColumn: "Id",
                keyValue: "d83bec77-fd53-4daa-ae27-6c0fe3de5dfb");
        }
    }
}
