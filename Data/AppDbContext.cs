using DieticianApp.Entities;
using Microsoft.EntityFrameworkCore;
using DieticianApp.Models;

namespace DieticianApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Dietician> Dieticians { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Food> Foods { get; set; }
        public DbSet<Allergy> Allergy { get; set; }
        public DbSet<Disease> Diseases { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<DieticianApp.Models.RegisterViewModel> RegisterViewModel { get; set; } = default!;
        public DbSet<DieticianApp.Models.LoginViewModel> LoginViewModel { get; set; } = default!;
    }
}
