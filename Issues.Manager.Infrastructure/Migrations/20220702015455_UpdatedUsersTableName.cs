using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Issues.Manager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedUsersTableName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Users_UserId",
                table: "Issues");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_AspNetUsers_IdentityId",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "AppUsers");

            migrationBuilder.RenameIndex(
                name: "IX_Users_IdentityId",
                table: "AppUsers",
                newName: "IX_AppUsers_IdentityId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Issues",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppUsers",
                table: "AppUsers",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "b93eee33-3455-4495-88de-7b1572a7a7e4", "9346b01c-b91b-4d79-9e93-4edcabf83cfb", "Administrator", "ADMINISTRATOR" },
                    { "bc5f4fa8-82a7-409f-abe8-49db8ef5c2f1", "78656144-fc80-4c96-bbf3-0caa1f699a96", "Manager", "MANAGER" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_AspNetUsers_IdentityId",
                table: "AppUsers",
                column: "IdentityId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_AppUsers_UserId",
                table: "Issues",
                column: "UserId",
                principalTable: "AppUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_AspNetUsers_IdentityId",
                table: "AppUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Issues_AppUsers_UserId",
                table: "Issues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppUsers",
                table: "AppUsers");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b93eee33-3455-4495-88de-7b1572a7a7e4");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bc5f4fa8-82a7-409f-abe8-49db8ef5c2f1");

            migrationBuilder.RenameTable(
                name: "AppUsers",
                newName: "Users");

            migrationBuilder.RenameIndex(
                name: "IX_AppUsers_IdentityId",
                table: "Users",
                newName: "IX_Users_IdentityId");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Issues",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Users_UserId",
                table: "Issues",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_AspNetUsers_IdentityId",
                table: "Users",
                column: "IdentityId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
