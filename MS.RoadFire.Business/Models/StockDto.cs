namespace MS.RoadFire.Business.Models
{
    public class StockDto
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public string ProductDescription { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public decimal ValueUnit { get; set; }

        public decimal Total { get; set; } 
    }
}
