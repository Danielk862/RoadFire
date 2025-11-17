using Microsoft.AspNetCore.Components;
using MS.RoadFire.Business.Models;

namespace MS.RoadFire.UI.Components.Pages.Information
{
    public partial class InformationSalesDetails
    {
        [Parameter]
        public List<SaleDetailsDto> Sale { get; set; } = null!;

        [Parameter]
        public Action? OnClose { get; set; }

        void Close()
        {
            OnClose?.Invoke();
        }
    }
}