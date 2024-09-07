using DieticianApp.Models.Entities;
using DieticianApp.Models.JoinTables;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Patients
{
    [Key]
    public int Patient_Id { get; set; }

    [MaxLength(110, ErrorMessage = "Max 110 age allowed")]
    public DateTime? DoB { get; set; }
    public string? Description { get; set; }
    public byte? Height { get; set; }
    public short? Weight { get; set; }
    public string? Gender { get; set; }
    public string? DieticianName { get; set; }
    public int? Created_By_Admin_Id { get; set; }


    // Relation
    [ForeignKey("User")]
    public int? User_Id { get; set; }
    public virtual Users? User { get; set; }

    [ForeignKey("Dietician")]
    public int? Dietician_Id { get; set; }
    public virtual Dieticians? Dietician { get; set; }

    public virtual Admins? CreatedByAdmin { get; set; }
    public virtual ICollection<Patient_Allergies>? PatientAllergies { get; set; } = new List<Patient_Allergies>();
    public virtual ICollection<Patient_Medication> PatientMedications { get; set;} = new List<Patient_Medication>();
    public virtual ICollection<Patient_Disease> PatientDiseases { get; set; } = new List<Patient_Disease>();

    public Patients(int? user_Id)
    {
        User_Id = user_Id;
    }

    public Patients(DateTime? doB, byte? height, short? weight, string? gender)
    {
        DoB = doB;
        Height = height;
        Weight = weight;
        Gender = gender;
    }
}
