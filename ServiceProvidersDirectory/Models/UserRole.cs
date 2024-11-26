using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ServiceProvidersDirectory.Models
{
    public enum RoleNames
    {
        SuperAdmin,
        HospitalAdmin,
        DepartmentAdmin,
        User
    }
    public class UserRole
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
