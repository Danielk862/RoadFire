using Microsoft.AspNetCore.Components;
using MS.RoadFire.Business.Models;

namespace MS.RoadFire.UI.Components.Pages.Roles
{
    public partial class RoleForm
    {
        // Cambiamos el nombre para evitar conflictos con el namespace "Roles"
        [EditorRequired, Parameter] public RoleDto CurrentRole { get; set; } = new();

        [EditorRequired, Parameter] public EventCallback OnValidSubmit { get; set; }
        [EditorRequired, Parameter] public EventCallback ReturnAction { get; set; }
        [Parameter] public bool IsEdit { get; set; }
    }
}