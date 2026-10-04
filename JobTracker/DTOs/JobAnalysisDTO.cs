namespace JobTracker.DTOs
{
    public class JobAnalysisDTO
    {
        public int Id { get; set; }
        public int ResumeId { get; set; }
        public string JobTitle { get; set; } = string.Empty;
        public string JobDescription { get; set; } = string.Empty;
        public int MatchScore { get; set; }
        public List<string> MatchingSkills { get; set; } = new List<string>();
        public List<string> MissingSkills { get; set; } = new List<string>();
        public string Recommendation { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
