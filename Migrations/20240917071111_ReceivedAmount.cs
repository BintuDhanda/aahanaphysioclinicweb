using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AahanaClinic.Migrations
{
    public partial class ReceivedAmount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ReceivedAmount",
                table: "Packages",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b241f5b3-1bdf-4f41-9cef-f7c78664bc80",
                column: "ConcurrencyStamp",
                value: "923f700c-44cf-49a9-9a1b-ea8d4bee7d0a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "61cc032a-985c-44c9-8aeb-8d2dc5d9626a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d112180b-ebb3-4c4c-b8b0-74daa45fd61a", "AQAAAAEAACcQAAAAEEz5PDQLCNfK5zOvT1gPWmFtdpjGmvDIIkLmfrK9Fy0+iTrXRhefKp/9Z0yegONJpQ==", "75324def-880b-467a-a92c-92580fbaaee2" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceivedAmount",
                table: "Packages");

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
        }
    }
}
