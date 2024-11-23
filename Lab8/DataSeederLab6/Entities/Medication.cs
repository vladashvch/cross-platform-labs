using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Medication
    {
        [Key]

        public required int MedicationId { get; set; }

        public required string MedicationTypeCodeId { get; set; }

        public required decimal MedicationUnitCost { get; set; }

        [MaxLength(100)]
        public required string MedicationName { get; set; }

        public string? MedicationDescription { get; set; }

        public string? OtherDetails { get; set; }

        public ICollection<PatientMedication>? PatientMedications { get; set; }
    }
}
