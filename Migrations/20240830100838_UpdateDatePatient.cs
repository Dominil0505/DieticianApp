using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DieticianApp.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatePatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Disease_Diseases_DiseaseId",
                table: "Patient_Disease");

            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Disease_Patients_PatientId",
                table: "Patient_Disease");

            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Medication_Medicines_MedicationId",
                table: "Patient_Medication");

            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Medication_Patients_PatientId",
                table: "Patient_Medication");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Patient_Medication",
                table: "Patient_Medication");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Patient_Disease",
                table: "Patient_Disease");

            migrationBuilder.RenameTable(
                name: "Patient_Medication",
                newName: "Patient_Medications");

            migrationBuilder.RenameTable(
                name: "Patient_Disease",
                newName: "Patient_Diseases");

            migrationBuilder.RenameIndex(
                name: "IX_Patient_Medication_MedicationId",
                table: "patient_Medications",
                newName: "IX_patient_Medications_MedicationId");

            migrationBuilder.RenameIndex(
                name: "IX_Patient_Disease_DiseaseId",
                table: "Patient_Diseases",
                newName: "IX_Patient_Diseases_DiseaseId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DoB",
                table: "Patients",
                type: "datetime2",
                maxLength: 110,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(110)",
                oldMaxLength: 110,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_patient_Medications",
                table: "patient_Medications",
                columns: new[] { "PatientId", "MedicationId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Patient_Diseases",
                table: "Patient_Diseases",
                columns: new[] { "PatientId", "DiseaseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Diseases_Diseases_DiseaseId",
                table: "Patient_Diseases",
                column: "DiseaseId",
                principalTable: "Diseases",
                principalColumn: "Disease_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Diseases_Patients_PatientId",
                table: "Patient_Diseases",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Patient_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_patient_Medications_Medicines_MedicationId",
                table: "patient_Medications",
                column: "MedicationId",
                principalTable: "Medicines",
                principalColumn: "Medicine_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_patient_Medications_Patients_PatientId",
                table: "patient_Medications",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Patient_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Diseases_Diseases_DiseaseId",
                table: "Patient_Diseases");

            migrationBuilder.DropForeignKey(
                name: "FK_Patient_Diseases_Patients_PatientId",
                table: "Patient_Diseases");

            migrationBuilder.DropForeignKey(
                name: "FK_patient_Medications_Medicines_MedicationId",
                table: "patient_Medications");

            migrationBuilder.DropForeignKey(
                name: "FK_patient_Medications_Patients_PatientId",
                table: "patient_Medications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_patient_Medications",
                table: "patient_Medications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Patient_Diseases",
                table: "Patient_Diseases");

            migrationBuilder.RenameTable(
                name: "patient_Medications",
                newName: "Patient_Medication");

            migrationBuilder.RenameTable(
                name: "Patient_Diseases",
                newName: "Patient_Disease");

            migrationBuilder.RenameIndex(
                name: "IX_patient_Medications_MedicationId",
                table: "Patient_Medication",
                newName: "IX_Patient_Medication_MedicationId");

            migrationBuilder.RenameIndex(
                name: "IX_Patient_Diseases_DiseaseId",
                table: "Patient_Disease",
                newName: "IX_Patient_Disease_DiseaseId");

            migrationBuilder.AlterColumn<string>(
                name: "DoB",
                table: "Patients",
                type: "nvarchar(110)",
                maxLength: 110,
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldMaxLength: 110,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Patient_Medication",
                table: "Patient_Medication",
                columns: new[] { "PatientId", "MedicationId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Patient_Disease",
                table: "Patient_Disease",
                columns: new[] { "PatientId", "DiseaseId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Disease_Diseases_DiseaseId",
                table: "Patient_Disease",
                column: "DiseaseId",
                principalTable: "Diseases",
                principalColumn: "Disease_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Disease_Patients_PatientId",
                table: "Patient_Disease",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Patient_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Medication_Medicines_MedicationId",
                table: "Patient_Medication",
                column: "MedicationId",
                principalTable: "Medicines",
                principalColumn: "Medicine_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_Medication_Patients_PatientId",
                table: "Patient_Medication",
                column: "PatientId",
                principalTable: "Patients",
                principalColumn: "Patient_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
