namespace JobTracker.DTOs
{
    public class JobApplicationResponseDTO
    {
        public int Id { get; set; }

        public int CompanyId { get; set; }

        public string CompanyName { get; set; } = string.Empty;

        public string JobTitle { get; set; } = string.Empty;

        public string Status { get; set; } = string.Empty;

        public DateOnly AppliedDate { get; set; }

        public string? JobUrl { get; set; }

        public string? Location { get; set; }

        public decimal? Salary { get; set; }

        public string? Notes { get; set; }
    }
}
