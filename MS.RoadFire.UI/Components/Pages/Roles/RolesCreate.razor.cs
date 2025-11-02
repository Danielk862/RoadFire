using Microsoft.AspNetCore.Components;
using MS.RoadFire.Business.Models;
using MS.RoadFire.UI.Repositories;
using MudBlazor;

namespace MS.RoadFire.UI.Components.Pages.Roles
{
    public partial class RolesCreate
    {
        // Debe llamarse igual que el parámetro que pasamos al formulario
        private RoleDto CurrentRole = new();

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private ISnackbar Snackbar { get; set; } = null!;

        private async Task CreateAsync()
        {
            var responseHttp = await Repository.PostAsync("/api/Roles/Add", CurrentRole);

            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                Snackbar.Add(message!, Severity.Error);
                return;
            }

            Snackbar.Add("Rol creado correctamente", Severity.Success);
            Return();
        }

        private void Return()
        {
            NavigationManager.NavigateTo("/gestionRoles");
        }
    }
}