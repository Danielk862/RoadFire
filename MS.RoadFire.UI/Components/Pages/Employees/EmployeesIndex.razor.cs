using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MS.RoadFire.Business.Models;
using MS.RoadFire.Common.Helpers;
using MS.RoadFire.UI.Components.Shared;
using MS.RoadFire.UI.Repositories;
using MudBlazor;
using System.Net;

namespace MS.RoadFire.UI.Components.Pages.Employees;

public partial class EmployeesIndex
{    private List<EmployeeDto>? Employees { get; set; }
    private MudTable<EmployeeDto> table = new();
    private readonly int[] pageSizeOptions = { 10, 20, 50, int.MaxValue };
    private int totalRecords = 0;
    private bool loading;
    private const string baseUrl = "api/Employees";
    private string infoFormat = "Registro {first_item} de {last_item} Total {all_items}";

    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private IDialogService DialogService { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private ProtectedLocalStorage localStorage { get; set; } = null!;

    [Parameter, SupplyParameterFromQuery] public string Filter { get; set; } = string.Empty;

    protected override async Task OnInitializedAsync()
    {
        await LoadTotalRecordsAsync();
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

    private async Task<TableData<EmployeeDto>> LoadListAsync(TableState state, CancellationToken cancellationToken)
    {
        int page = state.Page + 1;
        int pageSize = state.PageSize;
        var url = $"{baseUrl}/Get/Paginated/?Page={page}&RecordsNumber={pageSize}";

        if (!string.IsNullOrWhiteSpace(Filter))
        {
            url += $"&filter={Filter}";
        }

        var responseHttp = await Repository.GetAsync<ResponseDto<List<EmployeeDto>>>(url);
        if (responseHttp.Error)
        {
            var message = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(message!, Severity.Error);
            return new TableData<EmployeeDto> { Items = [], TotalItems = 0 };
        }

        return new TableData<EmployeeDto>
        {
            Items = responseHttp.Response!.Data,
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
            dialog = await DialogService.ShowAsync<EmployeesEdit>("Editar Empleado", parameters, options);
        }
        else
        {
            dialog = await DialogService.ShowAsync<EmployeesCreate>("Nuevo Empleado", options);
        }

        var result = await dialog.Result;

        if (result != null && !result.Canceled)
        {
            await LoadTotalRecordsAsync();
            await table.ReloadServerData();
        }
    }

    private async Task DeleteAsync(EmployeeDto employee)
    {
        var parameters = new DialogParameters
            {
                { "Message", $"¿Estás seguro de eliminar al empleado: {employee.FirtsName} {employee.Surname}?" }
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

        var responseHttp = await Repository.DeleteAsync<ResponseDto<bool>>($"{baseUrl}/Delete/{employee.Id}");

        if (responseHttp.Error)
        {
            if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                Snackbar.Add("El empleado no fue encontrado o ya fue eliminado.", Severity.Warning);
                NavigationManager.NavigateTo("/GestionEmpleados", forceLoad: true);
            }
            else
            {
                var message = await responseHttp.GetErrorMessageAsync();
                Snackbar.Add(message!, Severity.Error);
            }
            return;
        }

        Snackbar.Add("Empleado eliminado correctamente ✅", Severity.Success);
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
