using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DieticianApp.Entities
{
    [Index(nameof(Patient_Name), IsUnique = true)]
    [Index(nameof(Patient_Email), IsUnique = true)]

    public class Patient
    {
        [Key]
        public int Patient_Id{ get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string? Patient_Name { get; set;}

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Email format is not correct")]
        public string? Patient_Email{ get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password{ get; set; }

        [MaxLength(110, ErrorMessage = "Max 110 age allowed")]
        public byte? Age{ get; set; }
        public string? Description{ get; set; }
        public string? Mobile{ get; set; }
        public byte? Height{ get; set; }
        public short? Weight{ get; set; }
        public string? Gender{ get; set; }
        public string? Role { get; set; } = "Patient";
        public DateTime Created_At{ get; set; } = DateTime.Now;
        public DateTime? Updated_At { get; set; }

        // Relation
        [ForeignKey("Dietician_Id")]
        public Dietician Dietician { get; set; }

        public Menu Menu { get; set; }

        public ICollection<Disease> Diseases { get; set; }
        public ICollection<Medicine> Medicines { get; set; }
        public ICollection<Allergy> Allergy { get; set; }
    }
}
