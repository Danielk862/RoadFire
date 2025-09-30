using MS.RoadFire.Common.Resource;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MS.RoadFire.Business.Models
{
    public class UserDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Usuario
        /// </summary>
        /// <example>dgomez</example>
        [Required(ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.Required))]
        [StringLength(50, MinimumLength = 1)]
        public string Username { get; set; } = string.Empty;

        /// <summary>
        /// Contraseña
        /// </summary>
        /// <example>123456</example>
        [Required(ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.Required))]
        [StringLength(25, MinimumLength = 1)]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Estado
        /// </summary>
        /// <example>True</example>
        [Required(ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.Required))]
        public bool State { get; set; }

        /// <summary>
        /// id empleado
        /// </summary>
        /// <example>1</example>
        public int EmployeeId { get; set; }

        /// <summary>
        /// Nombre empleado
        /// </summary>
        /// <example>Daniel Gómez</example>
        public string EmployeeName { get; set; } = string.Empty;
        /// <summary>
        /// id rol
        /// </summary>
        /// <example>1</example>
        public int RoleId { get; set; }

        public string RoleName { get; set; } = string.Empty;
    }
}
