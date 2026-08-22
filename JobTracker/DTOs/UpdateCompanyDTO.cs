using System.ComponentModel.DataAnnotations;

namespace JobTracker.DTOs
{
    public class UpdateCompanyDTO
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Website { get; set; }
    }
}
