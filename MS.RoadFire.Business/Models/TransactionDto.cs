using System.ComponentModel.DataAnnotations;

namespace MS.RoadFire.Business.Models
{
    public class TransactionDto
    {
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
        ///   - Salida
        ///   - Entrada
        /// </summary>
        /// <example>Salida</example>
        [Required]
        [RegularExpression("^(Salida|Entrada)$", ErrorMessage = "El campo Type solo permite Salida, Entrada")]
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// id usuario
        /// </summary>
        /// <example>1</example>
        [Required]
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;    

        /// <summary>
        /// Lista de detalle de los productos.
        /// </summary>
        /// <example></example>
        public List<TransactionDetailDto> TransactionDetailDtos { get; set; } = new List<TransactionDetailDto>();

        public TransactionDto()
        {
            TransactionDetailDtos = new List<TransactionDetailDto>();
        }
    }
}
