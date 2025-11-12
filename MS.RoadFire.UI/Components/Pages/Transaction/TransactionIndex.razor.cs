using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MS.RoadFire.Business.Models;
using MS.RoadFire.UI.Models;
using MS.RoadFire.UI.Repositories;
using MudBlazor;

namespace MS.RoadFire.UI.Components.Pages.Transaction
{
    public partial class TransactionIndex
    {
        private TransactionDto transaction = new TransactionDto();

        private List<ProductDto> Products = new List<ProductDto>();
        private ProductDto? SelectProduct;

        private List<string> listTypes = new List<string>();
        private string? selectType;

        private int quantity = 0;
        private bool CanAddProduct => SelectProduct is not null && quantity > 0;
        private decimal TotalAmount = 0;

        private bool loading;
        private const string baseUrl = "api/Transaction/";

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private IDialogService DialogService { get; set; } = null!;
        [Inject] private ISnackbar Snackbar { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private ProtectedLocalStorage localStorage { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            transaction.Date = DateTime.Now;
            await LoadProducts();
            await LoadTypes();
        }

        private async Task LoadTypes()
        {
            await Task.CompletedTask;
            listTypes = new List<string> { "Entrada", "Salida" };
        }

        private Task<IEnumerable<string>> SearchTypes(string value, CancellationToken token)
        {
            IEnumerable<string> result;

            if (string.IsNullOrWhiteSpace(value))
                result = listTypes;
            else
                result = listTypes.Where(x =>
                    x.Contains(value, StringComparison.OrdinalIgnoreCase));

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

            var existing = transaction.TransactionDetailDtos.FirstOrDefault(p => p.ProductId == SelectProduct.Id);
            if (existing != null)
            {
                existing.Quantity += quantity;
                existing.Total = SelectProduct.Price * existing.Quantity;
            }
            else
            {
                transaction.TransactionDetailDtos.Add(new TransactionDetailDto
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
            TotalAmount = transaction.TransactionDetailDtos.Sum(x => x.Total);
            loading = false;
        }

        private void RemoveProduct(TransactionDetailDto item)
        {
            transaction.TransactionDetailDtos.Remove(item);
            TotalAmount = transaction.TransactionDetailDtos.Sum(x => x.Total);
        }

        private async Task SaveTransaction()
        {
            var user = await localStorage.GetAsync<int>("idUser");
            var transa = transaction;
            transa.Type = selectType!;
            transaction.Date = DateTime.Now;
            transaction.UserId = user.Value;

            if (!await ValidData(transa))
            {
                var message = "Todos los datos deben ser completados";
                Snackbar.Add(message!, Severity.Info);
                return;
            }

            var responseHttp = await Repository.PostAsync($"{baseUrl}Add", transaction);

            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                Snackbar.Add(message!, Severity.Error);
                return;
            }

            loading = true;
            Snackbar.Add("Movimiento realizado con éxito", Severity.Success);
            await Task.Delay(2000);
            Return();
            loading = false;
        }

        private async Task<bool> ValidData(TransactionDto transaction)
        {
            await Task.CompletedTask;
            if (transaction.UserId.Equals(0)) return false;
            if (string.IsNullOrEmpty(transaction.Description) || string.IsNullOrEmpty(transaction.Type)) return false;
            if (transaction.TransactionDetailDtos.Count() == 0) return false;
            return true;
        }

        private void Return()
        {
            NavigationManager.NavigateTo("/transaction", forceLoad: true);
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