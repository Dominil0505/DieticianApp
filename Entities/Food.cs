using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DieticianApp.Entities
{
    [Index(nameof(Food_Name), IsUnique = true)]
    public class Food
    {
        [Key]
        public int Food_Id{ get; set; }

        [Required(ErrorMessage = "Food name is required")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Food minimum length is 3 character")]
        public string? Food_Name { get; set; }
        public int? Calorie { get; set; }
        public int? Protein{ get; set; }
        public int? Fat{ get; set; }
        public int? Carbohydrate { get; set; }
       
        // Relations
        public ICollection<Allergy> Allergies { get; set; }
        public ICollection<Ingredient> Ingredients { get; set;}
    }
}
