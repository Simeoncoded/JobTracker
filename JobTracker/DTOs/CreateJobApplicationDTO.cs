using System.ComponentModel.DataAnnotations;

namespace JobTracker.DTOs
{
    public class CreateJobApplicationDTO
    {
        [Required]
        [MaxLength(100)]
        public string CompanyName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string JobTitle { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } = string.Empty;
    }
}
