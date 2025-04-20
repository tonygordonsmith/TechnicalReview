using System;
using System.Threading.Tasks;

namespace TechnicalInterView.Cameras.Models;
/// <summary>
/// This class is used to filter cameras based on their properties.
/// It inherits from the CameraModel class, which contains properties common to all cameras.
/// A null property means that no filter is in place for that property
/// A non-null property means that the filter is in place for that property
/// </summary>
public class CameraQueryFilter : CameraModel
{
    public event EventHandler<EventArgs>? FilterChanged;
    public bool PassFilter(CameraModel camera)
    {
        if (CameraId != null && camera.CameraId != CameraId)
            return false;
            
        if (TenantId != null && camera.TenantId != TenantId)
            return false;
            
        if (Name != null)
            if (camera.Name is null)
                return false;
            else
                if (!camera.Name.Contains(Name))
                    return false;
            
        if (UseCase != null && UseCase != string.Empty && camera.UseCase != UseCase)
            return false;
            
        if (Status != null && Status != string.Empty && camera.Status != Status)
            return false;
            
        return true;
    }
    public  void OnFilterChange()
    {
        var args = new EventArgs();
        FilterChanged?.Invoke(this,args);
    }
}
