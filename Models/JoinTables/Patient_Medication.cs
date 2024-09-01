using DieticianApp.Models.Entities;

namespace DieticianApp.Models.JoinTables
{
    public class Patient_Medication
    {
        public int PatientId { get; set; }
        public int MedicationId { get; set; }

        // Relations
        public virtual Patients Patient { get; set; }
        public virtual Medications Medication { get; set; }
    }
}
