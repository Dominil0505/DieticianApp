using DieticianApp.Models.Entities;

namespace DieticianApp.Models.JoinTables
{
    public class Patient_Disease
    {
        public int PatientId { get; set; }
        public int DiseaseId { get; set; }

        // Relations
        public virtual Patients Patient { get; set; }
        public virtual Diseases Disease { get; set; }

        public Patient_Disease(int patientId, int diseaseId)
        {
            PatientId = patientId;
            DiseaseId = diseaseId;
        }
    }
}
