using Microsoft.AspNetCore.Components;
using MS.RoadFire.Business.Models;
using MS.RoadFire.UI.Repositories;
using MudBlazor;

namespace MS.RoadFire.UI.Components.Pages.Employees
{
    public partial class EmployeesCreate
    {
        private EmployeeDto CurrentEmployee = new();
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private ISnackbar Snackbar { get; set; } = null!;

        private async Task CreateAsync()
        {
            var responseHttp = await Repository.PostAsync("/api/Employees/Add", CurrentEmployee);

            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                Snackbar.Add(message!, Severity.Error);
                return;
            }

            Return();
            Snackbar.Add("Empleado creado correctamente", Severity.Success);
        }

        private void Return()
        {
            NavigationManager.NavigateTo("/employees");
        }
    }
}