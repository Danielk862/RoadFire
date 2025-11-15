using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace MS.RoadFire.UI.Components.Pages
{
    public partial class SalesProfile
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

        private void GoToSales()
        {
            NavigatorManager.NavigateTo("../sales");
        }

        private void GoToStocks()
        {
            NavigatorManager.NavigateTo("../stocks");
        }

        private void GoToMovements()
        {
            NavigatorManager.NavigateTo("../movements");
        }

        private string currentDate = DateTime.Now.ToString("dd/MM/yyyy");
    }
}