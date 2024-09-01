using System.ComponentModel.DataAnnotations;

namespace DieticianApp.Models.ViewModel.AdminFunctions
{
    public class AddIngredientViewModel
    {
        [Required(ErrorMessage = "Please add ingredient name!")]
        public string IngredientName { get; set; }

        [Required(ErrorMessage = "Please add calorie")]
        public int Calorie { get; set; }

        [Required(ErrorMessage = "Please add protein")]
        public int Proteint { get; set; }
        
        [Required(ErrorMessage = "Please add fat")]
        public int Fat { get; set; }

        [Required(ErrorMessage = "Please add Carbohydrate")]
        public int Carbohydrate { get; set; }

    }
}
