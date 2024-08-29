using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AahanaClinic.Migrations
{
    public partial class updatelabreport : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabReports_Encounters_EncounterId",
                table: "LabReports");

            migrationBuilder.RenameColumn(
                name: "EncounterId",
                table: "LabReports",
                newName: "PatientId");

            migrationBuilder.RenameIndex(
                name: "IX_LabReports_EncounterId",
                table: "LabReports",
                newName: "IX_LabReports_PatientId");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b241f5b3-1bdf-4f41-9cef-f7c78664bc80",
                column: "ConcurrencyStamp",
                value: "e147e2e2-745f-4753-b86f-4d75fe21e2aa");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "61cc032a-985c-44c9-8aeb-8d2dc5d9626a",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "062a06ee-2b1f-4256-88f4-9cc59bdc12bd", "AQAAAAEAACcQAAAAEMggEp1IqpbeDo3QBZkigaLNC/ssoo00wCZM5YNn5YT8WOC+tAqf7B1i60W3u5tasg==", "d0d5e955-135c-48c2-b799-9e413920cf9b" });

            migrationBuilder.AddForeignKey(
                name: "FK_LabReports_Patients_PatientId",
                table: "LabReports",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LabReports_Patients_PatientId",
                table: "LabReports");

            migrationBuilder.RenameColumn(
                name: "PatientId",
                table: "LabReports",
                newName: "EncounterId");

            migrationBuilder.RenameIndex(
                name: "IX_LabReports_PatientId",
                table: "LabReports",
                newName: "IX_LabReports_EncounterId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_LabReports_Encounters_EncounterId",
                table: "LabReports",
                column: "EncounterId",
                principalTable: "Encounters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
