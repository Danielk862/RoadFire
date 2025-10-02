using System.Text.Json.Serialization;

namespace MS.RoadFire.UI.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        //public string Password { get; set; } = string.Empty;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? Password { get; set; }

        public bool State { get; set; }
        public int EmployeeId { get; set; }
        public int RoleId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string RoleName { get; set; } = string.Empty;
    }
}