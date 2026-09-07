namespace JobTracker.DTOs
{
    public class CreateJobAnalysisDTO
    {
        public int ResumeId { get; set; }

        public string JobTitle { get; set; } = string.Empty;

        public string JobDescription { get; set; } = string.Empty;
    }
}
