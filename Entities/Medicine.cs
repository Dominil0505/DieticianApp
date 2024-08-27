using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DieticianApp.Entities
{
    [Index(nameof(Medicine_Name), IsUnique = true)]

    public class Medicine
    {
        [Key]
        public int Medicine_Id{ get; set; }
        public string? Medicine_Name{ get; set; }

        // Relation
        public ICollection<Patient>? Patients { get; set; }
    }
}
