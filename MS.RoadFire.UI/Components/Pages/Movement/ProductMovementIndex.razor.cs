using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MS.RoadFire.Business.Models;
using MS.RoadFire.UI.Models;
using MS.RoadFire.UI.Repositories;
using MudBlazor;

namespace MS.RoadFire.UI.Components.Pages.Movement
{
    public partial class ProductMovementIndex
    {
        private List<ProductMovement>? productMovements = new();
        private MudTable<ProductMovement> table = new();
        private readonly int[] pageSizeOptions = { 10, 20, 50, int.MaxValue };
        private int totalRecords = 0;
        private bool loading = true;
        private bool isInitial = true;
        private const string baseUrl = "api/ProductMovement";
        private string infoFormat = "Registro {first_item} de {last_item} Total {all_items}";


        private List<ProductDto> Products = new List<ProductDto>();
        private ProductDto? SelectProduct;

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private IDialogService DialogService { get; set; } = null!;
        [Inject] private ISnackbar Snackbar { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private ProtectedLocalStorage localStorage { get; set; } = null!;

        [Parameter, SupplyParameterFromQuery] public string Filter { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            await LoadProducts();
            await LoadProductMovements();
            await Task.Delay(500);
            isInitial = false;
            loading = false;
        }

        private async Task LoadProducts()
        {
            loading = true;
            var url = $"api/Product/GetCombo";

            var responseHttp = await Repository.GetAsync<ResponseDto<List<ProductDto>>>(url);

            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                Snackbar.Add(message!, Severity.Error);
                return;
            }

            Products = responseHttp.Response!.Data!;
            loading = false;
        }

        private Task<IEnumerable<ProductDto>> SearchProductsAsync(string value, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Task.FromResult(Products.AsEnumerable());

            var result = Products
                .Where(p => p.Description.Contains(value, StringComparison.OrdinalIgnoreCase))
                .AsEnumerable();

            return Task.FromResult(result);
        }

        public void OnProductChange()
        {
            if (SelectProduct == null)
            {
                productMovements!.Clear();
            }
        }

        private async Task LoadProductMovements()
        {
            if (SelectProduct == null && !isInitial)
            {
                Snackbar.Add("Debe seleccionar un producto", Severity.Info);
                return;
            }

            var productId = SelectProduct != null ? SelectProduct!.Id : 0;
            var url = $"{baseUrl}/GetAll?productId={productId}";
            var responseHttp = await Repository.GetAsync<ResponseDto<List<ProductMovement>>>(url);

            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                Snackbar.Add(message!, Severity.Error);
                return;
            }

            productMovements = responseHttp.Response!.Data!;
        }

        private async Task ReturnAction()
        {
            var rol = await localStorage.GetAsync<string>("rol");

            string path = rol.Value switch
            {
                "Administrador" => "/Admin",
                "Ventas" => "/Ventas",
                "Compras" => "/Compras",
                "Inventario" => "/Inventario",
                _ => "/Home"
            };

            NavigationManager.NavigateTo(path);
        }
    }
}