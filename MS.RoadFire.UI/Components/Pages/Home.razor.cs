using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MS.RoadFire.Business.Models;
using MS.RoadFire.UI.Models;
using MS.RoadFire.UI.Repositories;
using MudBlazor;
using Unity;

namespace MS.RoadFire.UI.Components.Pages
{
    public partial class Home
    {
        private LoginModel loginModel = new();
        private string? loginError;

        [Inject] private ProtectedLocalStorage? localStorage { get; set; } = default!;

        [Inject] private NavigationManager Navigation { get; set; } = default!;
        [Inject] private IRepository repository { get; set; } = default!;
        [Inject] private ISnackbar Snackbar { get; set; } = null!;


        private async Task Login()
        {
            var url = $"api/Security/Login?username={loginModel.Username}&password={loginModel.Password}";

            var response = await repository.PostAsync<ResponseDto<UserDto>>(url, null!);

            if (!response.Error && response.Response!.Data != null && response.Response.Code.ToString() == "200")
            {
                var user = response.Response.Data;
                await localStorage!.SetAsync("rol", user.RoleName);
                await localStorage.SetAsync("idUser", user.Id);
                await localStorage!.SetAsync("user", user.EmployeeName);

                if (user != null && user.RoleName.Equals("Administrador"))                
                    Navigation.NavigateTo("/adminProfile");
                
                if (user != null && user.RoleName.Equals("Ventas"))                
                    Navigation.NavigateTo("/salesProfile");
                
                if (user != null && user.RoleName.Equals("Compras"))                
                    Navigation.NavigateTo("/purchasesProfile");
                
                if (user != null && user.RoleName.Equals("Inventario"))                
                    Navigation.NavigateTo("/inventoryProfile");                
            }
            else
            {
                Snackbar.Add("Usuario y/o contraseña incorrectos.", Severity.Warning);
            }
        }

        public class LoginModel
        {
            public string Username { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }
    }
}