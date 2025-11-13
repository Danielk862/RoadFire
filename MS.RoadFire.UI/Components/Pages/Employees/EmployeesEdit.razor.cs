using Microsoft.AspNetCore.Components;
using MS.RoadFire.Business.Models;
using MS.RoadFire.Common.Helpers;
using MS.RoadFire.UI.Repositories;
using MudBlazor;
using System.Net;

namespace MS.RoadFire.UI.Components.Pages.Employees;

public partial class EmployeesEdit
{
    private EmployeeDto? CurrentEmployee;

    [Inject] private IRepository Repository { get; set; } = null!;
    [Inject] private NavigationManager NavigationManager { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    [Parameter] public int Id { get; set; }

    protected override async Task OnInitializedAsync()
    {
        await LoadRoleAsync();
    }

    private async Task LoadRoleAsync()
    {
        var responseHttp = await Repository.GetAsync<ResponseDto<EmployeeDto>>($"api/Employees/Get/{Id}");
        if (responseHttp.Error)
        {
            if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
            {
                Snackbar.Add("El empleado no fue encontrado.", Severity.Warning);
                NavigationManager.NavigateTo("/employees");
            }
            else
            {
                var messageError = await responseHttp.GetErrorMessageAsync();
                Snackbar.Add(messageError!, Severity.Error);
            }
            return;
        }
        CurrentEmployee = responseHttp.Response!.Data;
    }

    private async Task EditAsync()
    {
        var responseHttp = await Repository.PutAsync("api/Employees/Update", CurrentEmployee);

        if (responseHttp.Error)
        {
            var messageError = await responseHttp.GetErrorMessageAsync();
            Snackbar.Add(messageError!, Severity.Error);
            return;
        }

        Return();
        Snackbar.Add("Empleado actualizado correctamente ?", Severity.Success);
    }

    private void Return()
    {
        NavigationManager.NavigateTo("/employees");
    }
}