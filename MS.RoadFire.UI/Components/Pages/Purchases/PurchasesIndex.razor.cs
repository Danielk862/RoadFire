using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MS.RoadFire.Business.Models;
using MS.RoadFire.UI.Models;
using MS.RoadFire.UI.Repositories;
using MudBlazor;

namespace MS.RoadFire.UI.Components.Pages.Purchases
{
    public partial class PurchasesIndex
    {
        private PurchaseDto purchase = new PurchaseDto();

        private List<ProductDto> Products = new List<ProductDto>();
        private ProductDto? SelectProduct;

        private List<SupplierDto> Supplier = new List<SupplierDto>();
        private SupplierDto? SelectSupplier;

        private int quantity = 0;
        private bool CanAddProduct => SelectProduct is not null && quantity > 0;
        private decimal TotalAmount = 0;

        private bool loading;
        private const string baseUrl = "api/Purchase/";

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private IDialogService DialogService { get; set; } = null!;
        [Inject] private ISnackbar Snackbar { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private ProtectedLocalStorage localStorage { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            purchase.Date = DateTime.Now;

            purchase = new PurchaseDto
            {
                Type = "Compra"
            };

            await LoadProducts();
            await LoadCustomer();
        }

        private async Task LoadCustomer()
        {
            loading = true;
            var url = $"api/Supplier/GetCombo";

            var responseHttp = await Repository.GetAsync<ResponseDto<List<SupplierDto>>>(url);

            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                Snackbar.Add(message!, Severity.Error);
                return;
            }

            Supplier = responseHttp.Response!.Data!;
            loading = false;
        }

        private Task<IEnumerable<SupplierDto>> SearchSupplierAsync(string value, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Task.FromResult(Supplier.AsEnumerable());

            var result = Supplier
                .Where(p => p.Name.Contains(value, StringComparison.OrdinalIgnoreCase))
                .AsEnumerable();

            return Task.FromResult(result);
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

        private void AddProduct()
        {
            loading = true;
            if (SelectProduct is null) return;

            var existing = purchase.PurchaseDetailsDtos.FirstOrDefault(p => p.ProductId == SelectProduct.Id);
            if (existing != null)
            {
                existing.Quantity += quantity;
                existing.Total = SelectProduct.Price * existing.Quantity;
            }
            else
            {
                purchase.PurchaseDetailsDtos.Add(new PurchaseDetailsDto
                {
                    ProductId = SelectProduct.Id,
                    ProductDescription = SelectProduct.Description,
                    Quantity = quantity,
                    UnitValue = SelectProduct.Price,
                    Total = SelectProduct.Price * quantity,
                });

            }

            SelectProduct = null;
            quantity = 0;
            TotalAmount = purchase.PurchaseDetailsDtos.Sum(x => x.Total);
            loading = false;
        }

        private void RemoveProduct(PurchaseDetailsDto item)
        {
            purchase.PurchaseDetailsDtos.Remove(item);
            TotalAmount = purchase.PurchaseDetailsDtos.Sum(x => x.Total);
        }

        private async Task SavePurchase()
        {
            var user = await localStorage.GetAsync<int>("idUser");
            var supplier = SelectSupplier;
            var sales = purchase;
            purchase.Date = DateTime.Now;
            purchase.UserId = user.Value;
            purchase.SupplierId = supplier!.Id;

            if (!await ValidData(purchase))
            {
                var message = "Todos los datos deben ser completados";
                Snackbar.Add(message!, Severity.Info);
                return;
            }

            var responseHttp = await Repository.PostAsync($"{baseUrl}Add", purchase);

            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                Snackbar.Add(message!, Severity.Error);
                return;
            }

            loading = true;
            Snackbar.Add("Compra realizada con éxito", Severity.Success);
            await Task.Delay(2000);
            Return();
            loading = false;
        }

        private async Task<bool> ValidData(PurchaseDto purchase)
        {
            await Task.CompletedTask;
            if (purchase.SupplierId.Equals(0) || purchase.UserId.Equals(0)) return false;
            if (string.IsNullOrEmpty(purchase.Description) || string.IsNullOrEmpty(purchase.Type)) return false;
            if (purchase.PurchaseDetailsDtos.Count() == 0) return false;
            return true;
        }

        private void Return()
        {
            NavigationManager.NavigateTo("/purchase", forceLoad: true);
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