using System.ComponentModel.DataAnnotations.Schema;

namespace MS.RoadFire.DataAccess.Contracts.Entities
{
    [Table("Products")]
    public class Product
    {
        public int Id { get; set; }
        public string Sku { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public virtual Category? Category { get; set; }

        public bool IsActive { get; set; }
    }
}
