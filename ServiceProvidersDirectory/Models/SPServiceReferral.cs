using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceProvidersDirectory.Models
{
    public class SPServiceReferral
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();


        [ForeignKey("SPId")]
        public required ServiceProvider SP { get; set; }
        public required Guid SPId { get; set; }

        [ForeignKey("ServiceId")]
        public required Service Service { get; set; }
        public required Guid ServiceId { get; set; }

        [StringLength(50)]
        public required string DocumentName { get; set; }

        [ForeignKey("UpdatedById")]
        public User? UpdatedBy { get; set; }
        public Guid? UpdatedById { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
