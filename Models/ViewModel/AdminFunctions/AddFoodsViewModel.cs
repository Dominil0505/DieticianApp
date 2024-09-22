using DieticianApp.Models.Entities;
using DieticianApp.Models.JoinTables;
using System.ComponentModel.DataAnnotations;

namespace DieticianApp.Models.ViewModel.AdminFunctions
{
    public class AddFoodsViewModel
    {
        [Required(ErrorMessage = "Food name is required")]
        public string Food_Name{ get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Calorie must be a positive number.")]
        public int Calorie { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Protein must be a positive number.")]
        public int Protein { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Fat must be a positive number.")]
        public int Fat { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Carbohydrate must be a positive number.")]
        public int Carbohydrate { get; set; }

        public List<int> SelectedAllergies { get; set; } = new List<int>();
        public List<int> SelectedIngredients { get; set; } = new List<int>();
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public List<Foods> Foods { get; set; } = new List<Foods>();
        public List<Allergies> Allergies { get; set; } = new List<Allergies>();
        public List<Food_Allergies> GetFood_Allergies { get; set; } = new List<Food_Allergies>();
        public List<Food_Ingredients> GetFood_Ingredients { get; set; } = new List<Food_Ingredients>();

        public AddFoodsViewModel()
        {
        }

        public AddFoodsViewModel(List<Foods> foods)
        {
            Foods = foods;
        }
    }
}
