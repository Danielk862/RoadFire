using Microsoft.AspNetCore.Components;
using MS.RoadFire.Common.Resource;

namespace MS.RoadFire.UI.Components.Shared
{
    public partial class Loading
    {
        [Parameter] public string? Label { get; set; }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (string.IsNullOrEmpty(Label))
            {
                Label = MessagesResource.Loading;
            }
        }
    }
}