using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DieticianApp.Migrations
{
    /// <inheritdoc />
    public partial class ChangeConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Patients_Patient_Name",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Dieticians_Dietician_Name",
                table: "Dieticians");

            migrationBuilder.AlterColumn<string>(
                name: "Medicine_Name",
                table: "Medicines",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Disease_Name",
                table: "Diseases",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Allergy_Name",
                table: "Allergy",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Patient_Name",
                table: "Patients",
                column: "Patient_Name");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_Menu_Name",
                table: "Menus",
                column: "Menu_Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Medicines_Medicine_Name",
                table: "Medicines",
                column: "Medicine_Name",
                unique: true,
                filter: "[Medicine_Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Diseases_Disease_Name",
                table: "Diseases",
                column: "Disease_Name",
                unique: true,
                filter: "[Disease_Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Dieticians_Dietician_Name",
                table: "Dieticians",
                column: "Dietician_Name");

            migrationBuilder.CreateIndex(
                name: "IX_Allergy_Allergy_Name",
                table: "Allergy",
                column: "Allergy_Name",
                unique: true,
                filter: "[Allergy_Name] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Patients_Patient_Name",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Menus_Menu_Name",
                table: "Menus");

            migrationBuilder.DropIndex(
                name: "IX_Medicines_Medicine_Name",
                table: "Medicines");

            migrationBuilder.DropIndex(
                name: "IX_Diseases_Disease_Name",
                table: "Diseases");

            migrationBuilder.DropIndex(
                name: "IX_Dieticians_Dietician_Name",
                table: "Dieticians");

            migrationBuilder.DropIndex(
                name: "IX_Allergy_Allergy_Name",
                table: "Allergy");

            migrationBuilder.AlterColumn<string>(
                name: "Medicine_Name",
                table: "Medicines",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Disease_Name",
                table: "Diseases",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Allergy_Name",
                table: "Allergy",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Patient_Name",
                table: "Patients",
                column: "Patient_Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dieticians_Dietician_Name",
                table: "Dieticians",
                column: "Dietician_Name",
                unique: true);
        }
    }
}
