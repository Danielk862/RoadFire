using Microsoft.AspNetCore.Components;
using MS.RoadFire.Business.Models;
using MS.RoadFire.UI.Models;
using MS.RoadFire.UI.Repositories;
using MudBlazor;

namespace MS.RoadFire.UI.Components.Pages.Products
{
    public partial class ProductsCreate
    {
        private ProductDto Product = new()
        {
            IsActive = true
        };

        private List<CategoryDto> Categories = new();
        private List<SupplierDto> Suppliers = new();

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private ISnackbar Snackbar { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            var categories = await Repository.GetAsync<ResponseDto<List<CategoryDto>>>("api/Category/GetAll");
            var suppliers = await Repository.GetAsync<ResponseDto<List<SupplierDto>>>("api/Supplier/GetAll");

            if (categories.Error)
            {
                Snackbar.Add(await categories.GetErrorMessageAsync() ?? "Error cargando empleados", Severity.Error);
            }
            else
            {
                Categories = categories.Response?.Data ?? new();
                if (Categories.Count == 0)
                    Snackbar.Add("No hay empleados disponibles para asignar.", Severity.Info);
            }

            if (suppliers.Error)
            {
                Snackbar.Add(await suppliers.GetErrorMessageAsync() ?? "Error cargando roles", Severity.Error);
            }
            else
            {
                Suppliers = suppliers.Response?.Data ?? new();
                if (Suppliers.Count == 0)
                    Snackbar.Add("No hay roles configurados.", Severity.Info);
            }
        }

        private async Task CreateAsync()
        {
            if (string.IsNullOrWhiteSpace(Product.Description))
            {
                Snackbar.Add("Ingrese el usuario.", Severity.Warning);
                return;
            }

            if (Product.CategoryId <= 0)
            {
                Snackbar.Add("Seleccione una categoría.", Severity.Warning);
                return;
            }

            if (Product.SupplierId <= 0)
            {
                Snackbar.Add("Seleccione un proveedor.", Severity.Warning);
                return;
            }

            var request = new ProductDto
            {
                Sku = Product.Sku,
                Description = Product.Description,
                CategoryId = Product.CategoryId,
                SupplierId = Product.SupplierId,
                IsActive = Product.IsActive
            };

            var responseHttp = await Repository.PostAsync("api/Product/Add", request);

            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                Snackbar.Add(message ?? "No se pudo crear el producto.", Severity.Error);
                return;
            }
            Return();
            Snackbar.Add("Producto creado correctamente", Severity.Success);
        }

        private void Return()
        {
            NavigationManager.NavigateTo("/products");
        }
    }
}