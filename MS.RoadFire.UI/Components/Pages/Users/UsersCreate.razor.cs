using Microsoft.AspNetCore.Components;
using MS.RoadFire.Business.Models;
using MS.RoadFire.Common.Helpers;
using MS.RoadFire.UI.Repositories;
using MudBlazor;

namespace MS.RoadFire.UI.Components.Pages.Users
{
    public partial class UsersCreate
    {
        private UserDto NewUser = new()
        {
            State = true
        };

        private List<EmployeeDto> Employees = new();
        private List<RoleDto> Roles = new();

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private ISnackbar Snackbar { get; set; } = null!;

        protected override async Task OnInitializedAsync()
        {
            var empResp = await Repository.GetAsync<ResponseDto<List<EmployeeDto>>>("api/Employees/GetAll");
            var roleResp = await Repository.GetAsync<ResponseDto<List<RoleDto>>>("api/Roles/GetAll");

            if (empResp.Error)
            {
                Snackbar.Add(await empResp.GetErrorMessageAsync() ?? "Error cargando empleados", Severity.Error);
            }
            else
            {
                Employees = empResp.Response?.Data ?? new();
                if (Employees.Count == 0)
                    Snackbar.Add("No hay empleados disponibles para asignar.", Severity.Info);
            }

            if (roleResp.Error)
            {
                Snackbar.Add(await roleResp.GetErrorMessageAsync() ?? "Error cargando roles", Severity.Error);
            }
            else
            {
                Roles = roleResp.Response?.Data ?? new();
                if (Roles.Count == 0)
                    Snackbar.Add("No hay roles configurados.", Severity.Info);
            }
        }

        private async Task CreateAsync()
        {
            if (string.IsNullOrWhiteSpace(NewUser.Username))
            {
                Snackbar.Add("Ingrese el usuario.", Severity.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(NewUser.Password))
            {
                Snackbar.Add("Ingrese la contraseña.", Severity.Warning);
                return;
            }

            if (NewUser.EmployeeId <= 0)
            {
                Snackbar.Add("Seleccione un empleado.", Severity.Warning);
                return;
            }

            if (NewUser.RoleId <= 0)
            {
                Snackbar.Add("Seleccione un rol.", Severity.Warning);
                return;
            }

            var nuevoUsuario = new UserDto
            {
                Username = NewUser.Username,
                Password = NewUser.Password,
                EmployeeId = NewUser.EmployeeId,
                RoleId = NewUser.RoleId,
                State = NewUser.State
            };

            var responseHttp = await Repository.PostAsync("api/Users/Add", nuevoUsuario);

            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                Snackbar.Add(message ?? "No se pudo crear el usuario.", Severity.Error);
                Console.WriteLine($"Mensaje backend: {message}");
                return;
            }
            Return();
            Snackbar.Add("Usuario creado correctamente", Severity.Success);
        }

        private void Return()
        {
            NavigationManager.NavigateTo("/GestionUsuarios");
        }
    }
}