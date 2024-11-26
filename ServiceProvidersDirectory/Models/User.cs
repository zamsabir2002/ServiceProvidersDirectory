using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;

namespace ServiceProvidersDirectory.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [StringLength(50)]
        public required string Name { get; set; }
        public required string Email { get; set; }
        [MinLength(8)]
        public required string Password { get; set; }

        [ForeignKey("RoleId")]
        public required UserRole Role { get; set; }
        public required int RoleId { get; set; }


        public required string Phone { get; set;}
        public bool IsActive { get; set; } = true;
        public string? Department { get; set; }
        [ForeignKey("UserCreatedById")]
        public User? CreatedBy { get; set; }
        public Guid? UserCreatedById { get; set; }

        [ForeignKey("UserUpdatedById")]
        public User? UpdatedBy { get; set; }
        public Guid? UserUpdatedById { get; set; }

        [ForeignKey("HospitalId")]
        public Hospital? Hospital { get; set; }
        public Guid? HospitalId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;


    }
}
