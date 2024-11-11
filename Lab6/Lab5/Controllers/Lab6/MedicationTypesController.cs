using DotNetEnv;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Controllers
{
    public class MedicationTypesController : Controller
    {
        [HttpGet("medication-types")]
        public IActionResult GetMedicationTypes()
        {
            ViewData["ApiUrl"] = Env.GetString("API_LAB_6");
            return View();
        }

        [HttpGet("medication-types/{id}")]
        public IActionResult GetMedicationType(string id)
        {
            ViewData["ApiUrl"] = Env.GetString("API_LAB_6");
            ViewData["id"] = id;
            return View();
        }
    }
}
