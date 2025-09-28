using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MS.RoadFire.DataAccess.Contracts.Entities
{
    [Table("Users")]
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [StringLength(25, MinimumLength = 1)]
        public string Password { get; set; } = string.Empty;

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public bool State { get; set; }

        public int EmployeeId { get; set; }
        public int RoleId { get; set; }
        
        [ForeignKey("EmployeeId")]
        public virtual Employee? Employee { get; set; }

        [ForeignKey("RoleId")]
        public virtual Role? Role { get; set; }
    }
}
