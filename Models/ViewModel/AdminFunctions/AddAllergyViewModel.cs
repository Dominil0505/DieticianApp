using DieticianApp.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace DieticianApp.Models.ViewModel.AdminFunctions
{
    public class AddAllergyViewModel
    {
        [Required(ErrorMessage = "Please add allergy name")]
        public string AllergyName{ get; set; }
        public List<Allergies> Allergies { get; set; } = new List<Allergies>();

        public AddAllergyViewModel()
        {
        }

        public AddAllergyViewModel(string allergyName)
        {
            AllergyName = allergyName;
        }

        public AddAllergyViewModel(List<Allergies> allergies)
        {
            Allergies = allergies ?? new List<Allergies>();
        }
    }
}
