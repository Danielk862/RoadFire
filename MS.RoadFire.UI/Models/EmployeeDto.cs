namespace MS.RoadFire.UI.Models;
public class EmployeeDto
{
    public int Id { get; set; }
    public string FirtsName { get; set; } = string.Empty;
    public string SecondName { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string SecondSurname { get; set; } = string.Empty;
    public DateTime BornDate { get; set; }
    public string Address { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Mobile { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool IsActive { get; set; }
}
