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
    public class PatientMedicationController : Controller
    {
        private readonly DataContext _context;
        private readonly TokenVerificationService _tokenVerificationService;

        public PatientMedicationController(DataContext context, TokenVerificationService tokenVerificationService)
        {
            _context = context;
            _tokenVerificationService = tokenVerificationService;
        }

        [HttpGet("patient-medications")]
        public async Task<ActionResult<IEnumerable<PatientMedication>>> GetPatientMedications()
        {
            return await _tokenVerificationService.VerifyTokenAndExecute(async () =>
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
            }, HttpContext);
        }


        [HttpGet("patient-medications/{id}")]
        public async Task<ActionResult<PatientMedication>> GetPatientMedicationById(int id)
        {
            return await _tokenVerificationService.VerifyTokenAndExecute(async () =>
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
            }, HttpContext);
        }
    }
}
