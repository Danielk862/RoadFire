using MS.RoadFire.Common.Resource;
using System.ComponentModel.DataAnnotations;

namespace MS.RoadFire.Business.Models
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.Required))]
        [StringLength(2, ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.StringLength), MinimumLength = 2)]
        public string TypeIdentification { get; set; } = string.Empty;

        [Required(ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.Required))]
        [StringLength(10, ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.StringLength), MinimumLength = 2)]
        public string Identification { get; set; } = string.Empty;

        [Required(ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.Required))]
        [StringLength(50, ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.StringLength), MinimumLength = 1)]
        public string FirstName { get; set; } = string.Empty;

        [StringLength(50, ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.StringLength), MinimumLength = 0)]
        public string SecondName { get; set; } = string.Empty;

        [Required(ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.Required))]
        [StringLength(50, ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.StringLength), MinimumLength = 1)]
        public string Surname { get; set; } = string.Empty;

        [StringLength(50, ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.StringLength), MinimumLength = 0)]
        public string SecondSurname { get; set; } = string.Empty;

        [Required(ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.Required))]
        [StringLength(120, ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.StringLength), MinimumLength = 1)]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.Required))]
        [StringLength(10, ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.StringLength), MinimumLength = 10)]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.Required))]
        [StringLength(120, ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.StringLength), MinimumLength = 1)]
        public string Email { get; set; } = string.Empty;

        public bool IsActive { get; set; }

    }
}
