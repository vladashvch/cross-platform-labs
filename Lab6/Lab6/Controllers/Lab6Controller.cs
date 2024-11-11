using Lab6.Data;
using Lab6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Lab6.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Lab6Controller : ControllerBase
    {
        private readonly DataContext _context;
        private readonly Auth0Service _auth0Service;
        public Lab6Controller(DataContext context, Auth0Service auth0Service)
        {
            _context = context;
            _auth0Service = auth0Service;
        }

        public async Task<ActionResult> VerifyTokenAndExecute(Func<Task<ActionResult>> action)
        {
            var token = HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
            {
                return Unauthorized(new { message = "Authorization token is required." });
            }

            try
            {
                var user = await _auth0Service.GetUser(token);

                if (user == false)
                {
                    return Unauthorized(new { message = "Invalid or expired token." });
                }

                return await action();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(500, new { message = "An error occurred while processing your request.", details = ex.Message });
            }
        }


        [HttpGet("medication-types")]
        public async Task<ActionResult<IEnumerable<RefMedicationType>>> GetMedicationTypes()
        {
            return await VerifyTokenAndExecute(async () =>
            {
                var medicationTypes = await _context.MedicationTypes.ToListAsync();
                return Ok(medicationTypes);
            });
        }

        [HttpGet("medication-types/{id}")]
        public async Task<ActionResult<RefMedicationType>> GetMedicationTypeById(string id)
        {
            return await VerifyTokenAndExecute(async () =>
            {
                var type = await _context.MedicationTypes
                                          .FirstOrDefaultAsync(m => m.MedicationTypeCode == id);

                if (type == null)
                {
                    return NotFound();
                }
                return Ok(type);
            });
        }

        [HttpGet("staff")]
        public async Task<ActionResult<IEnumerable<Staff>>> GetStaff()
        {
            return await VerifyTokenAndExecute(async () =>
            {
                var staff = await _context.Staff.ToListAsync();
                return Ok(staff);
            });
        }

        [HttpGet("staff/{id}")]
        public async Task<ActionResult<Staff>> GetStaffById(int id)
        {
            return await VerifyTokenAndExecute(async () =>
            {
                var staff = await _context.Staff.FindAsync(id);
                if (staff == null)
                {
                    return NotFound();
                }
                return Ok(staff);
            });
        }

        [HttpGet("patient-medications")]
        public async Task<ActionResult<IEnumerable<PatientMedication>>> GetPatientMedications()
        {
            return await VerifyTokenAndExecute(async () =>
            {
                var patientMedications = await _context.PatientMedications
                    .Include(pm => pm.Patient)
                    .Include(pm => pm.Medication)
                    .ToListAsync();

                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true
                };

                return new JsonResult(patientMedications, options);
            });
        }


        [HttpGet("patient-medications/{id}")]
        public async Task<ActionResult<PatientMedication>> GetPatientMedicationById(int id)
        {
            return await VerifyTokenAndExecute(async () =>
            {
                var patientMedication = await _context.PatientMedications
                    .Include(pm => pm.Patient)
                    .Include(pm => pm.Medication)
                    .FirstOrDefaultAsync(pm => pm.PatientMedicationId == id);

                if (patientMedication == null)
                {
                    return NotFound();
                }

                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true
                };

                return new JsonResult(patientMedication, options);
            });
        }


        [HttpGet("appointments")]
        public async Task<IActionResult> GetAppointments(
             [FromQuery] DateTime? fromDate,
             [FromQuery] DateTime? toDate,
             [FromQuery] List<int>? appointmentIds,
             [FromQuery] string? patientLastNameStart,
             [FromQuery] string? patientLastNameEnd
         )
        {
            return await VerifyTokenAndExecute(async () =>
            {
                var appointmentsQuery = _context.Appointments
                .Include(a => a.Patient)
                .Include(a => a.Staff)
                .AsQueryable();

                if (fromDate.HasValue)
                {
                    fromDate = DateTime.SpecifyKind(fromDate.Value, DateTimeKind.Utc);
                    var startOfDay = fromDate.Value.Date;
                    appointmentsQuery = appointmentsQuery.Where(a => a.DateTimeOfAppointment >= startOfDay);
                }

                if (toDate.HasValue)
                {
                    toDate = DateTime.SpecifyKind(toDate.Value, DateTimeKind.Utc);
                    var endOfDay = toDate.Value.Date.AddDays(1).AddMilliseconds(-1);
                    appointmentsQuery = appointmentsQuery.Where(a => a.DateTimeOfAppointment <= endOfDay);
                }

                if (appointmentIds != null && appointmentIds.Any())
                    appointmentsQuery = appointmentsQuery.Where(a => appointmentIds.Contains(a.AppointmentId));

                var appointments = await appointmentsQuery.ToListAsync();

                if (!string.IsNullOrEmpty(patientLastNameStart))
                {
                    appointments = appointments.Where(a =>
                        a.Patient.LastName.StartsWith(patientLastNameStart, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                if (!string.IsNullOrEmpty(patientLastNameEnd))
                {
                    appointments = appointments.Where(a =>
                        a.Patient.LastName.EndsWith(patientLastNameEnd, StringComparison.OrdinalIgnoreCase)).ToList();
                }

                appointments = appointments.OrderBy(a => a.DateTimeOfAppointment).ToList();

                var ukraineTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Kiev");

                foreach (var appointment in appointments)
                {
                    appointment.DateTimeOfAppointment = TimeZoneInfo.ConvertTimeFromUtc(appointment.DateTimeOfAppointment, ukraineTimeZone);
                }

                var options = new JsonSerializerOptions
                {
                    ReferenceHandler = ReferenceHandler.Preserve,
                    WriteIndented = true
                };

                return new JsonResult(appointments, options);
            });
        }
    }
}