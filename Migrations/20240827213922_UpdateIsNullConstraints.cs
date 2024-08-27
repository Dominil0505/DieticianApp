using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DieticianApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIsNullConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menus_Dieticians_Dietician_Id",
                table: "Menus");

            migrationBuilder.DropForeignKey(
                name: "FK_Menus_Patients_Patient_Id",
                table: "Menus");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Dieticians_Dietician_Id",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Menus_Patient_Id",
                table: "Menus");

            migrationBuilder.AlterColumn<int>(
                name: "Dietician_Id",
                table: "Patients",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Patient_Id",
                table: "Menus",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Menu_Start",
                table: "Menus",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Menu_End",
                table: "Menus",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<int>(
                name: "Dietician_Id",
                table: "Menus",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_Patient_Id",
                table: "Menus",
                column: "Patient_Id",
                unique: true,
                filter: "[Patient_Id] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_Dieticians_Dietician_Id",
                table: "Menus",
                column: "Dietician_Id",
                principalTable: "Dieticians",
                principalColumn: "Dietician_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_Patients_Patient_Id",
                table: "Menus",
                column: "Patient_Id",
                principalTable: "Patients",
                principalColumn: "Patient_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Patients_Dieticians_Dietician_Id",
                table: "Patients",
                column: "Dietician_Id",
                principalTable: "Dieticians",
                principalColumn: "Dietician_Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menus_Dieticians_Dietician_Id",
                table: "Menus");

            migrationBuilder.DropForeignKey(
                name: "FK_Menus_Patients_Patient_Id",
                table: "Menus");

            migrationBuilder.DropForeignKey(
                name: "FK_Patients_Dieticians_Dietician_Id",
                table: "Patients");

            migrationBuilder.DropIndex(
                name: "IX_Menus_Patient_Id",
                table: "Menus");

            migrationBuilder.AlterColumn<int>(
                name: "Dietician_Id",
                table: "Patients",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Patient_Id",
                table: "Menus",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Menu_Start",
                table: "Menus",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Menu_End",
                table: "Menus",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Dietician_Id",
                table: "Menus",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Menus_Patient_Id",
                table: "Menus",
                column: "Patient_Id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_Dieticians_Dietician_Id",
                table: "Menus",
                column: "Dietician_Id",
                principalTable: "Dieticians",
                principalColumn: "Dietician_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Menus_Patients_Patient_Id",
                table: "Menus",
                column: "Patient_Id",
                principalTable: "Patients",
                principalColumn: "Patient_Id",
                onDelete: ReferentialAction.Cascade);

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
