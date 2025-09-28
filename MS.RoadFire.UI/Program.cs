using MudBlazor.Services;
using MS.RoadFire.UI.Components;
using MS.RoadFire.CrossCutting.LocRegister;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRegister();
builder.Services.AddMudServices();
builder.Services.AddServerSideBlazor().AddCircuitOptions(opt => { opt.DetailedErrors = true; });

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAntiforgery();
app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();