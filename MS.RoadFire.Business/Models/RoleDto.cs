using MS.RoadFire.Common.Resource;
using System.ComponentModel.DataAnnotations;

namespace MS.RoadFire.Business.Models
{
    public class RoleDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Nombre rol
        /// </summary>
        /// <example>Administrador</example>
        [Display()]
        [Required(ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.Required))]
        [StringLength(50, ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.StringLength), MinimumLength = 1)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Descripción del rol
        /// </summary>
        /// <example>Rol encargado del sistema.<example>
        [Required(ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.Required))]
        [StringLength(100, ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.StringLength), MinimumLength = 1)]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Estado
        /// </summary>
        /// <example>True</example>
        public bool IsActive { get; set; }
    }
}
