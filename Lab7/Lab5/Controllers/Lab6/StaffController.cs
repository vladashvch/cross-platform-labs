using DotNetEnv;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Controllers
{
    public class StaffController : Controller
    {
        [HttpGet("staff")]
        public IActionResult GetStaff()
        {
            ViewData["ApiUrl"] = Env.GetString("API_LAB_6");
            return View();
        }

        [HttpGet("staff/{id}")]
        public IActionResult GetStaffId(int id)
        {
            ViewData["id"] = id;
            ViewData["ApiUrl"] = Env.GetString("API_LAB_6");
            return View();
        }

        [HttpGet("staff/create")]
        public IActionResult CreateStaff()
        {
            ViewData["ApiUrl"] = Env.GetString("API_LAB_6");
            return View();
        }

        [HttpGet("staff/edit/{id}")]
        public IActionResult UpdateStaff(int id)
        {
            ViewData["id"] = id;
            ViewData["ApiUrl"] = Env.GetString("API_LAB_6");
            return View();
        }
    }
}
