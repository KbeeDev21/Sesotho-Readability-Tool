using Microsoft.AspNetCore.Http;
using Sesotho_Readability_Tool.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;


namespace Sesotho_Readability_Tool.Services
{
    public class ReadabilityService
    {
        private readonly HashSet<string> _commonWords;

        public ReadabilityService(string wordListPath)
        {
            if (!File.Exists(wordListPath))
                throw new FileNotFoundException("Word list not found", wordListPath);

            // Load common Sesotho words into a HashSet for quick lookup
            _commonWords = File.ReadAllLines(wordListPath)
                               .Select(w => w.Trim().ToLower())
                               .ToHashSet();
        }

        // Main method: calculates DCI and returns detailed result
        public ReadabilityResult CalculateDCI(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return new ReadabilityResult();

            // Split text into words
            var words = text.Split(new char[] { ' ', '\n', '\r', '\t', '.', ',', ';', '!', '?' },
                                   StringSplitOptions.RemoveEmptyEntries)
                            .Select(w => w.Trim().ToLower())
                            .ToArray();

            int totalWords = words.Length;

            // Identify difficult words (not in the common words list)
            var difficultWordsList = words.Where(w => !_commonWords.Contains(w)).Distinct().ToArray();
            int difficultWords = difficultWordsList.Length;

            // Example DCI calculation: (number of difficult words / total words) * 100
            double dci = totalWords > 0 ? (difficultWords * 100.0 / totalWords) : 0;

            return new ReadabilityResult
            {
                TotalWords = totalWords,
                DifficultWords = difficultWords,
                DCI = Math.Round(dci, 2),
                DifficultWordList = difficultWordsList
            };
        }
    }
}
