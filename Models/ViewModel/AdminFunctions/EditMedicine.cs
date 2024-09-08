using System.ComponentModel.DataAnnotations;

namespace DieticianApp.Models.ViewModel.AdminFunctions
{
    public class EditMedicine
    {
        [Required(ErrorMessage = "Medicine Name is required")]
        public string Medicine_Name {  get; set; }
    }
}
