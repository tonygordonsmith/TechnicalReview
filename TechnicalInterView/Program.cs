using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.SignalR.Client;
using TechnicalInterView.Cameras.Models;
using TechnicalInterView.Components;
using TechnicalInterView.Services;
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
//
// Declare a Scoped Service for Data Cache
//
builder.Services.AddScoped<ICameraDataCache, CameraDataCache>();
//
// Rather than use cascading parameters, we can use a filter object instance
// scoped per user connection to the Asp.Net server
builder.Services.AddScoped<CameraQueryFilter>();

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