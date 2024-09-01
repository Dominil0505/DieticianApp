using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DieticianApp.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Allergy",
                columns: table => new
                {
                    Allergy_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Allergy_Name = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allergy", x => x.Allergy_Id);
                });

            migrationBuilder.CreateTable(
                name: "Diseases",
                columns: table => new
                {
                    Disease_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Disease_Name = table.Column<string>(type: "nvarchar(450)", nullable: true),
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
                    Calorie = table.Column<int>(type: "int", nullable: false),
                    Protein = table.Column<int>(type: "int", nullable: false),
                    Fat = table.Column<int>(type: "int", nullable: false),
                    Carbohydrate = table.Column<int>(type: "int", nullable: false)
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
                    Medicine_Name = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicines", x => x.Medicine_Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Role_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role_Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Role_Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    User_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Mobile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Avatar_Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Deleted_At = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.User_Id);
                });

            migrationBuilder.CreateTable(
                name: "Food_Allergies",
                columns: table => new
                {
                    FoodId = table.Column<int>(type: "int", nullable: false),
                    AllergyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Food_Allergies", x => new { x.FoodId, x.AllergyId });
                    table.ForeignKey(
                        name: "FK_Food_Allergies_Allergy_AllergyId",
                        column: x => x.AllergyId,
                        principalTable: "Allergy",
                        principalColumn: "Allergy_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Food_Allergies_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Food_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Food_Ingredients",
                columns: table => new
                {
                    FoodId = table.Column<int>(type: "int", nullable: false),
                    IngredientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Food_Ingredients", x => new { x.FoodId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_Food_Ingredients_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Food_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Food_Ingredients_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "Ingredient_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Admin_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Admin_Id);
                    table.ForeignKey(
                        name: "FK_Admins_Users_User_Id",
                        column: x => x.User_Id,
                        principalTable: "Users",
                        principalColumn: "User_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Dieticians",
                columns: table => new
                {
                    Dietician_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    User_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dieticians", x => x.Dietician_Id);
                    table.ForeignKey(
                        name: "FK_Dieticians_Users_User_Id",
                        column: x => x.User_Id,
                        principalTable: "Users",
                        principalColumn: "User_Id");
                });

            migrationBuilder.CreateTable(
                name: "User_Roles",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User_Roles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_User_Roles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Role_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_User_Roles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "User_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Patient_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoB = table.Column<string>(type: "nvarchar(110)", maxLength: 110, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Height = table.Column<byte>(type: "tinyint", nullable: true),
                    Weight = table.Column<short>(type: "smallint", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DieticianName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_By_Admin_Id = table.Column<int>(type: "int", nullable: true),
                    User_Id = table.Column<int>(type: "int", nullable: true),
                    Dietician_Id = table.Column<int>(type: "int", nullable: true),
                    CreatedByAdminAdmin_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Patient_Id);
                    table.ForeignKey(
                        name: "FK_Patients_Admins_CreatedByAdminAdmin_Id",
                        column: x => x.CreatedByAdminAdmin_Id,
                        principalTable: "Admins",
                        principalColumn: "Admin_Id");
                    table.ForeignKey(
                        name: "FK_Patients_Dieticians_Dietician_Id",
                        column: x => x.Dietician_Id,
                        principalTable: "Dieticians",
                        principalColumn: "Dietician_Id");
                    table.ForeignKey(
                        name: "FK_Patients_Users_User_Id",
                        column: x => x.User_Id,
                        principalTable: "Users",
                        principalColumn: "User_Id");
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Menu_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Menu_Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Menu_Start = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Menu_End = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created_At = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Updated_At = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Patient_Id = table.Column<int>(type: "int", nullable: true),
                    Dietician_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Menu_Id);
                    table.ForeignKey(
                        name: "FK_Menus_Dieticians_Dietician_Id",
                        column: x => x.Dietician_Id,
                        principalTable: "Dieticians",
                        principalColumn: "Dietician_Id");
                    table.ForeignKey(
                        name: "FK_Menus_Patients_Patient_Id",
                        column: x => x.Patient_Id,
                        principalTable: "Patients",
                        principalColumn: "Patient_Id");
                });

            migrationBuilder.CreateTable(
                name: "Patient_Allergies",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    AllergyId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient_Allergies", x => new { x.PatientId, x.AllergyId });
                    table.ForeignKey(
                        name: "FK_Patient_Allergies_Allergy_AllergyId",
                        column: x => x.AllergyId,
                        principalTable: "Allergy",
                        principalColumn: "Allergy_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Patient_Allergies_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Patient_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patient_Disease",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    DiseaseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient_Disease", x => new { x.PatientId, x.DiseaseId });
                    table.ForeignKey(
                        name: "FK_Patient_Disease_Diseases_DiseaseId",
                        column: x => x.DiseaseId,
                        principalTable: "Diseases",
                        principalColumn: "Disease_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Patient_Disease_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Patient_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patient_Medication",
                columns: table => new
                {
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    MedicationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient_Medication", x => new { x.PatientId, x.MedicationId });
                    table.ForeignKey(
                        name: "FK_Patient_Medication_Medicines_MedicationId",
                        column: x => x.MedicationId,
                        principalTable: "Medicines",
                        principalColumn: "Medicine_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Patient_Medication_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Patient_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Menu_Foods",
                columns: table => new
                {
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    FoodId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu_Foods", x => new { x.MenuId, x.FoodId });
                    table.ForeignKey(
                        name: "FK_Menu_Foods_Foods_FoodId",
                        column: x => x.FoodId,
                        principalTable: "Foods",
                        principalColumn: "Food_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Menu_Foods_Menus_MenuId",
                        column: x => x.MenuId,
                        principalTable: "Menus",
                        principalColumn: "Menu_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_User_Id",
                table: "Admins",
                column: "User_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Allergy_Allergy_Name",
                table: "Allergy",
                column: "Allergy_Name",
                unique: true,
                filter: "[Allergy_Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Dieticians_User_Id",
                table: "Dieticians",
                column: "User_Id",
                unique: true,
                filter: "[User_Id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Diseases_Disease_Name",
                table: "Diseases",
                column: "Disease_Name",
                unique: true,
                filter: "[Disease_Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Food_Allergies_AllergyId",
                table: "Food_Allergies",
                column: "AllergyId");

            migrationBuilder.CreateIndex(
                name: "IX_Food_Ingredients_IngredientId",
                table: "Food_Ingredients",
                column: "IngredientId");

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
                name: "IX_Medicines_Medicine_Name",
                table: "Medicines",
                column: "Medicine_Name",
                unique: true,
                filter: "[Medicine_Name] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_Foods_FoodId",
                table: "Menu_Foods",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_Dietician_Id",
                table: "Menus",
                column: "Dietician_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_Patient_Id",
                table: "Menus",
                column: "Patient_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_Allergies_AllergyId",
                table: "Patient_Allergies",
                column: "AllergyId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_Disease_DiseaseId",
                table: "Patient_Disease",
                column: "DiseaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_Medication_MedicationId",
                table: "Patient_Medication",
                column: "MedicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_CreatedByAdminAdmin_Id",
                table: "Patients",
                column: "CreatedByAdminAdmin_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_Dietician_Id",
                table: "Patients",
                column: "Dietician_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_User_Id",
                table: "Patients",
                column: "User_Id",
                unique: true,
                filter: "[User_Id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_User_Roles_RoleId",
                table: "User_Roles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_User_Name",
                table: "Users",
                column: "User_Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Food_Allergies");

            migrationBuilder.DropTable(
                name: "Food_Ingredients");

            migrationBuilder.DropTable(
                name: "Menu_Foods");

            migrationBuilder.DropTable(
                name: "Patient_Allergies");

            migrationBuilder.DropTable(
                name: "Patient_Disease");

            migrationBuilder.DropTable(
                name: "Patient_Medication");

            migrationBuilder.DropTable(
                name: "User_Roles");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Foods");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "Allergy");

            migrationBuilder.DropTable(
                name: "Diseases");

            migrationBuilder.DropTable(
                name: "Medicines");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Dieticians");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
