using MS.RoadFire.Common.Resource;
using System.ComponentModel.DataAnnotations;

namespace MS.RoadFire.Business.Models
{
    public class SupplierDto
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.Required))]
        [StringLength(120, ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.StringLength), MinimumLength = 1)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.Required))]
        [StringLength(120, ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.StringLength), MinimumLength = 1)]
        public string Address { get; set; } = string.Empty;

        [Display(ResourceType = typeof(MessagesResource) ,Name = nameof(MessagesResource.Phone))]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Ingrese solo números")]
        [Required(ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.Required))]
        [StringLength(10, ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.StringLength), MinimumLength = 10)]
        public string Phone { get; set; } = string.Empty;

        [Display(ResourceType = typeof(MessagesResource), Name = nameof(MessagesResource.Phone))]
        [RegularExpression(@"^\d{7,15}$", ErrorMessage = "Ingrese solo números")]
        [Required(ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.Required))]
        [StringLength(15, ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.StringLength), MinimumLength = 1)]
        public string Nit { get; set; } = string.Empty;

        [Required(ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.Required))]
        [StringLength(120, ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.StringLength), MinimumLength = 1)]
        public string ContactPerson { get; set; } = string.Empty;

        [Required(ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.Required))]
        [StringLength(120, ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.StringLength), MinimumLength = 1)]
        public string Website { get; set; } = string.Empty;

        [Required(ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.Required))]
        [StringLength(250, ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.StringLength), MinimumLength = 1)]
        public string Description { get; set; } = string.Empty;

        public DateTime RegistrationDate { get; set; }
    }
}
