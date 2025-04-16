using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using TechnicalInterView.Components;
using VideoAnalytics.Hubs;
using VideoAnalytics.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddServerSideBlazor();
builder.Services.AddSignalR();
builder.Services.AddScoped<ICameraDataService, MockCameraDataService>();
builder.Services.AddScoped<ICustomLogger, MockLogger>();
builder.Services.AddHostedService<MockStatusService>();
builder.Services.AddScoped<HubConnection>(sp =>
{
    var nav = sp.GetRequiredService<NavigationManager>();
    return new HubConnectionBuilder()
        .WithUrl(nav.ToAbsoluteUri("/statusHub"))
        .WithAutomaticReconnect()
        .Build();
});

builder.Services.AddRazorComponents().AddInteractiveServerComponents();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
//app.UseRouting();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


//app.MapBlazorHub();
app.MapHub<StatusHub>("/statusHub");

app.Run();