using Lab6.Data;
using Lab6.Models;
using Lab6.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab6.Controllers
{
    [ApiController]
    [Route("lab6")]
    public class StaffController : Controller
    {
        private readonly DataContext _context;
        private readonly TokenVerificationService _tokenVerificationService;

        public StaffController(DataContext context, TokenVerificationService tokenVerificationService)
        {
            _context = context;
            _tokenVerificationService = tokenVerificationService;
        }

        [HttpGet("staff")]
        public async Task<ActionResult<IEnumerable<Staff>>> GetStaff()
        {
            return await _tokenVerificationService.VerifyTokenAndExecute(async () =>
            {
                var staff = await _context.Staff.ToListAsync();
                return Ok(staff);
            }, HttpContext);
        }

        [HttpGet("staff/{id}")]
        public async Task<ActionResult<Staff>> GetStaffById(int id)
        {
            return await _tokenVerificationService.VerifyTokenAndExecute(async () =>
            {
                var staff = await _context.Staff.FindAsync(id);
                if (staff == null)
                {
                    return NotFound();
                }
                return Ok(staff);
            }, HttpContext);
        }

        [HttpPost("staff")]
        public async Task<ActionResult<Staff>> CreateStaff(Staff staff)
        {
            return await _tokenVerificationService.VerifyTokenAndExecute(async () =>
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                _context.Staff.Add(staff);
                await _context.SaveChangesAsync();

                return Ok();
            }, HttpContext);
        }

        [HttpPut("staff/{id}")]
        public async Task<IActionResult> UpdateStaff(int id, Staff staff)
        {
            return await _tokenVerificationService.VerifyTokenAndExecute(async () =>
            {
                var existingStaff = await _context.Staff.FindAsync(id);
                if (existingStaff == null)
                {
                    return NotFound();
                }

                existingStaff.FirstName = staff.FirstName;
                existingStaff.MiddleName = staff.MiddleName;
                existingStaff.LastName = staff.LastName;
                existingStaff.DateOfBirth = staff.DateOfBirth;
                existingStaff.Gender = staff.Gender;
                existingStaff.Qualifications = staff.Qualifications;
                existingStaff.OtherDetails = staff.OtherDetails;

                _context.Entry(existingStaff).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return Ok();
            }, HttpContext);
        }

        [HttpDelete("staff/{id}")]
        public async Task<IActionResult> DeleteStaff(int id)
        {
            return await _tokenVerificationService.VerifyTokenAndExecute(async () =>
            {
                var staff = await _context.Staff.FindAsync(id);
                if (staff == null)
                {
                    return NotFound();
                }

                _context.Staff.Remove(staff);
                await _context.SaveChangesAsync();

                return Ok();
            }, HttpContext);
        }
    }
}
