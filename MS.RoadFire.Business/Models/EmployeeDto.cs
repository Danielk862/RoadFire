using MS.RoadFire.Common.Resource;
using System.ComponentModel.DataAnnotations;

namespace MS.RoadFire.Business.Models
{
    public class EmployeeDto
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.Required))]
        [StringLength(50, ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.StringLength), MinimumLength = 1)]
        public string FirtsName { get; set; } = string.Empty;

        public string SecondName { get; set; } = string.Empty;

        [Required(ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.Required))]
        [StringLength(50, ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.StringLength), MinimumLength = 1)]
        public string Surname { get; set; } = string.Empty;

        public string SecondSurname { get; set; } = string.Empty;

        public DateTime BornDate { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.Required))]
        [StringLength(100, ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.StringLength), MinimumLength = 1)]
        public string Address { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string Mobile { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public bool IsActive {  get; set; }
    }
}
