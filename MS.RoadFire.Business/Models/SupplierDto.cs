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

        [Required(ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.Required))]
        [StringLength(10, ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.StringLength), MinimumLength = 10)]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.Required))]
        [StringLength(100, ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.StringLength), MinimumLength = 1)]
        public int Nit { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.Required))]
        [StringLength(120, ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.StringLength), MinimumLength = 1)]
        public string ContactPerson { get; set; } = string.Empty;

        [Required(ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.Required))]
        [StringLength(120, ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.StringLength), MinimumLength = 1)]
        public string Website { get; set; } = string.Empty;

        [Required(ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.Required))]
        [StringLength(250, ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.StringLength), MinimumLength = 1)]
        public string Description { get; set; } = string.Empty;
    }
}
