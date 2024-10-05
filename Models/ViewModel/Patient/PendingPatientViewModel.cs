using DieticianApp.Models.Entities;

namespace DieticianApp.Models.ViewModel.Patient
{
    public class PendingPatientViewModel
    {
        public List<Users> Users {  get; set; } = new List<Users>();

        public PendingPatientViewModel(List<Users> users)
        {
            Users = users ?? new List<Users>();
        }
    }
}
