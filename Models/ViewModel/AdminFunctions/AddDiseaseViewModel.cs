using DieticianApp.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DieticianApp.Models.ViewModel.AdminFunctions
{
    public class AddDiseaseViewModel
    {
        [Required(ErrorMessage = "Disease name is required")]
        public string Disease_Name { get; set; }

        public List<Diseases> Diseases { get; set; } = new List<Diseases>();

        public AddDiseaseViewModel(string disease_Name)
        {
            Disease_Name = disease_Name;
        }

        public AddDiseaseViewModel(List<Diseases> diseases)
        {
            Diseases = diseases;
        }

        public AddDiseaseViewModel()
        {
        }
    }
}
