namespace JobTracker.Models
{
    public class JobAnalysis
    {
        public int Id { get; set; }

        public int ResumeId { get; set; }

        public Resume Resume { get; set; } = null!;

        public string JobTitle { get; set; } = string.Empty;

        public string JobDescription { get; set; } = string.Empty;

        public int MatchScore { get; set; }

        public string Recommendation { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
        public List<string> MatchingSkills { get; set; } = new List<string>();

        public List<string> MissingSkills { get; set; } = new List<string>();
    }
}
