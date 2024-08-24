using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AahanaClinic.Migrations
{
    public partial class paymentvisits : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Visits",
                table: "Payments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b241f5b3-1bdf-4f41-9cef-f7c78664bc80",
                column: "ConcurrencyStamp",
                value: "52562b43-0c49-4c72-acf1-adea77d26fef");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "61cc032a-985c-44c9-8aeb-8d2dc5d9626a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8f80da73-bd43-449d-a6fa-b77466ed1856", "AQAAAAEAACcQAAAAEFXPhn+XymYfutmh4Ci5hMOLcDkOQhhTeo7LnUy+hwpD6AdEcnrQjYBXXNazKSnofQ==", "a5212af6-384d-4338-aa73-a4f57be38e6a" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Visits",
                table: "Payments");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b241f5b3-1bdf-4f41-9cef-f7c78664bc80",
                column: "ConcurrencyStamp",
                value: "7af0a381-297b-4c4b-9f7b-d780f384e958");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "61cc032a-985c-44c9-8aeb-8d2dc5d9626a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aead4679-6cc9-40a1-921b-f0f05fc4f85e", "AQAAAAEAACcQAAAAEKPBv8e/6Ba1G2GBDrXr9CScZKJQfFv3OE9zZVSFHNAmk1PMhdCUogET+vMvyKE5OA==", "38cbfdd5-4e80-4ce9-9a8b-f7c62528df6f" });
        }
    }
}
