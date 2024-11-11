using DotNetEnv;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Controllers
{
    public class PatientMedicationsController : Controller
    {
        [HttpGet("patient-medications")]
        public IActionResult GetPatientMedications()
        {
            ViewData["ApiUrl"] = Env.GetString("API_LAB_6");
            return View();
        }

        [HttpGet("patient-medications/{id}")]
        public IActionResult GetPatientMedication(int id)
        {
            ViewData["id"] = id;
            ViewData["ApiUrl"] = Env.GetString("API_LAB_6");
            return View();
        }

    }
}
