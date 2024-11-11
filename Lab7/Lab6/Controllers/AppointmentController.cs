using Lab6.Data;
using Lab6.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using Lab6.Service;

namespace Lab6.Controllers
{
    [ApiController]
    [Route("lab6")]
    public class AppointmentController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly TokenVerificationService _tokenVerificationService;

        public AppointmentController(DataContext context, TokenVerificationService tokenVerificationService)
        {
            _context = context;
            _tokenVerificationService = tokenVerificationService;
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
            return await _tokenVerificationService.VerifyTokenAndExecute(async () =>
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
            }, HttpContext);
        }
    }
}