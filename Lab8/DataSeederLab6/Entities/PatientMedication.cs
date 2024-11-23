using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class PatientMedication
    {
        [Key]
        public int PatientMedicationId { get; set; }

        public required int MedicationId { get; set; }

        public required int PatientId { get; set; }

        public DateTime DateTimeAdministered { get; set; }

        public required decimal Dosage { get; set; }

        public string? Comments { get; set; }

        public string? OtherDetails { get; set; }

        public Patient? Patient { get; set; }
        public Medication? Medication { get; set; }
    }
}
