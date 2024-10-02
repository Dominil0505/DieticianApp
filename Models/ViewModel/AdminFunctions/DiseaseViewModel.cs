using DieticianApp.Helpers;
using DieticianApp.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace DieticianApp.Models.ViewModel.AdminFunctions
{
    public class DiseaseViewModel : Paginate
    {
        [Required(ErrorMessage = "Disease name is required")]
        public string Disease_Name { get; set; }

        public List<Diseases> Diseases { get; set; } = new List<Diseases>();

        public DiseaseViewModel(string disease_Name)
        {
            Disease_Name = disease_Name;
        }

        public DiseaseViewModel(List<Diseases> diseases)
        {
            Diseases = diseases;
        }

        public DiseaseViewModel()
        {
        }
    }
}
