using Microsoft.AspNetCore.Components;
using MS.RoadFire.Business.Models;
using MS.RoadFire.UI.Models;
using MS.RoadFire.UI.Repositories;
using MudBlazor;
using System.Net;

namespace MS.RoadFire.UI.Components.Pages.Products
{
    public partial class ProductsEdit
    {
        private ProductDto? Product;
        private List<CategoryDto> Categories = new();
        private List<SupplierDto> Suppliers = new();
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private ISnackbar Snackbar { get; set; } = null!;
        [Parameter] public int Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadLookupsAsync();
            var responseHttp = await Repository.GetAsync<ResponseDto<ProductDto>>($"api/Product/Get/{Id}");

            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("productsIndex");
                }
                else
                {
                    var messageError = await responseHttp.GetErrorMessageAsync();
                    Snackbar.Add(messageError!, Severity.Error);
                }
            }
            else
            {
                Product = responseHttp.Response!.Data;
            }
        }

        private async Task LoadLookupsAsync()
        {
            var categories = await Repository.GetAsync<ResponseDto<List<CategoryDto>>>("api/Category/GetAll");
            if (!categories.Error && categories.Response?.Data is not null)
                Categories = categories.Response.Data;

            var suppliers = await Repository.GetAsync<ResponseDto<List<SupplierDto>>>("api/Supplier/GetAll");
            if (!suppliers.Error && suppliers.Response?.Data is not null)
                Suppliers = suppliers.Response.Data;
        }

        private async Task EditAsync()
        {
            var responseHttp = await Repository.PutAsync("api/Product/Update", Product);

            if (responseHttp.Error) 
            { 
                var messageError = await responseHttp.GetErrorMessageAsync(); 
                Snackbar.Add(messageError!, Severity.Error); 
                return; 
            }

            Return(); 
            Snackbar.Add("Registro actualizado.", Severity.Success);
        }

        private void Return()
        {
            NavigationManager.NavigateTo("productsIndex", forceLoad: true);
        }
    }
}