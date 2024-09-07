using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DieticianApp.Models.ViewModel.AdminFunctions
{
    public class AddPatientViewModel
    {
        [Required(ErrorMessage = "Date of Birth is require")]
        public DateTime? DoB { get; set; }
        public string? Description { get; set; }

        [Required(ErrorMessage = "Height is required")]
        public byte? Height { get; set; }

        [Required(ErrorMessage = "Weight is required")]
        public short? Weight { get; set; }

        [Required(ErrorMessage = "Please choose a gender")]
        public string? Gender { get; set; }

        public int? Dietician_Id {  get; set; }

        public List<Patients> Patients { get; set; } = new List<Patients>();

        // Allergies
        public List<SelectListItem>? AvaliableAllergies { get; set; }
        public List<int>? SelectedAllergyIds { get; set; }

        // Medications
        public List<string>? Medication { get; set; }

        // Disease
        public List<string>? Disease { get; set; }


    }
}
