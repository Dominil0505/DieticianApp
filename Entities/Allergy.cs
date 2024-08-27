using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DieticianApp.Entities
{
    [Index(nameof(Allergy_Name), IsUnique = true)]
    public class Allergy
    {
        [Key]
        public int Allergy_Id{ get; set; }
        public string? Allergy_Name{ get; set; }

        // Relations
        public ICollection<Patient>? Patients { get; set; }
        public ICollection<Food>? Foods { get; set; }
    }
}
