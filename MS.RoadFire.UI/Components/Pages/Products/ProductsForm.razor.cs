using Microsoft.AspNetCore.Components;
using MS.RoadFire.Business.Models;

namespace MS.RoadFire.UI.Components.Pages.Products
{
    public partial class ProductsForm
    {
        [EditorRequired, Parameter] public ProductDto Product { get; set; } = null!;
        [EditorRequired, Parameter] public List<CategoryDto> Categories { get; set; } = new();
        [EditorRequired, Parameter] public List<SupplierDto> Suppliers { get; set; } = new();
        [EditorRequired, Parameter] public EventCallback OnValidSubmit { get; set; }
        [EditorRequired, Parameter] public EventCallback ReturnAction { get; set; }
        [Parameter] public bool IsEdit { get; set; }
    }
}