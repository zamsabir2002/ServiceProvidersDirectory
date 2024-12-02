using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceProvidersDirectory.Models
{
    public class HospitalService
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [ForeignKey("HospitalId")]
        public Hospital? Hospital { get; set; }
        public Guid HospitalId { get; set; }


        [ForeignKey("ServiceId")]
        public Service? Service { get; set; }
        public Guid ServiceId { get; set; }

        [ForeignKey("UpdatedById")]
        public User? UpdatedBy { get; set; }
        public Guid? UpdatedById { get; set; }

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
