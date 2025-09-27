using MudBlazor.Services;
using MS.RoadFire.UI.Components;
using MS.RoadFire.CrossCutting.LocRegister;

var builder = WebApplication.CreateBuilder(args);

// Add MudBlazor services
builder.Services.AddRegister();
builder.Services.AddMudServices();
builder.Services.AddServerSideBlazor().AddCircuitOptions(opt => { opt.DetailedErrors = true; });

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();


app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
