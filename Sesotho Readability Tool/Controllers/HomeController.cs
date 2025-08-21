using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Sesotho_Readability_Tool.Models;
using Sesotho_Readability_Tool.Models.Sesotho_Readability_Tool.Models;
using Sesotho_Readability_Tool.Services;

namespace Sesotho_Readability_Tool.Controllers
{
    public class HomeController : Controller
    {
        private readonly ReadabilityService _readabilityService;

        public HomeController(ReadabilityService readabilityService)
        {
            _readabilityService = readabilityService;
        }

        // GET: Index
        public IActionResult Index()
        {
            return View();
        }

        // POST: Analyze text or file
        [HttpPost]
        public IActionResult Analyze(InputModel input, IFormFile? file)
        {
            string text = input.Text ?? "";

            // If a file is uploaded, read its content
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

            // Calculate readability
            var result = _readabilityService.CalculateDCI(text); // returns a ReadabilityResult object
            return View("Index", result);
        }
    }
}

