using System;

namespace TechnicalInterView.Cameras.Models;
/// <summary>
/// This class is used to filter cameras based on their properties.
/// It inherits from the CameraModel class, which contains properties common to all cameras.
/// A null property means that no filter is in place for that property
/// A non-null property means that the filter is in place for that property
/// </summary>
public class CameraQueryFilter : CameraModel
{
    public bool PassFilter(CameraModel camera)
    {
        if (CameraId != null && camera.CameraId != CameraId)
            return false;
            
        if (TenantId != null && camera.TenantId != TenantId)
            return false;
            
        if (Name != null && camera.Name != Name)
            return false;
            
        if (UseCase != null && camera.UseCase != UseCase)
            return false;
            
        if (Status != null && camera.Status != Status)
            return false;
            
        return true;
    }
}
