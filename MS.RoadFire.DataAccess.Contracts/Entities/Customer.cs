using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MS.RoadFire.DataAccess.Contracts.Entities
{
    [Table("Customers")]
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(2, MinimumLength = 2)]
        public string TypeIdentification { get; set; } = string.Empty;

        [Required]
        [StringLength(10, MinimumLength = 2)]
        public string Identification { get; set; } = string.Empty;

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string FirstName { get; set; } = string.Empty;
        
        [StringLength(50, MinimumLength = 0)]
        public string SecondName { get; set; } = string.Empty;
        
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Surname { get; set; } = string.Empty;
        
        [StringLength(50, MinimumLength = 0)]
        public string SecondSurname { get; set; } = string.Empty;
        
        [Required]
        [StringLength(120, MinimumLength = 1)]
        public string Address { get; set; } = string.Empty;
        
        [Required]
        [StringLength(10, MinimumLength = 10)]
        public string Phone { get; set; } = string.Empty;
        
        [Required]
        [StringLength(120, MinimumLength = 1)]
        public string Email { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public DateTime RegistrationDate { get; set; }

        public DateTime? UpdateDate { get; set; }
    }
}
