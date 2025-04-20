using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TechnicalInterView.Cameras.Models;
using TechnicalInterView.Services;

namespace TechnicalInterView.Components;

public partial class CameraComponent
{
    [Inject]
    public ComponentRegistryService RegistryService { get; set; } = null!;
    [Inject]
    public CameraQueryFilter? CameraFilter {get; set;} = null!;

    [Parameter]
    public CameraModel? CameraModel { get; set; } = new CameraModel();

    public bool asTableRow = true;
    public bool Display = false;



protected override void OnParametersSet()
{
    if ((CameraModel is not null)&&(CameraModel.CameraId is not null))
    {
        RegistryService.RegisterCallback(CameraModel.CameraId, HandleCameraUpdate);
    }
}
#region LifeCycle
    private void HandleCameraUpdate(CameraModel updatedCamera)
    {
        if (CameraModel is null || updatedCamera.CameraId != CameraModel.CameraId)
        {
            return;
        }
        // Update the CameraModel with the new status
        CameraModel.Status = updatedCamera.Status;
        SetDisplay();
        InvokeAsync(StateHasChanged);
    }

    protected override async Task OnInitializedAsync()
    {
        await RegistryService.InitializeAsync();
        if (CameraFilter is not null)
            CameraFilter.FilterChanged += OnFilterChange;
        SetDisplay();  
    }

    public void Dispose()
    {
        // Unregister the callback when the component is disposed
        RegistryService.UnregisterCallback(CameraModel?.CameraId ?? string.Empty);
        if (CameraFilter is not null)
        CameraFilter.FilterChanged -= OnFilterChange;
    }
#endregion LifeCycle
#region Css
public string GetStatusClass()
{
    if (CameraModel is null)
    {
        return "btn btn-secondary";
    }
    return CameraModel.CameraStatusEnum switch
    {
        Cameras.Enum.CameraStatusEnum.Online => "btn btn-success",
        Cameras.Enum.CameraStatusEnum.Offline => "btn btn-danger",
        _ => "btn btn-secondary"
    };
}
#endregion Css
private void SetDisplay()
{
        if (CameraModel is null)
        {
            Display = false;
            return;
        }
        if (CameraFilter is null)
        {
            Display = true;
            return;
        }
        Display = CameraFilter.PassFilter(CameraModel);
}
public void OnFilterChange(object? sender, EventArgs args)
{
    SetDisplay();
    InvokeAsync(StateHasChanged);
}

}
