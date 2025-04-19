using System;
using TechnicalInterView.Cameras.Enum;
using TechnicalInterView.Cameras.Extensions;

namespace TechnicalInterView.Cameras.Models;

public class CameraModel
{
    public string? CameraId {get; set;}
    public string? TenantId { get; set; }
    public string? Name { get; set; }
    private string? _useCase;
    public string? UseCase { 
                get => _useCase; 
                set {
                    if (string.IsNullOrEmpty(value))
                    {
                        UseCaseEnum = null;
                        _useCase = value;
                        return;
                    }
                    try
                    {
                        _useCase = value;
                        UseCaseEnum = CameraUseCaseEnumExtensions.FromString(value);
                    }
                    catch (ArgumentException)
                    {
                        UseCaseEnum = null;
                        _useCase = value;
                    }
                } }
    public CameraUseCaseEnum? UseCaseEnum { get; set;}
    public string? Status { 
        get => _status;
         set {
            if (string.IsNullOrEmpty(value))
            {
                _status = value;
                return;
            }
            try
            {
                _status = value;
                CameraStatusEnum = CameraStatusEnumExtensions.FromString(value);
            }
            catch (ArgumentException)
            {
                CameraStatusEnum = null;
                _status = value;
            }

         }
          }
    private string? _status;
    public CameraStatusEnum? CameraStatusEnum { get; set; } = null;

}
