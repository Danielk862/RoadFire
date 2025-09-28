using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MS.RoadFire.DataAccess.Contracts.Entities
{
    [Table("Employees")]
    public class Employee
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string FirtsName { get; set; } = string.Empty;

        [StringLength(50, MinimumLength = 1)]
        public string SecondName { get; set; } = string.Empty;

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Surname { get; set; } = string.Empty;

        [StringLength(50, MinimumLength = 1)]
        public string SecondSurname { get; set; } = string.Empty;

        public DateTime BornDate { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Address { get; set; } = string.Empty;

        [StringLength(10, MinimumLength = 1)]
        public string Phone { get; set; } = string.Empty;

        [StringLength(10, MinimumLength = 1)]
        public string Mobile { get; set; } = string.Empty;

        [StringLength(100, MinimumLength = 1)]
        public string Email { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}
