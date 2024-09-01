using DieticianApp.Models.Entities;

namespace DieticianApp.Models.JoinTables
{
    public class Food_Allergies
    {
        public int FoodId { get; set; }
        public int AllergyId { get; set; }

        // Relations
        public Foods? Food { get; set; }
        public virtual Allergies? Allergy { get; set; }
    }
}
