using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MS.RoadFire.Business.Models;
using MS.RoadFire.UI.Components.Shared;
using MS.RoadFire.UI.Models;
using MS.RoadFire.UI.Repositories;
using MudBlazor;
using System.Net;

namespace MS.RoadFire.UI.Components.Pages.Users
{
    public partial class UsersIndex
    {
        private List<UserDto>? Users { get; set; }
        private List<EmployeeDto> Employees { get; set; } = new();
        private List<RoleDto> Roles { get; set; } = new();

        private MudTable<UserDto> table = new();
        private readonly int[] pageSizeOptions = { 10, 20, 50, int.MaxValue };
        private int totalRecords = 0;
        private bool loading;
        private const string baseUrl = "api/Users";
        private string infoFormat = "Registro {first_item} de {last_item} Total {all_items}";

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private IDialogService DialogService { get; set; } = null!;
        [Inject] private ISnackbar Snackbar { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private ProtectedLocalStorage localStorage { get; set; } = null!;

        [Parameter, SupplyParameterFromQuery] public string Filter { get; set; } = string.Empty;

        protected override async Task OnInitializedAsync()
        {
            await LoadEmployeesAndRolesAsync();
            await LoadTotalRecordsAsync();
        }

        private async Task LoadEmployeesAndRolesAsync()
        {
            var empResp = await Repository.GetAsync<ResponseDto<List<EmployeeDto>>>("api/Employees/GetAll");
            if (!empResp.Error && empResp.Response?.Data is not null)
                Employees = empResp.Response.Data;

            var roleResp = await Repository.GetAsync<ResponseDto<List<RoleDto>>>("api/Roles/GetAll");
            if (!roleResp.Error && roleResp.Response?.Data is not null)
                Roles = roleResp.Response.Data;
        }

        private async Task LoadTotalRecordsAsync()
        {
            loading = true;
            var url = $"{baseUrl}/GetTotalRecords/totalRecords";

            if (!string.IsNullOrWhiteSpace(Filter))
            {
                url += $"?filter={Filter}";
            }

            var responseHttp = await Repository.GetAsync<ResponseDto<int>>(url);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                Snackbar.Add(message!, Severity.Error);
                return;
            }

            totalRecords = responseHttp.Response!.Data;
            loading = false;
        }

        private async Task<TableData<UserDto>> LoadListAsync(TableState state, CancellationToken cancellationToken)
        {
            int page = state.Page + 1;
            int pageSize = state.PageSize;
            var url = $"{baseUrl}/Get/Paginated/?Page={page}&RecordsNumber={pageSize}";

            if (!string.IsNullOrWhiteSpace(Filter))
            {
                url += $"&filter={Filter}";
            }

            var responseHttp = await Repository.GetAsync<ResponseDto<List<UserDto>>>(url);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                Snackbar.Add(message!, Severity.Error);
                return new TableData<UserDto> { Items = [], TotalItems = 0 };
            }

            //Para cargar/llenar los nombres de empleado y rol
            var users = responseHttp.Response?.Data ?? new List<UserDto>();

            if (Employees == null || Employees.Count == 0 || Roles == null || Roles.Count == 0)
            {
                await LoadEmployeesAndRolesAsync();
            }

            foreach (var user in users)
            {
                var employee = Employees?.FirstOrDefault(e => e.Id == user.EmployeeId);
                if (employee != null)
                    user.EmployeeName = $"{employee.FirtsName} {employee.Surname}";

                var role = Roles?.FirstOrDefault(r => r.Id == user.RoleId);
                if (role != null)
                    user.RoleName = role.Name;
            }

            return new TableData<UserDto>
            {
                Items = users,
                TotalItems = totalRecords,
            };
        }

        private async Task SetFilterValue(string value)
        {
            Filter = value;
            await LoadTotalRecordsAsync();
            await table.ReloadServerData();
        }

        private async Task ShowModalAsync(int id = 0, bool isEdit = false)
        {
            var options = new DialogOptions
            {
                CloseOnEscapeKey = true,
                CloseButton = true
            };

            IDialogReference? dialog;

            if (isEdit)
            {
                var parameters = new DialogParameters
                {
                    { "Id", id }
                };
                dialog = await DialogService.ShowAsync<UsersEdit>("Editar Usuario", parameters, options);
            }
            else
            {
                dialog = await DialogService.ShowAsync<UsersCreate>("Nuevo Usuario", options);
            }

            var result = await dialog.Result;

            if (result!.Canceled!)
            {
                await LoadEmployeesAndRolesAsync();
                await LoadTotalRecordsAsync();
                await table.ReloadServerData();
            }
        }

        private async Task DeleteAsync(UserDto user)
        {
            var parameters = new DialogParameters
            {
                { "Message", $"¿Estás seguro de borrar el usuario: {user.Username}?" }
            };

            var options = new DialogOptions
            {
                CloseButton = true,
                MaxWidth = MaxWidth.ExtraSmall,
                CloseOnEscapeKey = true
            };

            var dialog = await DialogService.ShowAsync<ConfirmDialog>("Confirmación", parameters, options);
            var result = await dialog.Result;

            if (result == null || result.Canceled)
                return;

            var responseHttp = await Repository.DeleteAsync<ResponseDto<bool>>($"{baseUrl}/Delete/{user.Id}");

            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    Snackbar.Add("El usuario no fue encontrado o ya fue eliminado.", Severity.Warning);
                    NavigationManager.NavigateTo("/GestionUsuarios", forceLoad: true);
                }
                else
                {
                    var message = await responseHttp.GetErrorMessageAsync();
                    Snackbar.Add(message!, Severity.Error);
                }
                return;
            }

            Snackbar.Add("Usuario eliminado correctamente", Severity.Success);
            await LoadTotalRecordsAsync();
            await table.ReloadServerData();
        }

        private async Task ReturnAction()
        {
            var rol = await localStorage.GetAsync<string>("rol");

            string path = rol.Value switch
            {
                "Administrador" => "/Admin",
                "Ventas" => "/Ventas",
                "Compras" => "/Compras",
                "Inventario" => "/Inventario",
                _ => "/Home"
            };

            NavigationManager.NavigateTo(path);
        }
    }
}