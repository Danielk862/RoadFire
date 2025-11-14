using Microsoft.AspNetCore.Components;
using MS.RoadFire.Business.Models;
using MS.RoadFire.UI.Models;
using MS.RoadFire.UI.Repositories;
using MudBlazor;
using System.Net;

namespace MS.RoadFire.UI.Components.Pages.Customer
{
    public partial class CustomerEdit
    {
        private CustomerDto? Customer;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private ISnackbar Snackbar { get; set; } = null!;
        [Parameter] public int Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            var responseHttp = await Repository.GetAsync<ResponseDto<CustomerDto>>($"api/Customer/Get/{Id}");

            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("customers");
                }
                else
                {
                    var messageError = await responseHttp.GetErrorMessageAsync();
                    Snackbar.Add(messageError!, Severity.Error);
                }
            }
            else
            {
                Customer = responseHttp.Response!.Data;
            }
        }

        private async Task EditAsync()
        {
            var responseHttp = await Repository.PutAsync("api/Customer/Update", Customer);

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
            NavigationManager.NavigateTo("customers");
        }
    }
}