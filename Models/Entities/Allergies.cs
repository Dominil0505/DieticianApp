using DieticianApp.Models.JoinTables;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DieticianApp.Models.Entities
{
    [Index(nameof(Allergy_Name), IsUnique = true)]
    public class Allergies
    {
        [Key]
        public int Allergy_Id { get; set; }
        public string? Allergy_Name { get; set; }

        // Relations
        public virtual ICollection<Food_Allergies> FoodAllergies { get; set; } = new List<Food_Allergies>();
        public virtual ICollection<Patient_Allergies> PatientAllergies { get; set; } = new List<Patient_Allergies>();

        public Allergies(string? allergy_Name)
        {
            Allergy_Name = allergy_Name;
        }
    }
}
