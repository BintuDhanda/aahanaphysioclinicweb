using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AahanaClinic.Migrations
{
    public partial class accountantsignature : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AccountantSignature",
                table: "Settings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b241f5b3-1bdf-4f41-9cef-f7c78664bc80",
                column: "ConcurrencyStamp",
                value: "c025abe6-5351-4902-a220-b88fa2028411");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "61cc032a-985c-44c9-8aeb-8d2dc5d9626a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cb9ada4b-16e8-4b2f-80aa-f19c636e5ae1", "AQAAAAEAACcQAAAAEJAzJrhTzOtFvMy2dsJ4ZrcTwp22xtVHIw7uE4SSSIbjx7AkfH3PgILZ+X/r2ZdHww==", "9b45ce22-e94e-4f9d-b25a-d2e8386bd0d2" });

            migrationBuilder.CreateIndex(
                name: "IX_Settings_AccountantSignature",
                table: "Settings",
                column: "AccountantSignature");

            migrationBuilder.AddForeignKey(
                name: "FK_Settings_FileStorage_AccountantSignature",
                table: "Settings",
                column: "AccountantSignature",
                principalTable: "FileStorage",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Settings_FileStorage_AccountantSignature",
                table: "Settings");

            migrationBuilder.DropIndex(
                name: "IX_Settings_AccountantSignature",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "AccountantSignature",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Settings");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b241f5b3-1bdf-4f41-9cef-f7c78664bc80",
                column: "ConcurrencyStamp",
                value: "3ba3693e-7f88-4191-8c2f-9cb0f5ec3ffe");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "61cc032a-985c-44c9-8aeb-8d2dc5d9626a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a08b22a6-42c1-4a70-985d-08bf9faee7c9", "AQAAAAEAACcQAAAAEJfGllcF510gbFA0Ov3UuEV/qPKtdjQANpYHLbXqLydRriFB2fZvGz5WM9lXlQMXbQ==", "2745256c-6979-416d-8651-12c3de4db9c1" });
        }
    }
}
