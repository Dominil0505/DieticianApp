using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DieticianApp.Entities
{
    [Index(nameof(Disease_Name), IsUnique = true)]
    public class Disease
    {
        [Key]
        public int Disease_Id{ get; set; }

        public string? Disease_Name{ get; set; }
        public string? Diagnosis_Date { get; set; }

        // Relation
        public ICollection<Patient>? Patients { get; set; }
    }
}
