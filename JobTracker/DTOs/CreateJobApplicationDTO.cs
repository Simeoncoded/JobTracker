using System.ComponentModel.DataAnnotations;

namespace JobTracker.DTOs
{
    public class CreateJobApplicationDTO
    {
        [Required]
        public int CompanyId { get; set; }

        [Required]
        [MaxLength(100)]
        public string JobTitle { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = string.Empty;

        [Required]
        public DateOnly AppliedDate { get; set; }

        [MaxLength(500)]
        public string? JobUrl { get; set; }

        [MaxLength(100)]
        public string? Location { get; set; }

        public decimal? Salary { get; set; }

        public string? Notes { get; set; }
    }
}
