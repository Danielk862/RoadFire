using Microsoft.AspNetCore.Components;
using MS.RoadFire.Business.Models;

namespace MS.RoadFire.UI.Components.Pages.Information
{
    public partial class InformationTransactionDetails
    {
        [Parameter]
        public List<TransactionDetailDto> Transaction { get; set; } = null!;

        [Parameter]
        public Action? OnClose { get; set; }

        void Close()
        {
            OnClose?.Invoke();
        }
    }
}