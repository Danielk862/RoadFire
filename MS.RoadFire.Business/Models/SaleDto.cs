using System.ComponentModel.DataAnnotations;

namespace MS.RoadFire.Business.Models
{
    public class SaleDto
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
        [RegularExpression("^(Venta)$", ErrorMessage = "El campo tipo solo permite venta")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// id usuario
        /// </summary>
        /// <example>1</example>
        [Required]
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;

        [Required]
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;

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
