using System.ComponentModel.DataAnnotations;

namespace MS.RoadFire.Business.Models
{
    public class CategoryDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Description { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}
