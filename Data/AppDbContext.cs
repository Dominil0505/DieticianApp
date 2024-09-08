using Microsoft.EntityFrameworkCore;
using DieticianApp.Models.Entities;
using DieticianApp.Models.JoinTables;
using DieticianApp.Models.ViewModel;

namespace DieticianApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Dieticians> Dieticians { get; set; }
        public DbSet<Patients> Patients { get; set; }
        public DbSet<Menus> Menus { get; set; }
        public DbSet<Foods> Foods { get; set; }
        public DbSet<Allergies> Allergy { get; set; }
        public DbSet<Diseases> Diseases { get; set; }
        public DbSet<Medicines> Medicines { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Admins> Admins{ get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }

        // Join Tables
        public DbSet<User_Roles> User_Roles { get; set; }
        public DbSet<Food_Allergies> Food_Allergies { get; set; }
        public DbSet<Food_Ingredients> Food_Ingredients { get; set; }
        public DbSet<Menu_Foods> Menu_Foods { get; set; }
        public DbSet<Patient_Allergies> Patient_Allergies { get; set; }
        public DbSet<Patient_Disease> Patient_Diseases { get; set; }
        public DbSet<Patient_Medication> patient_Medications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User_Roles>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<User_Roles>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<User_Roles>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(ur => ur.RoleId);

            modelBuilder.Entity<Food_Ingredients>()
                .HasKey(fi => new { fi.FoodId, fi.IngredientId });

            modelBuilder.Entity<Food_Ingredients>()
                .HasOne(fi => fi.Food)
                .WithMany(f => f.FoodIngredients)
                .HasForeignKey(fi => fi.FoodId);

            modelBuilder.Entity<Food_Ingredients>()
                .HasOne(fi => fi.Ingredient)
                .WithMany(i => i.FoodIngredients)
                .HasForeignKey(fi => fi.IngredientId);

            modelBuilder.Entity<Food_Allergies>()
                .HasKey(fa => new { fa.FoodId, fa.AllergyId });

            modelBuilder.Entity<Food_Allergies>()
                .HasOne(fa => fa.Food)
                .WithMany(f => f.FoodAllergies)
                .HasForeignKey(fa => fa.FoodId);

            modelBuilder.Entity<Food_Allergies>()
                .HasOne(fa => fa.Allergy)
                .WithMany(a => a.FoodAllergies)
                .HasForeignKey(fa => fa.AllergyId);

            modelBuilder.Entity<Patient_Allergies>()
                .HasKey(pa => new { pa.PatientId, pa.AllergyId });

            modelBuilder.Entity<Patient_Allergies>()
                .HasOne(pa => pa.Patient)
                .WithMany(p => p.PatientAllergies)
                .HasForeignKey(pa => pa.PatientId);

            modelBuilder.Entity<Patient_Allergies>()
                .HasOne(pa => pa.Allergy)
                .WithMany(a => a.PatientAllergies)
                .HasForeignKey(pa => pa.AllergyId);

            modelBuilder.Entity<Patient_Medication>()
                .HasKey(pm => new { pm.PatientId, pm.MedicationId });

            modelBuilder.Entity<Patient_Medication>()
                .HasOne(pm => pm.Patient)
                .WithMany(p => p.PatientMedications)
                .HasForeignKey(pm => pm.PatientId);

            modelBuilder.Entity<Patient_Medication>()
                .HasOne(pm => pm.Medication)
                .WithMany(m => m.PatientMedications)
                .HasForeignKey(pm => pm.MedicationId);

            modelBuilder.Entity<Patient_Disease>()
                .HasKey(pd => new { pd.PatientId, pd.DiseaseId });

            modelBuilder.Entity<Patient_Disease>()
                .HasOne(pd => pd.Patient)
                .WithMany(p => p.PatientDiseases)
                .HasForeignKey(pd => pd.PatientId);

            modelBuilder.Entity<Patient_Disease>()
                .HasOne(pd => pd.Disease)
                .WithMany(d => d.PatientDiseases)
                .HasForeignKey(pd => pd.DiseaseId);

            modelBuilder.Entity<Menu_Foods>()
                .HasKey(mf => new { mf.MenuId, mf.FoodId });

            modelBuilder.Entity<Menu_Foods>()
                .HasOne(mf => mf.Menu)
                .WithMany(m => m.MenuFoods)
                .HasForeignKey(mf => mf.MenuId);

            modelBuilder.Entity<Menu_Foods>()
                .HasOne(mf => mf.Food)
                .WithMany(f => f.MenuFoods)
                .HasForeignKey(mf => mf.FoodId);
        }
        public DbSet<DieticianApp.Models.ViewModel.LoginViewModel> LoginViewModel { get; set; } = default!;
    }
}
