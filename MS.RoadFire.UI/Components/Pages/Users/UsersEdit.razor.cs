using Microsoft.AspNetCore.Components;
using MS.RoadFire.Business.Models;
using MS.RoadFire.Common.Helpers;
using MS.RoadFire.UI.Repositories;
using MudBlazor;
using System.Net;

namespace MS.RoadFire.UI.Components.Pages.Users
{
    public partial class UsersEdit
    {
        private UserDto? UserToEdit;
        private List<EmployeeDto> Employees = new();
        private List<RoleDto> Roles = new();

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private ISnackbar Snackbar { get; set; } = null!;

        [Parameter] public int Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadUserAsync();
            await LoadLookupsAsync();
        }

        private async Task LoadUserAsync()
        {
            var responseHttp = await Repository.GetAsync<ResponseDto<UserDto>>($"api/Users/Get/{Id}");

            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    Snackbar.Add("El usuario no fue encontrado.", Severity.Warning);
                    NavigationManager.NavigateTo("/GestionUsuarios");
                }
                else
                {
                    var messageError = await responseHttp.GetErrorMessageAsync();
                    Snackbar.Add(messageError!, Severity.Error);
                }
                return;
            }

            UserToEdit = responseHttp.Response!.Data;
        }

        private async Task LoadLookupsAsync()
        {
            var empResp = await Repository.GetAsync<ResponseDto<List<EmployeeDto>>>("api/Employees/GetAll");

            if (!empResp.Error && empResp.Response?.Data is not null)
                Employees = empResp.Response.Data;

            var roleResp = await Repository.GetAsync<ResponseDto<List<RoleDto>>>("api/Roles/GetAll");

            if (!roleResp.Error && roleResp.Response?.Data is not null)
            {
                var response = roleResp.Response?.Data;
                var resultRoles = response!.FindAll(x => x.IsActive);
                Roles = resultRoles;
            }
        }

        private async Task EditAsync()
        {
            if (UserToEdit is null)
            {
                Snackbar.Add("No hay datos para actualizar.", Severity.Error);
                return;
            }

            var emp = Employees.FirstOrDefault(e => e.Id == UserToEdit.EmployeeId);
            if (emp != null) UserToEdit.EmployeeName = $"{emp.FirtsName} {emp.Surname}";
            var rol = Roles.FirstOrDefault(r => r.Id == UserToEdit.RoleId);
            if (rol != null) UserToEdit.RoleName = rol.Name;

            var responseHttp = await Repository.PutAsync("api/Users/Update", UserToEdit);

            if (responseHttp.Error)
            {
                var messageError = await responseHttp.GetErrorMessageAsync();
                Snackbar.Add(messageError!, Severity.Error);
                return;
            }

            Return();
            Snackbar.Add("Usuario actualizado correctamente", Severity.Success);
        }

        private void Return()
        {
            NavigationManager.NavigateTo("/GestionUsuarios");
        }
    }
}