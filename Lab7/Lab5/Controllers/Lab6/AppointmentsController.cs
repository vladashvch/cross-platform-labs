using DotNetEnv;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Controllers
{
    public class AppointmentsController : Controller
    {
        [HttpGet("appointments")]
        public IActionResult Index()
        {
            ViewData["ApiUrl"] = Env.GetString("API_LAB_6");
            return View();
        }
    }
}
