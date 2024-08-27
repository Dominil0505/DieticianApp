using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DieticianApp.Entities
{
    [Index(nameof(Menu_Name), IsUnique = true)]

    public class Menu
    {
        [Key]
        public int Menu_Id{ get; set; }

        [Required(ErrorMessage = "Menu name is required")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "Minimum length is 3 character")]
        public string? Menu_Name{ get; set; }
        public string? Comment{ get; set; }
        public string? Created_by { get; set; }
        public DateTime? Menu_Start{ get; set; }
        public DateTime? Menu_End{ get; set; }
        public DateTime Created_At { get; set; } = DateTime.Now;
        public DateTime? Updated_At { get; set; }

        // Foreign Keys
        public int? Dietician_Id { get; set; }
        public int? Patient_Id{ get; set; }

        // Relation
        [ForeignKey("Dietician_Id")]
        public Dietician? Dietician { get; set; }

        [ForeignKey("Patient_Id")]
        public Patient? Patient { get; set; }

    }
}
