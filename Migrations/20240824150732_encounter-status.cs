using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AahanaClinic.Migrations
{
    public partial class encounterstatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Encounters",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b241f5b3-1bdf-4f41-9cef-f7c78664bc80",
                column: "ConcurrencyStamp",
                value: "d518bd5e-675c-4a4d-aa12-b3c1af5fb097");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "61cc032a-985c-44c9-8aeb-8d2dc5d9626a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0a1e4bb5-1555-41a5-8f35-ff7dd475bb52", "AQAAAAEAACcQAAAAEFcFrxQGFtBXyvVPc8Jd9uI+xLx03JkOuiiAzgUsc7OMLl7NPWz69Dj+xBOYfr0Z0A==", "470b5beb-27af-48c0-8d44-f5e5576931f0" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Encounters");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b241f5b3-1bdf-4f41-9cef-f7c78664bc80",
                column: "ConcurrencyStamp",
                value: "20a45aa0-4f69-4d15-826a-b0aed7d1d204");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "61cc032a-985c-44c9-8aeb-8d2dc5d9626a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0e15d62d-c7c9-4617-a968-f952d0b8ae4a", "AQAAAAEAACcQAAAAEOWi5pNKUz8f0Z8ug8ppComXr9yRlAdKnGfj5PBKntSFuhF+QL92/hgzIsNc835nxw==", "5aba7b38-1717-45d8-b61a-bcbda666f06e" });
        }
    }
}
