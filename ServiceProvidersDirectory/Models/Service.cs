using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceProvidersDirectory.Models
{
    public class Service
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [StringLength(50)]
        public required string Name { get; set; }

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
