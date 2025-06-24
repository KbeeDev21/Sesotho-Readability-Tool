using Microsoft.AspNetCore.Mvc;
using Sesotho_Readability_Tool.Models;

namespace Sesotho_Readability_Tool.Controllers
{
    public class ReadabilityController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ReadabilityModel model)
        {
            if (!string.IsNullOrWhiteSpace(model.InputText))
            {
                var tokens = TokenizeText(model.InputText);
                var sentences = SplitIntoSentences(model.InputText);

                model.AverageWordLength = tokens.Average(w => w.Length);
                model.AverageSentenceLength = sentences.Average(s => s.Split(' ').Length);
                model.ReadabilityScore = model.AverageWordLength + model.AverageSentenceLength; // Basic formula
                model.LongWords = tokens.Where(w => w.Length > 8).Distinct().ToList();
            }

            return View(model);
        }

        private List<string> TokenizeText(string text)
        {
            // Naive tokenizer for Sesotho – improve later
            var separators = new[] { ' ', '.', ',', '!', '?', ':', ';', '\n', '\r' };
            return text.Split(separators, StringSplitOptions.RemoveEmptyEntries).ToList();
        }

        private List<string> SplitIntoSentences(string text)
        {
            return text.Split(new[] { '.', '?', '!' }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }
    }
}
