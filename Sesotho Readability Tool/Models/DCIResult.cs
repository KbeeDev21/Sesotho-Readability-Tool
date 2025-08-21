namespace Sesotho_Readability_Tool.Models
{
    public record DCIResult(
        double DCI,
        int WordCount,
        int SentenceCount,
        int DifficultWordCount
    );
}
