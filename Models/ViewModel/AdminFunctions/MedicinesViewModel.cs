using DieticianApp.Helpers;
using DieticianApp.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace DieticianApp.Models.ViewModel.AdminFunctions
{
    public class MedicinesViewModel : Paginate
    {
        [Required(ErrorMessage = "Medicine name is required")]
        public string? Medicine_Name { get; set; }

        public List<Medicines> Medicines { get; set; } = new List<Medicines>();

        public MedicinesViewModel(string? medicine_Name)
        {
            Medicine_Name = medicine_Name;
        }

        public MedicinesViewModel(List<Medicines> medicines)
        {
            Medicines = medicines;
        }

        public MedicinesViewModel()
        {
        }
    }
}
