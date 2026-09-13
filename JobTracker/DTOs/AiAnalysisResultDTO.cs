namespace JobTracker.DTOs
{
    public class AiAnalysisResultDTO
    {
        public int MatchScore { get; set; }

        public List<string> MatchingSkills { get; set; } = new List<string>();

        public List<string> MissingSkills { get; set; } = new List<string>();

        public string Recommendation { get; set; } = string.Empty;
    }
}
