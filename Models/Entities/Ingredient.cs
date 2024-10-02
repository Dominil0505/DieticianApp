using DieticianApp.Models.JoinTables;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DieticianApp.Models.Entities
{
    [Index(nameof(Ingredient_Name), IsUnique = true)]
    public class Ingredient
    {
        [Key]
        public int Ingredient_Id { get; set; }

        [Required(ErrorMessage = "Ingredient name is required")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Ingredient name minimum length is 3")]
        public string? Ingredient_Name { get; set; }

        [Required(ErrorMessage = "Calorie is required")]
        public double? Calorie { get; set; }

        [Required(ErrorMessage = "Proteint is required")]
        public double? Protein { get; set; }

        [Required(ErrorMessage = "Fat is required")]
        public double? Fat { get; set; }

        [Required(ErrorMessage = "Carbohydrate is required")]
        public double? Carbohydrate { get; set; }

        // Relation
        public virtual ICollection<Food_Ingredients> FoodIngredients { get; set; } = new List<Food_Ingredients>();

        public Ingredient()
        {
        }

        public Ingredient(string? ingredient_Name, double? calorie, double? protein, double? fat, double? carbohydrate)
        {
            Ingredient_Name = ingredient_Name;
            Calorie = calorie;
            Protein = protein;
            Fat = fat;
            Carbohydrate = carbohydrate;
        }
    }
}

