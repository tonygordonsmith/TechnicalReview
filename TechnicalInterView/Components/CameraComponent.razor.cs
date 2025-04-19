using System;
using Microsoft.AspNetCore.Components;
using TechnicalInterView.Cameras.Models;
using TechnicalInterView.Services;

namespace TechnicalInterView.Components;

public partial class CameraComponent
{
    [Inject]
    public ComponentRegistryService RegistryService { get; set; } = null!;
    [Parameter]
    public CameraModel? CameraModel { get; set; } = new CameraModel();

    public bool asTableRow = true;

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
        InvokeAsync(StateHasChanged);
    }

    protected override async Task OnInitializedAsync()
    {
        await RegistryService.InitializeAsync();
    }

    public void Dispose()
    {
        // Unregister the callback when the component is disposed
        RegistryService.UnregisterCallback(CameraModel?.CameraId ?? string.Empty);
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


}
