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

                if (user != null && user.RoleName.Equals("Administrador"))
                {
                    await localStorage!.SetAsync("user", user.EmployeeName);
                    Navigation.NavigateTo("/Admin");
                }
                //else if (user != null && user.RoleName.Equals("Ventas"))
                //{
                //    await localStorage!.SetAsync("user", user.EmployeeName);
                //    Navigation.NavigateTo("/MenuRolVentas");
                //}
                //else if (user != null && user.RoleName.Equals("Compras"))
                //{
                //    await localStorage!.SetAsync("user", user.EmployeeName);
                //    Navigation.NavigateTo("/MenuAdmin");
                //}
                //else if (user != null && user.RoleName.Equals("Inventario"))
                //{
                //    await localStorage!.SetAsync("user", user.EmployeeName);
                //    Navigation.NavigateTo("/MenuInventario");
                //}
                //else
                //{
                //    Snackbar.Add("Usuario no encontrado.", Severity.Warning);
                //}
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