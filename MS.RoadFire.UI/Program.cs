using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using MS.RoadFire.UI.Components;
using MS.RoadFire.UI.Repositories;
using MudBlazor;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped(_ => new HttpClient
{
    BaseAddress = new Uri("https://localhost:7214/") //  API
});

builder.Services.AddScoped<IRepository, Repository>();
//builder.Services.AddScoped<RolesRepository>();
builder.Services.AddScoped<CategoriesRepository>();
//builder.Services.AddScoped<UsuariosRepository>();
builder.Services.AddScoped<ProtectedLocalStorage>();
builder.Services.AddScoped<ProtectedSessionStorage>();
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
app.UseStaticFiles();
app.UseAntiforgery();
app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();