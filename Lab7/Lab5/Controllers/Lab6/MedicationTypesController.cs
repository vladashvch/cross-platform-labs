using DotNetEnv;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Controllers
{
    public class MedicationTypesController : Controller
    {
        [HttpGet("medication-types-1")]
        public IActionResult GetMedicationTypesV1()
        {
            ViewData["ApiUrl"] = Env.GetString("API_LAB_6");
            return View();
        }

        [HttpGet("medication-types-2")]
        public IActionResult GetMedicationTypesV2()
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
