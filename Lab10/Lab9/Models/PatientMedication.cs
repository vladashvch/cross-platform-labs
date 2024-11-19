namespace Lab9.Models
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
    }

    public class Medication
    {
        public int MedicationId { get; set; }
        public string? MedicationName { get; set; }
        public string? MedicationDescription { get; set; }
    }

    public class PatientMedication
    {
        public int PatientMedicationId { get; set; }
        public DateTime DateTimeAdministered { get; set; }
        public double Dosage { get; set; }
        public string? Comments { get; set; }
        public string? OtherDetails { get; set; }
        public Patient? Patient { get; set; }
        public Medication? Medication { get; set; }
    }
}
