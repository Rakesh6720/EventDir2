using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class AddMigrateAsync : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "54811444-0469-41ba-83ae-880d3a2fcfaf");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9274c75c-b907-44ea-bdc4-13f6c7eb1e59");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "367cea45-4f91-40d6-a111-776b0d31032c", null, "Admin", "ADMIN" },
                    { "a12a83e3-d447-4121-9714-d1ade94ac810", null, "Member", "MEMBER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "367cea45-4f91-40d6-a111-776b0d31032c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a12a83e3-d447-4121-9714-d1ade94ac810");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "54811444-0469-41ba-83ae-880d3a2fcfaf", null, "Admin", "ADMIN" },
                    { "9274c75c-b907-44ea-bdc4-13f6c7eb1e59", null, "Member", "MEMBER" }
                });
        }
    }
}
