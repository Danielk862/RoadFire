namespace MS.RoadFire.Business.Models
{
    public class ProductDto
    {
        public int Id { get; set; }
        
        /// <summary>
        /// Sku del producto
        /// </summary>
        /// <example>S00001</example>
        public string Sku { get; set; } = string.Empty;

        /// <summary>
        /// Descripción del producto.
        /// </summary>
        /// <example></example>
        public string Description { get; set; } = string.Empty;
        
        /// <summary>
        /// Precio unitario del producto
        /// </summary>
        /// <example>1200</example>
        public decimal Price { get; set; }
        
        /// <summary>
        /// Id categoria
        /// </summary>
        /// <example></example>
        public int CategoryId { get; set; }
        
        /// <summary>
        /// Descripción de la categoria.
        /// </summary>
        /// <example></example>
        public string CategoryName { get; set; } = string.Empty;
        
        /// <summary>
        /// Estado
        /// </summary>
        /// <example>True</example>
        public bool IsActive { get; set; }
    }
}
