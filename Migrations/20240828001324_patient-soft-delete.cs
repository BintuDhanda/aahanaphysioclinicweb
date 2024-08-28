using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AahanaClinic.Migrations
{
    public partial class patientsoftdelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Patients",
                type: "bit",
                nullable: false,
                defaultValue: false);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Patients");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b241f5b3-1bdf-4f41-9cef-f7c78664bc80",
                column: "ConcurrencyStamp",
                value: "674f53ce-a0f3-4fb8-9ce6-a8f591629a3d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "61cc032a-985c-44c9-8aeb-8d2dc5d9626a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "684b7481-760a-459b-87c2-f7867a63620e", "AQAAAAEAACcQAAAAEMPK6i7kNWkW4Sb7Ryy3Pq8i6lRCWk2eAhE+LK3ZoRHmAaXFhGnJNpR+VtTv/RKIeA==", "3ea3f490-2e86-43c8-8228-a5ad1c7b380d" });
        }
    }
}
