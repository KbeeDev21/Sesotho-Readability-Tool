using Microsoft.AspNetCore.Mvc;
using Sesotho_Readability_Tool.Models;
using Sesotho_Readability_Tool.Models.Sesotho_Readability_Tool.Models;
using Sesotho_Readability_Tool.Services;

namespace Sesotho_Readability_Tool.Controllers
{
    public class ReadabilityController : Controller
    {
        private readonly ReadabilityService _readabilityService;

        public ReadabilityController(ReadabilityService readabilityService)
        {
            _readabilityService = readabilityService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Analyze(InputModel input, IFormFile? file)
        {
            string text = input.Text ?? "";

            if (file != null && file.Length > 0)
            {
                using var reader = new StreamReader(file.OpenReadStream());
                text = reader.ReadToEnd();
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                ViewBag.Error = "Please enter text or upload a file.";
                return View("Index");
            }

            var result = _readabilityService.CalculateDCI(text);
            return View("Index", result);
        }
    }
}
