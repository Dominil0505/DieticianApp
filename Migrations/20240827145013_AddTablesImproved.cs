using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DieticianApp.Migrations
{
    /// <inheritdoc />
    public partial class AddTablesImproved : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Dieticians_Dietician_Id",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_Dietician_Id",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Dietician_Id",
                table: "Patients");

            migrationBuilder.RenameColumn(
                name: "Dietician_id",
                table: "Patients",
                newName: "Dietician_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Dietician_Id",
                table: "Patients",
                column: "Dietician_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Dieticians_Dietician_Id",
                table: "Patients",
                column: "Dietician_Id",
                principalTable: "Dieticians",
                principalColumn: "Dietician_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Dieticians_Dietician_Id",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Patients_Dietician_Id",
                table: "Patients");

            migrationBuilder.RenameColumn(
                name: "Dietician_Id",
                table: "Patients",
                newName: "Dietician_id");

            migrationBuilder.AddColumn<int>(
                name: "Dietician_Id",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Dietician_Id",
                table: "Patients",
                column: "Dietician_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Dieticians_Dietician_Id",
                table: "Patients",
                column: "Dietician_Id",
                principalTable: "Dieticians",
                principalColumn: "Dietician_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
