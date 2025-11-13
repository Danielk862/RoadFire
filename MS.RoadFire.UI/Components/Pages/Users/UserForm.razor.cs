using Microsoft.AspNetCore.Components;
using MS.RoadFire.Business.Models;

namespace MS.RoadFire.UI.Components.Pages.Users;

public partial class UserForm
{
    [EditorRequired, Parameter] public UserDto CurrentUser { get; set; } = new();
    [EditorRequired, Parameter] public List<EmployeeDto> Employees { get; set; } = new();
    [EditorRequired, Parameter] public List<RoleDto> Roles { get; set; } = new();
    [EditorRequired, Parameter] public EventCallback OnValidSubmit { get; set; }
    [EditorRequired, Parameter] public EventCallback ReturnAction { get; set; }
    [Parameter] public bool IsEdit { get; set; }


}