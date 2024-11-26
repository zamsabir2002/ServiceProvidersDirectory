using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceProvidersDirectory.Models
{
    public class ServiceProvider
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [StringLength(50)]
        public required string Name { get; set; }
        public required string PostCode { get; set; }
        [MinLength(8)]
        public required string Website { get; set; }
        public required string Phone { get; set; }
        public bool IsActive { get; set; } = true;

        [ForeignKey("CreatedById")]
        public User? CreatedBy { get; set; }
        public Guid? CreatedById { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [ForeignKey("UpdatedById")]
        public User? UpdatedBy { get; set; }
        public Guid? UpdatedById { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
