using DieticianApp.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace DieticianApp.Models.ViewModel.AdminFunctions
{
    public class AddMedicinesViewModel
    {
        [Required(ErrorMessage = "Medicine name is required")]
        public string? Medicine_Name { get; set; }

        public List<Medicines> Medicines { get; set; } = new List<Medicines>();

        public AddMedicinesViewModel(string? medicine_Name)
        {
            Medicine_Name = medicine_Name;
        }

        public AddMedicinesViewModel(List<Medicines> medicines)
        {
            Medicines = medicines;
        }

        public AddMedicinesViewModel()
        {
        }
    }
}
