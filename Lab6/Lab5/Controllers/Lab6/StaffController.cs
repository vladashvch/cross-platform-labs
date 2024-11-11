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
        public IActionResult GetPerson(int id)
        {
            ViewData["id"] = id;
            ViewData["ApiUrl"] = Env.GetString("API_LAB_6");
            return View();
        }
    }
}
