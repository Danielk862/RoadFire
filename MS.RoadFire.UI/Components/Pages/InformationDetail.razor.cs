using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace MS.RoadFire.UI.Components.Pages
{
    public partial class InformationDetail
    {
        private string? user;
        [Inject] private NavigationManager NavigatorManager { get; set; } = null!;
        [Inject] private ProtectedLocalStorage localStorage { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            var result = await localStorage.GetAsync<string>("user");

            if (result.Success)
            {
                user = result.Value!;
            }
        }

        private void Logout()
        {
            NavigatorManager.NavigateTo("/");
        }

        private void GoToInformationPurchase()
        {
            NavigatorManager.NavigateTo("../informationPurchase");
        }

        private void GoToInformationSale()
        {
            NavigatorManager.NavigateTo("../informationSale");
        }

        private void GoToInformationTransaction()
        {
            NavigatorManager.NavigateTo("../informationTransaction");
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

            NavigatorManager.NavigateTo(path);
        }

        private string currentDate = DateTime.Now.ToString("dd/MM/yyyy");
    }
}