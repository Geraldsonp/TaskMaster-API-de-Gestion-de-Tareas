using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Issues.Manager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedSomePrepertiesNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b93eee33-3455-4495-88de-7b1572a7a7e4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bc5f4fa8-82a7-409f-abe8-49db8ef5c2f1");

            migrationBuilder.RenameColumn(
                name: "Completed",
                table: "Issues",
                newName: "CompletedAt");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Issues",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Issues",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "02a9cb06-b6cd-4646-91ac-3f69479133e5", "5c101ea7-827c-4234-a7de-946bada9d032", "Administrator", "ADMINISTRATOR" },
                    { "2dc0ac99-042d-468b-b48b-479860ed85cd", "659dab86-69a0-4404-8a49-a11af9b21ef4", "Manager", "MANAGER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "02a9cb06-b6cd-4646-91ac-3f69479133e5");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2dc0ac99-042d-468b-b48b-479860ed85cd");

            migrationBuilder.RenameColumn(
                name: "CompletedAt",
                table: "Issues",
                newName: "Completed");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Issues",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Issues",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b93eee33-3455-4495-88de-7b1572a7a7e4", "9346b01c-b91b-4d79-9e93-4edcabf83cfb", "Administrator", "ADMINISTRATOR" },
                    { "bc5f4fa8-82a7-409f-abe8-49db8ef5c2f1", "78656144-fc80-4c96-bbf3-0caa1f699a96", "Manager", "MANAGER" }
                });
        }
    }
}
