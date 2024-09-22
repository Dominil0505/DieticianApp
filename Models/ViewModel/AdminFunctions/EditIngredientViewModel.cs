using System.ComponentModel.DataAnnotations;

namespace DieticianApp.Models.ViewModel.AdminFunctions
{
    public class EditIngredientViewModel
    {

        public string Ingredient_Name { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Calorie must be a positive number.")]
        public int Calorie { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Protein must be a positive number.")]
        public int Protein { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Fat must be a positive number.")]
        public int Fat { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Carbohydrate must be a positive number.")]
        public int Carbohydrate { get; set; }
    }
}
