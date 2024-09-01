using DieticianApp.Models.Entities;

namespace DieticianApp.Models.JoinTables
{
    public class Food_Ingredients
    {
        public int FoodId { get; set; }
        public int IngredientId { get; set; }

        // Relations
        public virtual Foods? Food { get; set; }
        public virtual Ingredient? Ingredient { get; set; }
    }
}
