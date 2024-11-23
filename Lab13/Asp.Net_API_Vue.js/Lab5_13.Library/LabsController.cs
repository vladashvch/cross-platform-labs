using Lab5.Library;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace Lab5.Controllers
{

    [ApiController]
    [Route("[get-labs]")]
    public class LabsController : Controller
    {
        [HttpPost("/lab1")]
        public IActionResult Lab1Post([FromBody] string inputData)
        {
            return ProcessLab("Lab1", inputData);
        }

        [HttpPost("/lab2")]
        public IActionResult Lab2Post([FromBody] string inputData)
        {
            return ProcessLab("Lab2", inputData);
        }

        [HttpPost("/lab3")]
        public IActionResult Lab3Post([FromBody] string inputData)
        {
            return ProcessLab("Lab3", inputData);
        }
        private IActionResult ProcessLab(string labName, string inputData)
        {
            LabsLibrary labLibrary = new LabsLibrary(labName);
            labLibrary.WriteToInputFile(inputData);
            labLibrary.StartLab();
            string outputData = labLibrary.ReadOutputFile();

            return Ok(outputData);
        }
    }
}
