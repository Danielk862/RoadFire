using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MS.RoadFire.Business.Models;
using MS.RoadFire.UI.Models;
using MS.RoadFire.UI.Repositories;
using MudBlazor;

namespace MS.RoadFire.UI.Components.Pages.Information
{
    public partial class InformationSalesIndex
    {
        private List<SaleDto>? Sales { get; set; }
        private MudTable<SaleDto> table = new();
        private readonly int[] pageSizeOptions = { 10, 20, 50, int.MaxValue };
        private int totalRecords = 0;
        private bool loading;
        private const string baseUrl = "api/Sale";
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

        private async Task<TableData<SaleDto>> LoadListAsync(TableState state, CancellationToken cancellationToken)
        {
            int page = state.Page + 1;
            int pageSize = state.PageSize;
            var url = $"{baseUrl}/Get/Paginated/?Page={page}&RecordsNumber={pageSize}";

            if (!string.IsNullOrWhiteSpace(Filter))
            {
                url += $"&filter={Filter}";
            }

            var responseHttp = await Repository.GetAsync<ResponseDto<List<SaleDto>>>(url);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                Snackbar.Add(message!, Severity.Error);
                return new TableData<SaleDto> { Items = [], TotalItems = 0 };
            }

            if (responseHttp.Response == null)
            {
                return new TableData<SaleDto> { Items = [], TotalItems = 0 };
            }

            return new TableData<SaleDto>
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

        private async Task ShowDetailsPopup(SaleDto sale)
        {
            IDialogReference dialogRef = null!;

            var parameters = new DialogParameters
            {
                ["Sale"] = sale.SaleDetailsDtos,
                ["OnClose"] = (Action)(() => dialogRef?.Close())
            };

            var options = new DialogOptions
            {
                CloseOnEscapeKey = true,
                MaxWidth = MaxWidth.Medium,
                FullWidth = true
            };

            dialogRef = await DialogService.ShowAsync<InformationSalesDetails>(sale.Description, parameters, options);
        }

        private void ReturnAction()
        {

            NavigationManager.NavigateTo("/informationDetail");
        }
    }
}