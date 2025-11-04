using Microsoft.AspNetCore.Components;
using MS.RoadFire.Business.Models;

namespace MS.RoadFire.UI.Components.Pages.Employees;

public partial class EmployeesForm
{
    [EditorRequired, Parameter] public EmployeeDto CurrentEmployee { get; set; } = new();
    [EditorRequired, Parameter] public EventCallback OnValidSubmit { get; set; }
    [EditorRequired, Parameter] public EventCallback ReturnAction { get; set; }
    [Parameter] public bool IsEdit { get; set; }
}