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
        private readonly HashSet<string> commonWords;

        public ReadabilityService(string commonWordsFilePath)
        {
            if (!File.Exists(commonWordsFilePath))
                throw new FileNotFoundException("Common words file not found.", commonWordsFilePath);

            commonWords = new HashSet<string>(
                File.ReadAllLines(commonWordsFilePath)
                    .Select(w => w.Trim().ToLower()));
        }

        private int CountSentences(string text)
        {
            var sentences = Regex.Split(text, @"[.!?]+")
                .Where(s => !string.IsNullOrWhiteSpace(s));
            return sentences.Count();
        }

        private List<string> TokenizeWords(string text)
        {
            return Regex.Matches(text.ToLower(), @"\b\w+\b")
                .Cast<Match>()
                .Select(m => m.Value)
                .ToList();
        }

        public DCIResult CalculateDCI(string text)
        {
            int sentenceCount = CountSentences(text);
            var words = TokenizeWords(text);
            int wordCount = words.Count;

            if (sentenceCount == 0 || wordCount == 0)
                return new DCIResult(0, wordCount, sentenceCount, 0);

            int difficultWordCount = words.Count(w => !commonWords.Contains(w));

            double dci = 4.66547 + 0.14199 * ((double)wordCount / sentenceCount)
                         + 0.03264 * (((double)difficultWordCount / wordCount) * 100);

            return new DCIResult(dci, wordCount, sentenceCount, difficultWordCount);
        }
    }
}
