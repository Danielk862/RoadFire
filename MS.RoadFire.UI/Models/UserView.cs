namespace MS.RoadFire.UI.Models
{
    public class UserView
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool State { get; set; }
        public int EmployeeId { get; set; }
        public int RoleId { get; set; }
        public string RoleName { get; set; } = string.Empty;
    }
}