using DieticianApp.Models.JoinTables;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DieticianApp.Models.Entities
{
    [Index(nameof(Disease_Name), IsUnique = true)]
    public class Diseases
    {
        [Key]
        public int Disease_Id { get; set; }
        public string? Disease_Name { get; set; }
        public string? Diagnosis_Date { get; set; }

        // Relation
        public virtual ICollection<Patient_Disease> PatientDiseases { get; set; } = new List<Patient_Disease>();
    }
}
