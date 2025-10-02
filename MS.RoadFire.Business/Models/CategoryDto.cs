using System.ComponentModel.DataAnnotations;

namespace MS.RoadFire.Business.Models
{
    public class CategoryDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Nombre de la categoria.
        /// </summary>
        /// <example>Aceites</example>
        [Required]
        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Descripción de la categoria.
        /// </summary>
        /// <example>Linea de aceites para la motocicleta</example>
        [Required]
        [StringLength(100, MinimumLength = 1)]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Estado de la categoria
        /// </summary>
        /// <example>True</example>
        public bool IsActive { get; set; }
    }
}
