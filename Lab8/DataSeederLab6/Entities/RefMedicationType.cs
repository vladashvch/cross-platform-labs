using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6.Models
{
    public class RefMedicationType
    {
        [Key]
        public required string MedicationTypeCode { get; set; }

        [MaxLength(100)]
        public required string MedicationTypeName { get; set; }

        public string? MedicationTypeDescription { get; set; }

        [MaxLength(100)]
        public string? Example { get; set; }

        public ICollection<Medication>? Medications { get; set; }
    }
}
