using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6.Models
{
    public class Appointment
    {
        [Key]
        public required int AppointmentId { get; set; }

        public required int PatientId { get; set; }

        public required int StaffId { get; set; }

        public DateTime DateTimeOfAppointment { get; set; }

        public string? OtherDetails { get; set; }

        public Patient? Patient { get; set; }
        public Staff? Staff { get; set; }
    }
}
