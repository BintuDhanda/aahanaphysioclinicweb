using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AahanaClinic.Migrations
{
    public partial class settings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Settings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Logo = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Signature = table.Column<int>(type: "int", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Settings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Settings_FileStorage_Logo",
                        column: x => x.Logo,
                        principalTable: "FileStorage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Settings_FileStorage_Signature",
                        column: x => x.Signature,
                        principalTable: "FileStorage",
                        principalColumn: "Id");
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_Settings_Logo",
                table: "Settings",
                column: "Logo");

            migrationBuilder.CreateIndex(
                name: "IX_Settings_Signature",
                table: "Settings",
                column: "Signature");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Settings");

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
    }
}
