using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using DieticianApp.Models.Entities;
using DieticianApp.Models.JoinTables;

public class Menus
{
    [Key]
    public int Menu_Id { get; set; }

    [Required(ErrorMessage = "Menu name is required")]
    [StringLength(255, MinimumLength = 3, ErrorMessage = "Minimum length is 3 character")]
    public string? Menu_Name { get; set; }

    public string? Notes { get; set; }
    public string? Created_by { get; set; }
    public DateTime? Menu_Start { get; set; }
    public DateTime? Menu_End { get; set; }
    public DateTime Created_At { get; set; } = DateTime.Now;
    public DateTime? Updated_At { get; set; }

    // Relations
    public virtual Patients? patient { get; set; }
    public virtual Dieticians? dietician { get; set; }
    public virtual ICollection<Menu_Foods> MenuFoods { get; set; } = new List<Menu_Foods>();

}
