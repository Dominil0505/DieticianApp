using DieticianApp.Helpers;
using DieticianApp.Models.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DieticianApp.Models.ViewModel.AdminFunctions
{
    public class PatientViewModel : Paginate
    {
        public List<Patients> Patients { get; set; } = new List<Patients>();
        public List<Dieticians> Dieticians { get; set; } = new List<Dieticians>();
        public int Selected_Dietitan { get; set; }

        public PatientViewModel()
        {
        }

        public PatientViewModel(List<Patients> patients)
        {
            Patients = patients;
        }
    }
}
