using System.ComponentModel.DataAnnotations;

namespace DieticianApp.Entities
{
    public class Disease
    {
        [Key]
        public int Disease_Id{ get; set; }

        public string? Disease_Name{ get; set; }
        public string? Diagnosis_Date { get; set; }
    }
}
