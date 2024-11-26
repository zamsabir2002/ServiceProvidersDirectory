using System.ComponentModel.DataAnnotations;

namespace ServiceProvidersDirectory.Models
{
    public class Section
    {
        [Key]
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
