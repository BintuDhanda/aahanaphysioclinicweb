using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aahanaphysioclinic.Migrations
{
    public partial class PatientAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MobileNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b241f5b3-1bdf-4f41-9cef-f7c78664bc80",
                column: "ConcurrencyStamp",
                value: "0cd593de-12e1-4e51-b4d8-c65a5cfa81c7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "61cc032a-985c-44c9-8aeb-8d2dc5d9626a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "85d86a4f-006e-4462-834d-a33024e42b92", "AQAAAAEAACcQAAAAEGdB3ZPOmIzu7VWAC6keBTVdl2AHb7sPF5xH8sZugpAtA79qR1VWqvfj7Tnh3Y7+XA==", "47ee2955-34f7-4ce5-9ced-6f5ba408116d" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b241f5b3-1bdf-4f41-9cef-f7c78664bc80",
                column: "ConcurrencyStamp",
                value: "c60f15f9-2f4f-48fc-82bd-e113ae9c79fb");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "61cc032a-985c-44c9-8aeb-8d2dc5d9626a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5c595cb3-013e-428b-8555-656b9c534051", "AQAAAAEAACcQAAAAEO/4LL86OI3+YMLS8EsSJKxpf1Fic9knGW6eKdxqttUUZouGXkMDNB3a/1JgZ3p/cg==", "8722bc68-76e8-4033-afbc-c56f2d9afe88" });
        }
    }
}
