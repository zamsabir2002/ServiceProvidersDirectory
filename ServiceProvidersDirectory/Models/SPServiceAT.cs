using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceProvidersDirectory.Models
{
    public class SPServiceAT
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey("SPServiceId")]
        public required SPService SPService { get; set; }
        public required Guid SPServiceId { get; set; }

        [ForeignKey("SPId")]
        public required ServiceProvider SP { get; set; }
        public required Guid SPId { get; set; }

        [ForeignKey("ServiceId")]
        public required Service Service { get; set; }
        public required Guid ServiceId { get; set; }

        [StringLength(50)]
        public required string SPName { get; set; }
        public required string ServiceName { get; set; }
        public required string HowToRefer { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        public required string Email { get; set; }
        public required string Information { get; set; }
        public bool IsApproved { get; set; } = false;

        [ForeignKey("RequestTypeId")]
        public required RequestType RequestType { get; set; }
        public required int RequestTypeId { get; set; }

        [ForeignKey("SectionId")]
        public required Section Section { get; set; }
        public required int SectionId { get; set; }


        [ForeignKey("RequestedById")]
        public User? RequestedBy { get; set; }
        public Guid? RequestedById { get; set; }
        public DateTime RequestedAt { get; set; } = DateTime.UtcNow;
        [ForeignKey("ApprovedById")]
        public User? ApprovedBy { get; set; }
        public Guid? ApprovedById { get; set; }
        public DateTime ApprovedAt { get; set; } = DateTime.UtcNow;
    }
}
