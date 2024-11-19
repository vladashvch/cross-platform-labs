using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6.Models
{
    public class Patient
    {
        [Key]
        public required int PatientId { get; set; }

        [MaxLength(50)]
        public required string FirstName { get; set; }

        [MaxLength(50)]
        public string? MiddleName { get; set; }

        [MaxLength(50)]
        public required string LastName { get; set; }

        public DateTime DateOfBirth { get; set; }

        [MaxLength(1)]
        public required string Gender { get; set; }

        public required string Address { get; set; }

        public string? OtherDetails { get; set; }

        public ICollection<Appointment>? Appointments { get; set; }
        public ICollection<PatientMedication>? PatientMedications { get; set; }
    }
}
