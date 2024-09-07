using System.ComponentModel.DataAnnotations;

namespace DieticianApp.Models.ViewModel.AdminFunctions
{
    public class EditAllergy
    {
        [Required(ErrorMessage = "Allergy name is required")]
        public string AllergyName { get; set; }
    }
}
