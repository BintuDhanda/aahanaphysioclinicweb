using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AahanaClinic.Migrations
{
    public partial class packageid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VisitBalance",
                table: "Patients");

            migrationBuilder.AddColumn<int>(
                name: "PackageId",
                table: "Encounters",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Encounters_PackageId",
                table: "Encounters",
                column: "PackageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Encounters_Packages_PackageId",
                table: "Encounters",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Encounters_Packages_PackageId",
                table: "Encounters");

            migrationBuilder.DropIndex(
                name: "IX_Encounters_PackageId",
                table: "Encounters");

            migrationBuilder.DropColumn(
                name: "PackageId",
                table: "Encounters");

            migrationBuilder.AddColumn<int>(
                name: "VisitBalance",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b241f5b3-1bdf-4f41-9cef-f7c78664bc80",
                column: "ConcurrencyStamp",
                value: "2d615175-8ff8-411d-a5a2-0449bf16f66c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "61cc032a-985c-44c9-8aeb-8d2dc5d9626a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d9d04aa8-bd6f-4e05-acad-0545ba791995", "AQAAAAEAACcQAAAAEHgS9gdeK7aYQ7XZH9sHcktJvF+JGD0opBQegZyKv8ZyPxIPdF6lrJLQekCMYZGy4g==", "80296cf1-8cb9-4870-9b17-659fc36dcb75" });
        }
    }
}
