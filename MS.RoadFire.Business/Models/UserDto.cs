using MS.RoadFire.Common.Resource;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MS.RoadFire.Business.Models
{
    public class UserDto
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.Required))]
        [StringLength(50, MinimumLength = 1)]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.Required))]
        [StringLength(25, MinimumLength = 1)]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessageResourceType = typeof(MessagesResource), ErrorMessageResourceName = nameof(MessagesResource.Required))]
        public bool State { get; set; }

        public int EmployeeId { get; set; }
        public int RoleId { get; set; }

        [JsonIgnore]
        public string RoleName { get; set; } = string.Empty;
    }
}
