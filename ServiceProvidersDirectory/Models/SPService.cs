using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceProvidersDirectory.Models
{
    public class SPService
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey("ServiceProviderId")]
        public required ServiceProvider SP { get; set; }
        public Guid SPId { get; set; }

        [ForeignKey("ServiceId")]
        public required Service Service { get; set; }
        public Guid ServiceId { get; set; }

        [StringLength(50)]
        public required string HowToRefer { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        public required string Email { get; set; }
        public required string Information { get; set; }

        [ForeignKey("UpdatedById")]
        public User? UpdatedBy { get; set; }
        public Guid? UpdatedById { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
