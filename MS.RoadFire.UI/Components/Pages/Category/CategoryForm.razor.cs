using Microsoft.AspNetCore.Components;
using MS.RoadFire.Business.Models;

namespace MS.RoadFire.UI.Components.Pages.Category
{
    public partial class CategoryForm
    {
        [EditorRequired, Parameter] public CategoryDto Category { get; set; } = null!;
        [EditorRequired, Parameter] public EventCallback OnValidSubmit { get; set; }
        [EditorRequired, Parameter] public EventCallback ReturnAction { get; set; }
        [Parameter] public bool IsEdit { get; set; }
    }
}