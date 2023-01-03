using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Issues.Manager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dataseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "347c4128-03ef-4fcc-a851-763787998fd0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5c0c0dc4-293f-48c6-aeae-1501070968af");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d0a755e8-3e30-4f14-afe4-a9e3ac739139");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d42b77d1-a210-4e9f-af98-25ad0b10b821");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3bfff56e-e2c0-46db-9502-67c347193ad7", "8fff6800-6c6f-4d7f-b680-2d1d1c151420", "Administrator", "ADMINISTRATOR" },
                    { "ba485996-f503-44f0-a6ce-b7650dc11f5d", "bf7456a8-440d-46e8-ac25-6207679265d1", "Project Manager", "PROJECT MANAGER" },
                    { "e504828b-3a0b-4e04-b098-57e0a47193eb", "1ebf1dfd-6aaf-42e5-82fc-bbae509e0446", "Developer", "DEVELOPER" },
                    { "ed14ec3c-2f6d-4520-aea6-12d6513e29ec", "0f223817-c16c-4b27-a549-54721d4ca496", "Quality Assurance", "Quality Assurance" }
                });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3bfff56e-e2c0-46db-9502-67c347193ad7");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ba485996-f503-44f0-a6ce-b7650dc11f5d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e504828b-3a0b-4e04-b098-57e0a47193eb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ed14ec3c-2f6d-4520-aea6-12d6513e29ec");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "347c4128-03ef-4fcc-a851-763787998fd0", "a6d6391a-efec-4d87-918b-3fefe44332cb", "Administrator", "ADMINISTRATOR" },
                    { "5c0c0dc4-293f-48c6-aeae-1501070968af", "45bd2626-6fd6-4278-9a71-10f0042a166a", "Project Manager", "PROJECT MANAGER" },
                    { "d0a755e8-3e30-4f14-afe4-a9e3ac739139", "15811e4d-b6f0-412d-ba6e-a6ded08b96fe", "Quality Assurance", "Quality Assurance" },
                    { "d42b77d1-a210-4e9f-af98-25ad0b10b821", "f7bfafe7-bc9a-4e4f-9a25-f85d7188e668", "Developer", "DEVELOPER" }
                });
        }
    }
}