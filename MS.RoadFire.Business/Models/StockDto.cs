namespace MS.RoadFire.Business.Models
{
    public class StockDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Id del producto
        /// </summary>
        /// <example>1</example>
        public int ProductId { get; set; }

        /// <summary>
        /// Descripción del producto
        /// </summary>
        /// <example>Aceite mobile</example>
        public string ProductDescription { get; set; } = string.Empty;

        /// <summary>
        /// Cantidad del producto
        /// </summary>
        /// <example>25</example>
        public int Quantity { get; set; }

        /// <summary>
        /// Valor unitario
        /// </summary>
        /// <example>1500</example>
        public decimal ValueUnit { get; set; }

        /// <summary>
        /// Total inventario en pesos
        /// </summary>
        /// <example>250000</example>
        public decimal Total { get; set; } 
    }
}
