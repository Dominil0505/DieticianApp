﻿using DieticianApp.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace DieticianApp.Models.ViewModel.Profile
{
    public class CompleteProfileViewModel
    {
        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime? DoB { get; set; }
        [Required(ErrorMessage = "Height is required")]
        
        [Range(50,250, ErrorMessage = "Height must be between 50 and 250 cm")]
        public byte? Height { get; set; }

        [Range(20, 350, ErrorMessage = "Weight mus be between 20 and 350 Kg" )]
        public short? Weight { get; set; }
        
        [Required(ErrorMessage = "Please choose a gender")]
        public string? Gender { get; set; }
        public Users CompletedUser { get; set; }

        public List<int> SelectedAllergies { get; set; } = new List<int>();
        public List<int> SelectedDiseases { get; set; } = new List<int>();
        public List<int> SelectedMedicines { get; set; } = new List<int>();

        public List<Patients> Patients { get; set; } = new List<Patients>();
        public List<Allergies> Allergies { get; set; } = new List<Allergies>(); 
        public List<Diseases> Diseases { get; set; } = new List<Diseases>();
        public List<Medicines> Medicines { get; set; } = new List<Medicines>();

        public CompleteProfileViewModel()
        {
        }
    }
}
