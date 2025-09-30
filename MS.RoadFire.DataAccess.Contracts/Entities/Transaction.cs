using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MS.RoadFire.DataAccess.Contracts.Entities
{
    [Table("Transactions")]
    public class Transaction
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string Type { get; set; } = string.Empty;

        [Required]
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User? User { get; set; }
    }
}
