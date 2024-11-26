using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceProvidersDirectory.Models
{
    public class ServiceAT
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey("ServiceId")]
        public required Service Service { get; set; }
        public required Guid ServiceId { get; set; }

        [StringLength(50)]
        public required string Name { get; set; }
        public bool IsApproved { get; set; } = false;
        public required RequestType RequestType { get; set; }
        [ForeignKey("RequestedById")]
        public User? RequestedBy { get; set; }
        public Guid RequestedById { get; set; }
        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
        [ForeignKey("ApprovedById")]
        public User? ApprovedBy { get; set; }
        public Guid? ApprovedById { get; set; }
        public DateTime ApprovedAt { get; set; } = DateTime.UtcNow;
    }
}
