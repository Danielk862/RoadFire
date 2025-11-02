using Microsoft.AspNetCore.Components;
using MS.RoadFire.Business.Models;
using MS.RoadFire.Common.Helpers;
using MS.RoadFire.UI.Repositories;
using MudBlazor;
using System.Net;

namespace MS.RoadFire.UI.Components.Pages.Roles
{
    public partial class RolesEdit
    {
        private RoleDto? CurrentRole;

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private ISnackbar Snackbar { get; set; } = null!;

        [Parameter] public int Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadRoleAsync();
        }

        private async Task LoadRoleAsync()
        {
            var responseHttp = await Repository.GetAsync<ResponseDto<RoleDto>>($"api/Roles/Get/{Id}");

            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    Snackbar.Add("El rol no fue encontrado.", Severity.Warning);
                    NavigationManager.NavigateTo("/gestionRoles");
                }
                else
                {
                    var messageError = await responseHttp.GetErrorMessageAsync();
                    Snackbar.Add(messageError!, Severity.Error);
                }
                return;
            }

            CurrentRole = responseHttp.Response!.Data;
        }

        private async Task EditAsync()
        {
            var responseHttp = await Repository.PutAsync("api/Roles/Update", CurrentRole);

            if (responseHttp.Error)
            {
                var messageError = await responseHttp.GetErrorMessageAsync();
                Snackbar.Add(messageError!, Severity.Error);
                return;
            }

            Snackbar.Add("Rol actualizado correctamente ?", Severity.Success);
            Return();
        }

        private void Return()
        {
            NavigationManager.NavigateTo("/GestionRoles");
        }
    }
}