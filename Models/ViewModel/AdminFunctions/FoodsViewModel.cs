using DieticianApp.Helpers;
using DieticianApp.Models.Entities;
using DieticianApp.Models.JoinTables;
using System.ComponentModel.DataAnnotations;

namespace DieticianApp.Models.ViewModel.AdminFunctions
{
    public class FoodsViewModel : Paginate
    {
        [Required(ErrorMessage = "Food name is required")]
        public string Food_Name{ get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Calorie must be a positive number.")]
        public double Calorie { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Protein must be a positive number.")]
        public double Protein { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Fat must be a positive number.")]
        public double Fat { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Carbohydrate must be a positive number.")]
        public double Carbohydrate { get; set; }

        public List<int> SelectedAllergies { get; set; } = new List<int>();
        public List<int> SelectedIngredients { get; set; } = new List<int>();
        public List<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public List<Foods> Foods { get; set; } = new List<Foods>();
        public List<Allergies> Allergies { get; set; } = new List<Allergies>();
        public List<Food_Allergies> GetFood_Allergies { get; set; } = new List<Food_Allergies>();
        public List<Food_Ingredients> GetFood_Ingredients { get; set; } = new List<Food_Ingredients>();

        public FoodsViewModel()
        {
        }

        public FoodsViewModel(List<Foods> foods)
        {
            Foods = foods;
        }
    }
}
