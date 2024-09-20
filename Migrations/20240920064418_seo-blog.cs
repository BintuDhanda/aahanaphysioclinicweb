using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AahanaClinic.Migrations
{
    public partial class seoblog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetaTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetaKeyword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetaDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Seos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Page = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MetaTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetaKeyword = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetaDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seos", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b241f5b3-1bdf-4f41-9cef-f7c78664bc80",
                column: "ConcurrencyStamp",
                value: "7c42af96-e478-4a2e-8586-49d283376ea9");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "61cc032a-985c-44c9-8aeb-8d2dc5d9626a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fd70088d-5ce7-46fe-8b95-df3676095e34", "AQAAAAEAACcQAAAAEEtYpIusO+J6wa9RLB4r2McoD1m00Jrzqz7RUYQ2KaxHFt1O/3IEARIsx3Q3E3S0aA==", "0b311c4b-0df4-4641-8c14-0aeaf2f23d07" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blogs");

            migrationBuilder.DropTable(
                name: "Seos");

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
    }
}
