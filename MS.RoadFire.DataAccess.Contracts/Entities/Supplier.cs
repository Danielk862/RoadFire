using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MS.RoadFire.DataAccess.Contracts.Entities
{
    [Table("Suppliers")]
    public class Supplier
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(120, MinimumLength = 1)]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(120, MinimumLength = 1)]
        public string Address { get; set; } = string.Empty;

        [Required]
        [StringLength(10, MinimumLength = 10)]
        public string Phone { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public int Nit { get; set; }

        [Required]
        [StringLength(120, MinimumLength = 1)]
        public string ContactPerson { get; set; } = string.Empty;

        [StringLength(120, MinimumLength = 1)]
        public string Website { get; set; } = string.Empty;

        [StringLength(250, MinimumLength = 1)]
        public string Description { get; set; } = string.Empty;

        public DateTime RegistrationDate { get; set; }

    }
}
