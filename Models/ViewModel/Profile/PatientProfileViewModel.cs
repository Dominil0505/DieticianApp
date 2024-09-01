using DieticianApp.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DieticianApp.Models.ViewModel.Profile
{
    public class PatientProfileViewModel
    {
        public string? Username { get; set; }
        public DateTime? DoB {  get; set; }
        public string? Description{ get; set; }
        public byte? Height { get; set; }
        public short? Weight{ get; set; }
        [Required(ErrorMessage = "Please choose a gender")]
        public string? Gender{ get; set; }

        // Allergies
        public List<SelectListItem>? AvaliableAllergies{ get; set; }
        public List<int>? SelectedAllergyIds{ get; set; }

        // Medications
        public List<string>? Medication {  get; set; }

        // Disease
        public List<string>? Disease { get; set; }
    }
}
