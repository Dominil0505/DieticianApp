using DieticianApp.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace DieticianApp.Models.ViewModel.AdminFunctions
{
    public class AddIngredientsViewModel
    {
        [Required(ErrorMessage = "Ingredient name is required")]
        public string Ingredient_Name { get; set; }

        [Required(ErrorMessage = "Calorie is required")]
        public int Calorie{ get; set; }

        [Required(ErrorMessage = "Protein is required")]
        public int Protein{ get; set; }

        [Required(ErrorMessage = "Fat is required")]
        public int Fat{ get; set; }

        [Required(ErrorMessage = "Carbohydrate name is required")]
        public int Carbohydrate { get; set; }

        public List<Ingredient> Ingredient { get; set; } = new List<Ingredient>();

        public AddIngredientsViewModel()
        {
        }

        public AddIngredientsViewModel(List<Ingredient> ingredient)
        {
            Ingredient = ingredient;
        }

        public AddIngredientsViewModel(string ingredient_Name, int calorie, int protein, int fat, int carbohydrate)
        {
            Ingredient_Name = ingredient_Name;
            Calorie = calorie;
            Protein = protein;
            Fat = fat;
            Carbohydrate = carbohydrate;
        }
    }
}
