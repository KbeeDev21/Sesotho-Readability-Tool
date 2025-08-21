namespace Sesotho_Readability_Tool.Models
{
    public class ReadabilityResult
    {
        public string InputText { get; set; } = "";         // store original input
        public int TotalWords { get; set; }
        public int DifficultWords { get; set; }
        public double DCI { get; set; }
        public string[] DifficultWordList { get; set; } = Array.Empty<string>();

        // Optional advanced stats
        public double AverageWordLength { get; set; }
        public double AverageSentenceLength { get; set; }
        public double ReadabilityScore { get; set; }
        public string[] LongWords { get; set; } = Array.Empty<string>();
    }
}
