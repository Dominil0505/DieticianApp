using DieticianApp.Helpers;
using DieticianApp.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace DieticianApp.Models.ViewModel.AdminFunctions
{
    public class AllergyViewModel : Paginate
    {
        [Required(ErrorMessage = "Please add allergy name")]
        public string AllergyName{ get; set; }
        public List<Allergies> Allergies { get; set; } = new List<Allergies>();

        public AllergyViewModel()
        {
        }

        public AllergyViewModel(string allergyName)
        {
            AllergyName = allergyName;
        }

        public AllergyViewModel(List<Allergies> allergies)
        {
            Allergies = allergies ?? new List<Allergies>();
        }


    }
}
