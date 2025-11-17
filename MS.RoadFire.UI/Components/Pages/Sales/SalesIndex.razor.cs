using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MS.RoadFire.Business.Models;
using MS.RoadFire.UI.Models;
using MS.RoadFire.UI.Repositories;
using MudBlazor;
using Newtonsoft.Json;

namespace MS.RoadFire.UI.Components.Pages.Sales
{
    public partial class SalesIndex
    {
        private SaleDto sale = new SaleDto();

        private List<ProductDto> Products = new List<ProductDto>();
        private ProductDto? SelectProduct;

        private List<CustomerDto> Customers = new List<CustomerDto>();
        private CustomerDto? SelectCustomer;

        private int quantity = 0;
        private bool CanAddProduct => SelectProduct is not null && quantity > 0;
        private decimal TotalAmount = 0;

        private bool loading;
        private const string baseUrl = "api/Sale/";

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private IDialogService DialogService { get; set; } = null!;
        [Inject] private ISnackbar Snackbar { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private ProtectedLocalStorage localStorage { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            sale.Date = DateTime.Now;

            sale = new SaleDto
            {
                Type = "Venta"
            };

            await LoadProducts();
            await LoadCustomer();
        }

        private async Task LoadCustomer()
        {
            loading = true;
            var url = $"api/Customer/GetCombo";

            var responseHttp = await Repository.GetAsync<ResponseDto<List<CustomerDto>>>(url);

            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                Snackbar.Add(message!, Severity.Error);
                return;
            }

            var response = responseHttp.Response!.Data!;
            var activeCustomer = response.FindAll(x => x.IsActive);
            Customers = activeCustomer;
            loading = false;
        }

        private Task<IEnumerable<CustomerDto>> SearchCustomerAsync(string value, CancellationToken token)
        {
            if (string.IsNullOrWhiteSpace(value))
                return Task.FromResult(Customers.AsEnumerable());

            var result = Customers
                .Where(p => p.FirstName.Contains(value, StringComparison.OrdinalIgnoreCase) || p.Surname.Contains(value, StringComparison.OrdinalIgnoreCase))
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

            var response = responseHttp.Response!.Data!;
            var activeProducts = response.FindAll(x => x.IsActive);
            Products = activeProducts;
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

            var existing = sale.SaleDetailsDtos.FirstOrDefault(p => p.ProductId == SelectProduct.Id);
            if (existing != null)
            {
                existing.Quantity += quantity;
                existing.Total = SelectProduct.Price * existing.Quantity;
            }
            else
            {
                sale.SaleDetailsDtos.Add(new SaleDetailsDto
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
            TotalAmount = sale.SaleDetailsDtos.Sum(x => x.Total);
            loading = false;
        }

        private void RemoveProduct(SaleDetailsDto item)
        {
            sale.SaleDetailsDtos.Remove(item);
            TotalAmount = sale.SaleDetailsDtos.Sum(x => x.Total);
        }

        private async Task SaveSale()
        {
            loading = true;
            StateHasChanged();

            try
            {
                var user = await localStorage.GetAsync<int>("idUser");
                var customer = SelectCustomer;
                var sales = sale;
                sale.Date = DateTime.Now;
                sale.UserId = user.Value;
                sale.CustomerId = customer!.Id;

                if (!await ValidData(sale))
                {
                    var message = "Todos los datos deben ser completados";
                    Snackbar.Add(message!, Severity.Info);
                    return;
                }

                var responseHttp = await Repository.PostAsync($"{baseUrl}Add", sale);

                if (responseHttp.Error)
                {
                    var message = await responseHttp.GetErrorMessageAsync();
                    var json = System.Text.Json.JsonSerializer.Deserialize<ApiResponse>(message!);
                    Snackbar.Add(json!.messages, Severity.Error);
                    return;
                }

                Snackbar.Add("Venta realizada con éxito", Severity.Success);
                await Task.Delay(2000);
                Return();
            }
            finally 
            {
                loading = false;
                StateHasChanged();
            }
        }

        private async Task<bool> ValidData(SaleDto sale)
        {
            await Task.CompletedTask;
            if (sale.CustomerId.Equals(0) || sale.UserId.Equals(0)) return false;
            if (string.IsNullOrEmpty(sale.Description) || string.IsNullOrEmpty(sale.Type)) return false;
            if (sale.SaleDetailsDtos.Count() == 0) return false;
            return true;
        }

        private void Return()
        {
            NavigationManager.NavigateTo("/sales", forceLoad: true);
        }

        private async Task ReturnAction()
        {
            var rol = await localStorage.GetAsync<string>("rol");

            string path = rol.Value switch
            {
                "Administrador" => "/adminProfile",
                "Ventas" => "/salesProfile",
                "Compras" => "/purchasesProfile",
                "Inventario" => "/inventoryProfile",
                _ => "/Home"
            };

            NavigationManager.NavigateTo(path);
        }
    }
}