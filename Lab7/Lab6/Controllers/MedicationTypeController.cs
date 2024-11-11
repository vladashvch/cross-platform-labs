using Lab6.Data;
using Lab6.Models;
using Lab6.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers
{
    [ApiController]
    [Route("lab6")]
    public class MedicationTypeController : Controller
    {
        private readonly DataContext _context;
        private readonly TokenVerificationService _tokenVerificationService;

        public MedicationTypeController(DataContext context, TokenVerificationService tokenVerificationService)
        {
            _context = context;
            _tokenVerificationService = tokenVerificationService;
        }

        [HttpGet("v1/medication-types")]
        public async Task<ActionResult<IEnumerable<RefMedicationType>>> GetMedicationTypesV2()
        {
            return await _tokenVerificationService.VerifyTokenAndExecute(async () =>
            {
                var medicationTypes = await _context.MedicationTypes
                    .Select(m => new
                    {
                        m.MedicationTypeCode,
                        m.MedicationTypeName,
                        m.MedicationTypeDescription
                    })
                    .ToListAsync();

                return Ok(medicationTypes);
            }, HttpContext);
        }

        [HttpGet("v2/medication-types")]
        public async Task<ActionResult<IEnumerable<RefMedicationType>>> GetMedicationTypes()
        {
            return await _tokenVerificationService.VerifyTokenAndExecute(async () =>
            {
                var medicationTypes = await _context.MedicationTypes.ToListAsync();
                return Ok(medicationTypes);
            }, HttpContext);
        }

        [HttpGet("medication-types/{id}")]
        public async Task<ActionResult<RefMedicationType>> GetMedicationTypeById(string id)
        {
            return await _tokenVerificationService.VerifyTokenAndExecute(async () =>
            {
                var type = await _context.MedicationTypes
                                          .FirstOrDefaultAsync(m => m.MedicationTypeCode == id);

                if (type == null)
                {
                    return NotFound();
                }
                return Ok(type);
            }, HttpContext);
        }
    }
}
