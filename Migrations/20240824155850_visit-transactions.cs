using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AahanaClinic.Migrations
{
    public partial class visittransactions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VisitTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EncounterId = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisitTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_VisitTransactions_Encounters_EncounterId",
                        column: x => x.EncounterId,
                        principalTable: "Encounters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_VisitTransactions_EncounterId",
                table: "VisitTransactions",
                column: "EncounterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VisitTransactions");

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
    }
}
