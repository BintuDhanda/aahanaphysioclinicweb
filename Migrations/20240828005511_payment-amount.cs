using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AahanaClinic.Migrations
{
    public partial class paymentamount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "Payments",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b241f5b3-1bdf-4f41-9cef-f7c78664bc80",
                column: "ConcurrencyStamp",
                value: "ec8f9a39-bbe8-4ee5-98fd-790c0352989a");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "61cc032a-985c-44c9-8aeb-8d2dc5d9626a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8c77c4d9-7481-4678-9687-e18415c09d6a", "AQAAAAEAACcQAAAAEEbyclEBtRHkkTti8nz1wM+gd7uRBxjLPyyNOLUoOLC4oVMqqcCvDMalmq596op6OA==", "e754fa54-50a0-41a3-a7f8-f6b2b344ae4e" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Payments");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b241f5b3-1bdf-4f41-9cef-f7c78664bc80",
                column: "ConcurrencyStamp",
                value: "fdc0be12-6568-4d41-938d-b5fc98ae9b63");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "61cc032a-985c-44c9-8aeb-8d2dc5d9626a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a214d11f-a9c3-4a29-8fb6-03b9f3b1aa23", "AQAAAAEAACcQAAAAEPkY/SW1naZNipKA58Me/OhSKrT1dbJrnjlBU9p/0PPdX8mvnHSG+NWLhcAvpSzRuw==", "262a8049-b6eb-44aa-aa90-e7f31fd29060" });
        }
    }
}
