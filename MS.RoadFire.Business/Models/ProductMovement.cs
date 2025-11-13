namespace MS.RoadFire.Business.Models
{
    public class ProductMovement
    {
        public string Type { get; set; } = string.Empty;
        public DateTime Date { get; set; }
        public int ProductId { get; set; }
        public string Description { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal UnitCost { get; set; }
        public decimal TotalCost { get; set; }
    }
}
