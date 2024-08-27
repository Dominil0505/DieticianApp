﻿// <auto-generated />
using System;
using DieticianApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DieticianApp.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20240827213922_UpdateIsNullConstraints")]
    partial class UpdateIsNullConstraints
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AllergyFood", b =>
                {
                    b.Property<int>("AllergiesAllergy_Id")
                        .HasColumnType("int");

                    b.Property<int>("FoodsFood_Id")
                        .HasColumnType("int");

                    b.HasKey("AllergiesAllergy_Id", "FoodsFood_Id");

                    b.HasIndex("FoodsFood_Id");

                    b.ToTable("AllergyFood");
                });

            modelBuilder.Entity("AllergyPatient", b =>
                {
                    b.Property<int>("Allergy_Id")
                        .HasColumnType("int");

                    b.Property<int>("PatientsPatient_Id")
                        .HasColumnType("int");

                    b.HasKey("Allergy_Id", "PatientsPatient_Id");

                    b.HasIndex("PatientsPatient_Id");

                    b.ToTable("AllergyPatient");
                });

            modelBuilder.Entity("DieticianApp.Entities.Allergy", b =>
                {
                    b.Property<int>("Allergy_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Allergy_Id"));

                    b.Property<string>("Allergy_Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Allergy_Id");

                    b.HasIndex("Allergy_Name")
                        .IsUnique()
                        .HasFilter("[Allergy_Name] IS NOT NULL");

                    b.ToTable("Allergy");
                });

            modelBuilder.Entity("DieticianApp.Entities.Dietician", b =>
                {
                    b.Property<int>("Dietician_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Dietician_Id"));

                    b.Property<string>("AvatarUrL")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created_At")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Dietician_Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Dietician_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Mobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Updated_At")
                        .HasColumnType("datetime2");

                    b.HasKey("Dietician_Id");

                    b.HasIndex("Dietician_Email")
                        .IsUnique();

                    b.HasIndex("Dietician_Name");

                    b.ToTable("Dieticians");
                });

            modelBuilder.Entity("DieticianApp.Entities.Disease", b =>
                {
                    b.Property<int>("Disease_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Disease_Id"));

                    b.Property<string>("Diagnosis_Date")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Disease_Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Disease_Id");

                    b.HasIndex("Disease_Name")
                        .IsUnique()
                        .HasFilter("[Disease_Name] IS NOT NULL");

                    b.ToTable("Diseases");
                });

            modelBuilder.Entity("DieticianApp.Entities.Food", b =>
                {
                    b.Property<int>("Food_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Food_Id"));

                    b.Property<int?>("Calorie")
                        .HasColumnType("int");

                    b.Property<int?>("Carbohydrate")
                        .HasColumnType("int");

                    b.Property<int?>("Fat")
                        .HasColumnType("int");

                    b.Property<string>("Food_Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("Protein")
                        .HasColumnType("int");

                    b.HasKey("Food_Id");

                    b.HasIndex("Food_Name")
                        .IsUnique();

                    b.ToTable("Foods");
                });

            modelBuilder.Entity("DieticianApp.Entities.Ingredient", b =>
                {
                    b.Property<int>("Ingredient_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Ingredient_Id"));

                    b.Property<int?>("Calorie")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("Carbohydrate")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<int?>("Fat")
                        .IsRequired()
                        .HasColumnType("int");

                    b.Property<string>("Ingredient_Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("Protein")
                        .IsRequired()
                        .HasColumnType("int");

                    b.HasKey("Ingredient_Id");

                    b.HasIndex("Ingredient_Name")
                        .IsUnique();

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("DieticianApp.Entities.Medicine", b =>
                {
                    b.Property<int>("Medicine_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Medicine_Id"));

                    b.Property<string>("Medicine_Name")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Medicine_Id");

                    b.HasIndex("Medicine_Name")
                        .IsUnique()
                        .HasFilter("[Medicine_Name] IS NOT NULL");

                    b.ToTable("Medicines");
                });

            modelBuilder.Entity("DieticianApp.Entities.Menu", b =>
                {
                    b.Property<int>("Menu_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Menu_Id"));

                    b.Property<string>("Comment")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created_At")
                        .HasColumnType("datetime2");

                    b.Property<string>("Created_by")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Dietician_Id")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Menu_End")
                        .HasColumnType("datetime2");

                    b.Property<string>("Menu_Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<DateTime?>("Menu_Start")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Patient_Id")
                        .HasColumnType("int");

                    b.Property<DateTime?>("Updated_At")
                        .HasColumnType("datetime2");

                    b.HasKey("Menu_Id");

                    b.HasIndex("Dietician_Id");

                    b.HasIndex("Menu_Name")
                        .IsUnique();

                    b.HasIndex("Patient_Id")
                        .IsUnique()
                        .HasFilter("[Patient_Id] IS NOT NULL");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("DieticianApp.Entities.Patient", b =>
                {
                    b.Property<int>("Patient_Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Patient_Id"));

                    b.Property<byte?>("Age")
                        .HasMaxLength(110)
                        .HasColumnType("tinyint");

                    b.Property<DateTime>("Created_At")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Dietician_Id")
                        .HasColumnType("int");

                    b.Property<string>("Gender")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte?>("Height")
                        .HasColumnType("tinyint");

                    b.Property<string>("Mobile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Patient_Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Patient_Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Updated_At")
                        .HasColumnType("datetime2");

                    b.Property<short?>("Weight")
                        .HasColumnType("smallint");

                    b.HasKey("Patient_Id");

                    b.HasIndex("Dietician_Id");

                    b.HasIndex("Patient_Email")
                        .IsUnique();

                    b.HasIndex("Patient_Name");

                    b.ToTable("Patients");
                });

            modelBuilder.Entity("DieticianApp.Models.LoginViewModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("LoginViewModel");
                });

            modelBuilder.Entity("DieticianApp.Models.RegisterViewModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ConfirmPassword")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("RegisterViewModel");
                });

            modelBuilder.Entity("DiseasePatient", b =>
                {
                    b.Property<int>("DiseasesDisease_Id")
                        .HasColumnType("int");

                    b.Property<int>("PatientsPatient_Id")
                        .HasColumnType("int");

                    b.HasKey("DiseasesDisease_Id", "PatientsPatient_Id");

                    b.HasIndex("PatientsPatient_Id");

                    b.ToTable("DiseasePatient");
                });

            modelBuilder.Entity("FoodIngredient", b =>
                {
                    b.Property<int>("FoodsFood_Id")
                        .HasColumnType("int");

                    b.Property<int>("IngredientsIngredient_Id")
                        .HasColumnType("int");

                    b.HasKey("FoodsFood_Id", "IngredientsIngredient_Id");

                    b.HasIndex("IngredientsIngredient_Id");

                    b.ToTable("FoodIngredient");
                });

            modelBuilder.Entity("MedicinePatient", b =>
                {
                    b.Property<int>("MedicinesMedicine_Id")
                        .HasColumnType("int");

                    b.Property<int>("PatientsPatient_Id")
                        .HasColumnType("int");

                    b.HasKey("MedicinesMedicine_Id", "PatientsPatient_Id");

                    b.HasIndex("PatientsPatient_Id");

                    b.ToTable("MedicinePatient");
                });

            modelBuilder.Entity("AllergyFood", b =>
                {
                    b.HasOne("DieticianApp.Entities.Allergy", null)
                        .WithMany()
                        .HasForeignKey("AllergiesAllergy_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DieticianApp.Entities.Food", null)
                        .WithMany()
                        .HasForeignKey("FoodsFood_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AllergyPatient", b =>
                {
                    b.HasOne("DieticianApp.Entities.Allergy", null)
                        .WithMany()
                        .HasForeignKey("Allergy_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DieticianApp.Entities.Patient", null)
                        .WithMany()
                        .HasForeignKey("PatientsPatient_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DieticianApp.Entities.Menu", b =>
                {
                    b.HasOne("DieticianApp.Entities.Dietician", "Dietician")
                        .WithMany("Menus")
                        .HasForeignKey("Dietician_Id");

                    b.HasOne("DieticianApp.Entities.Patient", "Patient")
                        .WithOne("Menu")
                        .HasForeignKey("DieticianApp.Entities.Menu", "Patient_Id");

                    b.Navigation("Dietician");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("DieticianApp.Entities.Patient", b =>
                {
                    b.HasOne("DieticianApp.Entities.Dietician", "Dietician")
                        .WithMany("Patients")
                        .HasForeignKey("Dietician_Id");

                    b.Navigation("Dietician");
                });

            modelBuilder.Entity("DiseasePatient", b =>
                {
                    b.HasOne("DieticianApp.Entities.Disease", null)
                        .WithMany()
                        .HasForeignKey("DiseasesDisease_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DieticianApp.Entities.Patient", null)
                        .WithMany()
                        .HasForeignKey("PatientsPatient_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("FoodIngredient", b =>
                {
                    b.HasOne("DieticianApp.Entities.Food", null)
                        .WithMany()
                        .HasForeignKey("FoodsFood_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DieticianApp.Entities.Ingredient", null)
                        .WithMany()
                        .HasForeignKey("IngredientsIngredient_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MedicinePatient", b =>
                {
                    b.HasOne("DieticianApp.Entities.Medicine", null)
                        .WithMany()
                        .HasForeignKey("MedicinesMedicine_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DieticianApp.Entities.Patient", null)
                        .WithMany()
                        .HasForeignKey("PatientsPatient_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("DieticianApp.Entities.Dietician", b =>
                {
                    b.Navigation("Menus");

                    b.Navigation("Patients");
                });

            modelBuilder.Entity("DieticianApp.Entities.Patient", b =>
                {
                    b.Navigation("Menu");
                });
#pragma warning restore 612, 618
        }
    }
}
