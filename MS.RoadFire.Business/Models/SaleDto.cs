using System.ComponentModel.DataAnnotations;

namespace MS.RoadFire.Business.Models
{
    internal class SaleDto
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// Fecha del movimiento
        /// </summary>
        /// <example>2025-09-29</example>
        public DateTime Date { get; set; }

        /// <summary>
        /// Descripción del movimiento
        /// </summary>
        /// <example>Venta cliente #######</example>
        [Required]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Tipo de movimiento<br/>
        /// Solo se puede enviar
        ///   - Venta
        /// </summary>
        /// <example>Salida</example>
        [Required]
        [RegularExpression("^(Venta)$", ErrorMessage = "El campo Type solo permite venta")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// id usuario
        /// </summary>
        /// <example>1</example>
        [Required]
        public int UserId { get; set; }

        [Required]
        public int CustomerId { get; set; }

        /// <summary>
        /// Lista de detalle de los productos.
        /// </summary>
        /// <example></example>
        public List<SaleDetailsDto> SaleDetailsDtos { get; set; } = new List<SaleDetailsDto>();

        public SaleDto()
        {
            SaleDetailsDtos = new List<SaleDetailsDto>();
        }
    }
}
