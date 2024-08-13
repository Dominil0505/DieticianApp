using System.ComponentModel.DataAnnotations;

namespace DieticianApp.Entities
{
    public class Medicine
    {
        [Key]
        public int Medicine_Id{ get; set; }
        public string? Medicine_Name{ get; set; }
    }
}
