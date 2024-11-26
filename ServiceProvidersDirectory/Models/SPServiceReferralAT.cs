using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceProvidersDirectory.Models
{
    public class SPServiceReferralAT
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();


        [ForeignKey("SPId")]
        public required ServiceProvider SP { get; set; }
        public required Guid SPId { get; set; }

        [ForeignKey("ServiceId")]
        public required Service Service { get; set; }
        public required Guid ServiceId { get; set; }

        [ForeignKey("SPServiceReferralId")]
        public required SPServiceReferral SPServiceReferral  { get; set; }
        public required Guid SPServiceReferralId  { get; set; }


        [StringLength(50)]
        public required string DocumentName { get; set; }
        public bool IsApproved { get; set; } = false;

        [ForeignKey("RequestTypeId")]
        public required RequestType RequestType { get; set; }
        public required int RequestTypeId { get; set; }

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
