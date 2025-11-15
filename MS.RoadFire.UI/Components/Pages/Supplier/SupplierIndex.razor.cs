using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MS.RoadFire.Business.Models;
using MS.RoadFire.UI.Components.Shared;
using MS.RoadFire.UI.Models;
using MS.RoadFire.UI.Repositories;
using MudBlazor;
using System.Net;

namespace MS.RoadFire.UI.Components.Pages.Supplier
{
    public partial class SupplierIndex
    {
        private List<SupplierDto>? Suppliers { get; set; }
        private MudTable<SupplierDto> table = new();
        private readonly int[] pageSizeOptions = { 10, 20, 50, int.MaxValue };
        private int totalRecords = 0;
        private bool loading;
        private const string baseUrl = "api/Supplier";
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

        private async Task<TableData<SupplierDto>> LoadListAsync(TableState state, CancellationToken cancellationToken)
        {
            int page = state.Page + 1;
            int pageSize = state.PageSize;
            var url = $"{baseUrl}/Get/Paginated/?Page={page}&RecordsNumber={pageSize}";

            if (!string.IsNullOrWhiteSpace(Filter))
            {
                url += $"&filter={Filter}";
            }

            var responseHttp = await Repository.GetAsync<ResponseDto<List<SupplierDto>>>(url);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                Snackbar.Add(message!, Severity.Error);
                return new TableData<SupplierDto> { Items = [], TotalItems = 0 };
            }

            if (responseHttp.Response == null)
            {
                return new TableData<SupplierDto> { Items = [], TotalItems = 0 };
            }

            return new TableData<SupplierDto>
            {
                Items = responseHttp.Response.Data,
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
                dialog = await DialogService.ShowAsync<SupplierEdit>("Editar", parameters, options);
            }
            else
            {
                dialog = await DialogService.ShowAsync<SupplierCreate>("Nuevo", options);
            }

            var result = await dialog.Result;

            if (result!.Canceled!)
            {
                await LoadTotalRecordsAsync();
                await table.ReloadServerData();
            }
        }

        private async Task DeleteAsync(SupplierDto supplier)
        {
            var parameters = new DialogParameters
                {
                    { "Message", $"Estas seguro de borrar la categoría: {supplier.Name}"}
                };

            var options = new DialogOptions
            {
                CloseButton = true,
                MaxWidth = MaxWidth.ExtraSmall,
                CloseOnEscapeKey = true
            };

            var dialog = await DialogService.ShowAsync<ConfirmDialog>("Confirmación", parameters, options);
            var result = await dialog.Result;

            if (result!.Canceled)
            {
                return;
            }

            var responseHttp = await Repository.DeleteAsync<ResponseDto<bool>>($"{baseUrl}/Delete/{supplier.Id}");

            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("/suppliers");
                }
                else
                {
                    var message = await responseHttp.GetErrorMessageAsync();
                    Snackbar.Add(message!, Severity.Error);
                }
                return;
            }

            await LoadTotalRecordsAsync();
            await table.ReloadServerData();
            Snackbar.Add("Registro eliminado", Severity.Success);
        }

        private async Task ReturnAction()
        {
            var rol = await localStorage.GetAsync<string>("rol");

            string path = rol.Value switch
            {
                "Administrador" => "/adminProfile",
                "Ventas" => "/salesProfile",
                "Compras" => "/purchasesProfile",
                "Inventario" => "/inventoryProfile",
                _ => "/Home"
            };

            NavigationManager.NavigateTo(path);
        }
    }
}