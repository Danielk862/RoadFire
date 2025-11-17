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
            var users = await Repository.GetAsync<ResponseDto<List<UserDto>>>("api/Users/GetAll");

            if (empResp.Error)
            {
                Snackbar.Add(await empResp.GetErrorMessageAsync() ?? "Error cargando empleados", Severity.Error);
            }
            else
            {
                var response = empResp.Response?.Data;
                var resultEmployees = response!.FindAll(x => x.IsActive);
                var listEmployees = resultEmployees.Where(a => !users.Response!.Data!.Any(x => x.EmployeeId == a.Id)).ToList();
                Employees = listEmployees ?? new();

                if (Employees.Count == 0)
                    Snackbar.Add("No hay empleados disponibles para asignar.", Severity.Info);
            }

            if (roleResp.Error)
            {
                Snackbar.Add(await roleResp.GetErrorMessageAsync() ?? "Error cargando roles", Severity.Error);
            }
            else
            {
                var response = roleResp.Response?.Data;
                var resultRoles = response!.FindAll(x => x.IsActive);
                Roles = resultRoles ?? new();

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

            var request = new UserDto
            {
                Username = NewUser.Username,
                Password = NewUser.Password,
                EmployeeId = NewUser.EmployeeId,
                RoleId = NewUser.RoleId,
                State = NewUser.State
            };

            var users = await Repository.GetAsync<ResponseDto<List<UserDto>>>("api/Users/GetAll");

            var existUser = users.Response!.Data!.Where(x => x.Username == request.Username).FirstOrDefault();

            if (existUser != null)
            {
                Snackbar.Add("No se puede crear por que el usuario ya existe.", Severity.Error);
                return;
            }

            var responseHttp = await Repository.PostAsync("api/Users/Add", request);

            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                Snackbar.Add(message ?? "No se pudo crear el usuario.", Severity.Error);
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