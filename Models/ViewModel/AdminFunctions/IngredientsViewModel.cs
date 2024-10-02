using DieticianApp.Helpers;
using DieticianApp.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace DieticianApp.Models.ViewModel.AdminFunctions
{
    public class IngredientsViewModel : Paginate
    {
        [Required(ErrorMessage = "Ingredient name is required")]
        public string Ingredient_Name { get; set; }

        [Required(ErrorMessage = "Calorie is required")]
        public double Calorie { get; set; }

        [Required(ErrorMessage = "Protein is required")]
        public double Protein { get; set; }

        [Required(ErrorMessage = "Fat is required")]
        public double Fat { get; set; }

        [Required(ErrorMessage = "Carbohydrate name is required")]
        public double Carbohydrate { get; set; }

        public List<Ingredient> Ingredient { get; set; } = new List<Ingredient>();

        public IngredientsViewModel()
        {
        }

        public IngredientsViewModel(List<Ingredient> ingredient)
        {
            Ingredient = ingredient;
        }

        public IngredientsViewModel(string ingredient_Name, double calorie, double protein, double fat, double carbohydrate)
        {
            Ingredient_Name = ingredient_Name;
            Calorie = calorie;
            Protein = protein;
            Fat = fat;
            Carbohydrate = carbohydrate;
        }
    }
}
