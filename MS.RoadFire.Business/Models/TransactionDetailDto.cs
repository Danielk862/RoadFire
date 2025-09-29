using System.ComponentModel.DataAnnotations;

namespace MS.RoadFire.Business.Models
{
    public class TransactionDetailDto
    {
        public int Id { get; set; }

        public int TransactionId { get; set; }

        [Required]
        public int ProductId { get; set; }
        public string ProductDescription { get; set; } = string.Empty;

        [Required]
        public int Quantity { get; set; }

        public decimal UnitValue { get; set; }

        public decimal Total { get; set; }
    }
}
