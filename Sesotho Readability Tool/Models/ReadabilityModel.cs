namespace Sesotho_Readability_Tool.Models
{
    public class ReadabilityModel
    {
        public string InputText { get; set; }
        public double AverageWordLength { get; set; }
        public double AverageSentenceLength { get; set; }
        public double ReadabilityScore { get; set; }
        public List<string> LongWords { get; set; }
    }
}
