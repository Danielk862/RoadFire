using MS.RoadFire.Common.Resource;
using System.ComponentModel.DataAnnotations;

namespace MS.RoadFire.Business.Models
{
    public class EmployeeDto
    {
        public int Id { get; set; }

        /// <summary>
        /// Primer nombre.
        /// </summary>
        /// <example>Daniel</example>
        [Required(ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.Required))]
        [StringLength(50, ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.StringLength), MinimumLength = 1)]
        public string FirtsName { get; set; } = string.Empty;

        /// <summary>
        /// Segundo nombre.
        /// </summary>
        /// <example>Andrés</example>
        public string SecondName { get; set; } = string.Empty;

        /// <summary>
        /// Primer apellido.
        /// </summary>
        /// <example>Gómez</example>
        [Required(ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.Required))]
        [StringLength(50, ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.StringLength), MinimumLength = 1)]
        public string Surname { get; set; } = string.Empty;

        /// <summary>
        /// Segundo apellido.
        /// </summary>
        /// <example></example>
        public string SecondSurname { get; set; } = string.Empty;

        /// <summary>
        /// Fecha de nacimiento
        /// </summary>
        /// <example>1986-02-26</example>
        public DateTime BornDate { get; set; }

        /// <summary>
        /// Dirección residencia
        /// </summary>
        /// <example>calle 23 # 25-25</example>
        [Required(ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.Required))]
        [StringLength(100, ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.StringLength), MinimumLength = 1)]
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// Número telefónico
        /// </summary>
        /// <example>5298574</example>
        public string Phone { get; set; } = string.Empty;

        /// <summary>
        /// Celular
        /// </summary>
        /// <example>3245852458</example>
        public string Mobile { get; set; } = string.Empty;

        /// <summary>
        /// Email
        /// </summary>
        /// <example>example@example.com</example>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Estado
        /// </summary>
        /// <example>True</example>
        public bool IsActive {  get; set; }
    }
}
