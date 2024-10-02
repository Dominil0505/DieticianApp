using DieticianApp.Models.JoinTables;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DieticianApp.Models.Entities
{
    [Index(nameof(Food_Name), IsUnique = true)]
    public class Foods
    {
        [Key]
        public int Food_Id { get; set; }

        [Required(ErrorMessage = "Food name is required")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Food minimum length is 3 character")]
        public string? Food_Name { get; set; }


        [Required(ErrorMessage = "Calorie is required")]
        public double? Calorie { get; set; }

        [Required(ErrorMessage = "Proteint is required")]
        public double? Protein { get; set; }

        [Required(ErrorMessage = "Fat is required")]
        public double? Fat { get; set; }

        [Required(ErrorMessage = "Carbohydrate is required")]
        public double? Carbohydrate { get; set; }

        // Relations
        public virtual ICollection<Food_Ingredients> FoodIngredients { get; set; } = new List<Food_Ingredients>();
        public virtual ICollection<Food_Allergies> FoodAllergies { get; set; } = new List<Food_Allergies>();
        public virtual ICollection<Menu_Foods> MenuFoods { get; set; } = new List<Menu_Foods>();

        public Foods(string? food_Name, double? calorie, double? protein, double? fat, double? carbohydrate)
        {
            Food_Name = food_Name;
            Calorie = calorie;
            Protein = protein;
            Fat = fat;
            Carbohydrate = carbohydrate;
        }
    }
}
