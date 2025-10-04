using System.ComponentModel.DataAnnotations;

namespace MS.RoadFire.Business.Models
{
    public class SaleDetailsDto
    {
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int SaleId { get; set; }

        /// <summary>
        /// Id del producto
        /// </summary>
        /// <example>1</example>
        [Required]
        public int ProductId { get; set; }
        /// <summary>
        /// Descripción del producto
        /// </summary>
        /// <example>Aceite mobile</example>
        public int ProductDescription { get; set; }

        /// <summary>
        /// Cantidad del producto
        /// </summary>
        /// <example>25</example>
        [Required]
        public int Quantity { get; set; }

        /// <summary>
        /// Valor unitario
        /// </summary>
        /// <example>1500</example>
        public decimal UnitValue { get; set; }

        /// <summary>
        /// Total inventario en pesos
        /// </summary>
        /// <example>250000</example>
        public decimal Total { get; set; }
    }
}
