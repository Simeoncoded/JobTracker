namespace JobTracker.DTOs
{
    public class CreateJobApplicationDTO
    {
        public string CompanyName { get; set; } = string.Empty;
        public string JobTitle { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
