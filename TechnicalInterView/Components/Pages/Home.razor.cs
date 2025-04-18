using System;
using Microsoft.AspNetCore.Components;
using TechnicalInterView.Tenants.Models;
using VideoAnalytics.Services;

namespace TechnicalInterView.Components.Pages;

public partial class Home
{
    
    // Add logic to load tenant IDs from CameraDataService and pass selected TenantId to CameraGrid

    public  List<Tenant> TenantList = new();

    public string? CurrentTenantId;

#region Tenants
    protected async Task<bool> GetTenantsAsync(){
        var result = true;
        try
        {
            var tenantIds = await CameraDataService.GetTenantIdsAsync();
            foreach (var tenantId in tenantIds)
            {
                var tenant = new Tenant(tenantId);
                TenantList.Add(tenant);
            }
        }
        catch (Exception ex)
        {
            await Logger.LogErrorAsync("GetTenantsAsyncFail",string.Empty, ex);
            result = false;
        }
        return result;
    }
#endregion Tenats
#region BlazorLifecycleMethods
    protected override async Task OnInitializedAsync()
    {
        // This method is called when the component is initialized.
        // You can perform any necessary setup here, such as loading data or initializing services.
        await GetTenantsAsync();
        await InvokeAsync(() => StateHasChanged());
    }
#endregion BlazorLifecycleMethods


    private async Task OnTenantSelected(string tenantId)
    {
        CurrentTenantId = tenantId;
        await Logger.LogAsync($"Tenant selected: {tenantId}", tenantId);        
    }



}
