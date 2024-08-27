using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DieticianApp.Migrations
{
    public partial class AddTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Allergy",
                columns: table => new
                {
                    Allergy_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Allergy_Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allergy", x => x.Allergy_Id);
                });

            migrationBuilder.CreateTable(
                name: "Dieticians",
                columns: table => new
                {
                    Dietician_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Dietician_Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Dietician_Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    AvatarUrL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dieticians", x => x.Dietician_Id);
                });

            migrationBuilder.CreateTable(
                name: "Diseases",
                columns: table => new
                {
                    Disease_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Disease_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Diagnosis_Date = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Diseases", x => x.Disease_Id);
                });

            migrationBuilder.CreateTable(
                name: "Foods",
                columns: table => new
                {
                    Food_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Food_Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Calorie = table.Column<int>(type: "int", nullable: true),
                    Protein = table.Column<int>(type: "int", nullable: true),
                    Fat = table.Column<int>(type: "int", nullable: true),
                    Carbohydrate = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Foods", x => x.Food_Id);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    Ingredient_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ingredient_Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Calorie = table.Column<int>(type: "int", nullable: false),
                    Protein = table.Column<int>(type: "int", nullable: false),
                    Fat = table.Column<int>(type: "int", nullable: false),
                    Carbohydrate = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.Ingredient_Id);
                });

            migrationBuilder.CreateTable(
                name: "Medicines",
                columns: table => new
                {
                    Medicine_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Medicine_Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.Medicine_Id);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Patient_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Patient_Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Patient_Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<byte>(type: "tinyint", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Height = table.Column<byte>(type: "tinyint", nullable: true),
                    Weight = table.Column<short>(type: "smallint", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Dietician_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Patient_Id);
                    table.ForeignKey(
                        name: "FK_Patients_Dieticians_Dietician_Id",
                        column: x => x.Dietician_Id,
                        principalTable: "Dieticians",
                        principalColumn: "Dietician_Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AllergyFood",
                columns: table => new
                {
                    AllergiesAllergy_Id = table.Column<int>(type: "int", nullable: false),
                    FoodsFood_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllergyFood", x => new { x.AllergiesAllergy_Id, x.FoodsFood_Id });
                    table.ForeignKey(
                        name: "FK_AllergyFood_Allergy_AllergiesAllergy_Id",
                        column: x => x.AllergiesAllergy_Id,
                        principalTable: "Allergy",
                        principalColumn: "Allergy_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AllergyFood_Foods_FoodsFood_Id",
                        column: x => x.FoodsFood_Id,
                        principalTable: "Foods",
                        principalColumn: "Food_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodIngredient",
                columns: table => new
                {
                    FoodsFood_Id = table.Column<int>(type: "int", nullable: false),
                    IngredientsIngredient_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodIngredient", x => new { x.FoodsFood_Id, x.IngredientsIngredient_Id });
                    table.ForeignKey(
                        name: "FK_FoodIngredient_Foods_FoodsFood_Id",
                        column: x => x.FoodsFood_Id,
                        principalTable: "Foods",
                        principalColumn: "Food_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodIngredient_Ingredients_IngredientsIngredient_Id",
                        column: x => x.IngredientsIngredient_Id,
                        principalTable: "Ingredients",
                        principalColumn: "Ingredient_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AllergyPatient",
                columns: table => new
                {
                    Allergy_Id = table.Column<int>(type: "int", nullable: false),
                    PatientsPatient_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllergyPatient", x => new { x.Allergy_Id, x.PatientsPatient_Id });
                    table.ForeignKey(
                        name: "FK_AllergyPatient_Allergy_Allergy_Id",
                        column: x => x.Allergy_Id,
                        principalTable: "Allergy",
                        principalColumn: "Allergy_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AllergyPatient_Patients_PatientsPatient_Id",
                        column: x => x.PatientsPatient_Id,
                        principalTable: "Patients",
                        principalColumn: "Patient_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DiseasePatient",
                columns: table => new
                {
                    DiseasesDisease_Id = table.Column<int>(type: "int", nullable: false),
                    PatientsPatient_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiseasePatient", x => new { x.DiseasesDisease_Id, x.PatientsPatient_Id });
                    table.ForeignKey(
                        name: "FK_DiseasePatient_Diseases_DiseasesDisease_Id",
                        column: x => x.DiseasesDisease_Id,
                        principalTable: "Diseases",
                        principalColumn: "Disease_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DiseasePatient_Patients_PatientsPatient_Id",
                        column: x => x.PatientsPatient_Id,
                        principalTable: "Patients",
                        principalColumn: "Patient_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MedicinePatient",
                columns: table => new
                {
                    MedicinesMedicine_Id = table.Column<int>(type: "int", nullable: false),
                    PatientsPatient_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicinePatient", x => new { x.MedicinesMedicine_Id, x.PatientsPatient_Id });
                    table.ForeignKey(
                        name: "FK_MedicinePatient_Medicines_MedicinesMedicine_Id",
                        column: x => x.MedicinesMedicine_Id,
                        principalTable: "Medicines",
                        principalColumn: "Medicine_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicinePatient_Patients_PatientsPatient_Id",
                        column: x => x.PatientsPatient_Id,
                        principalTable: "Patients",
                        principalColumn: "Patient_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Menu_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Menu_Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Menu_Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Menu_End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Dietician_Id = table.Column<int>(type: "int", nullable: false),
                    Patient_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Menu_Id);
                    table.ForeignKey(
                        name: "FK_Menus_Dieticians_Dietician_Id",
                        column: x => x.Dietician_Id,
                        principalTable: "Dieticians",
                        principalColumn: "Dietician_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Menus_Patients_Patient_Id",
                        column: x => x.Patient_Id,
                        principalTable: "Patients",
                        principalColumn: "Patient_Id",
                        onDelete: ReferentialAction.NoAction); // Changed from Cascade to NoAction
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllergyFood_FoodsFood_Id",
                table: "AllergyFood",
                column: "FoodsFood_Id");

            migrationBuilder.CreateIndex(
                name: "IX_AllergyPatient_PatientsPatient_Id",
                table: "AllergyPatient",
                column: "PatientsPatient_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Dieticians_Dietician_Email",
                table: "Dieticians",
                column: "Dietician_Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dieticians_Dietician_Name",
                table: "Dieticians",
                column: "Dietician_Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DiseasePatient_PatientsPatient_Id",
                table: "DiseasePatient",
                column: "PatientsPatient_Id");

            migrationBuilder.CreateIndex(
                name: "IX_FoodIngredient_IngredientsIngredient_Id",
                table: "FoodIngredient",
                column: "IngredientsIngredient_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Foods_Food_Name",
                table: "Foods",
                column: "Food_Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_Ingredient_Name",
                table: "Ingredients",
                column: "Ingredient_Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MedicinePatient_PatientsPatient_Id",
                table: "MedicinePatient",
                column: "PatientsPatient_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_Dietician_Id",
                table: "Menus",
                column: "Dietician_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_Patient_Id",
                table: "Menus",
                column: "Patient_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Dietician_Id",
                table: "Patients",
                column: "Dietician_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Patient_Email",
                table: "Patients",
                column: "Patient_Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Patient_Name",
                table: "Patients",
                column: "Patient_Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllergyFood");

            migrationBuilder.DropTable(
                name: "AllergyPatient");

            migrationBuilder.DropTable(
                name: "DiseasePatient");

            migrationBuilder.DropTable(
                name: "FoodIngredient");

            migrationBuilder.DropTable(
                name: "MedicinePatient");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "Allergy");

            migrationBuilder.DropTable(
                name: "Diseases");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Dieticians");
        }
    }
}
