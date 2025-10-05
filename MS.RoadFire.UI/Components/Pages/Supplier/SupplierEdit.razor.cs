using Microsoft.AspNetCore.Components;
using MS.RoadFire.Business.Models;
using MS.RoadFire.UI.Models;
using MS.RoadFire.UI.Repositories;
using MudBlazor;
using System.Net;

namespace MS.RoadFire.UI.Components.Pages.Supplier
{
    public partial class SupplierEdit
    {
        private SupplierDto? Supplier;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private ISnackbar Snackbar { get; set; } = null!;
        [Parameter] public int Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var responseHttp = await Repository.GetAsync<ResponseDto<SupplierDto>>($"api/Supplier/Get/{Id}");

            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("suppliers");
                }
                else
                {
                    var messageError = await responseHttp.GetErrorMessageAsync();
                    Snackbar.Add(messageError!, Severity.Error);
                }
            }
            else
            {
                Supplier = responseHttp.Response!.Data;
            }
        }
        private async Task EditAsync()
        {
            var responseHttp = await Repository.PutAsync("api/Supplier/Update", Supplier);
            if (responseHttp.Error) { var messageError = await responseHttp.GetErrorMessageAsync(); Snackbar.Add(messageError!, Severity.Error); return; }
            Return(); Snackbar.Add("Registro actualizado.", Severity.Success);
        }

        private void Return()
        {
            NavigationManager.NavigateTo("suppliers");
        }

    }
}