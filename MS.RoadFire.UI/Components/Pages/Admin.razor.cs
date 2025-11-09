using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Unity;

namespace MS.RoadFire.UI.Components.Pages
{
    public partial class Admin
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

        private void GoToCategory()
        {
            NavigatorManager.NavigateTo("../categories");
        }

        private void GoToSupplier()
        {
            NavigatorManager.NavigateTo("../suppliers");
        }

        private void GoToInventories()
        {
            NavigatorManager.NavigateTo("../stocks");
        }

        private void GoToSales()
        {
            NavigatorManager.NavigateTo("../sales");
        }

        private void GoToUsers()
        {
            NavigatorManager.NavigateTo("../suppliers");
        }

        private string currentDate = DateTime.Now.ToString("dd/MM/yyyy");
    }
}