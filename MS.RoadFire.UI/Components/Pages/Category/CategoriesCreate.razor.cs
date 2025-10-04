using Microsoft.AspNetCore.Components;
using MS.RoadFire.UI.Repositories;
using MudBlazor;
using MS.RoadFire.Business.Models;

namespace MS.RoadFire.UI.Components.Pages.Category
{
    public partial class CategoriesCreate
    {
        private CategoryDto Category = new(); 
        [Inject] private IRepository Repository { get; set; } = null!; 
        [Inject] private NavigationManager NavigationManager { get; set; } = null!; 
        [Inject] private ISnackbar Snackbar { get; set; } = null!;

        private async Task CreateAsync()
        {
            var responseHttp = await Repository.PostAsync("/api/Category/Add", Category); 

            if (responseHttp.Error) 
            { 
                var message = await responseHttp.GetErrorMessageAsync(); 
                Snackbar.Add(message!, Severity.Error); 
                return; 
            }

            Return(); Snackbar.Add("Registro creado", Severity.Success);
        }

        private void Return()
        {
            NavigationManager.NavigateTo("/categories");
        }
    }
}