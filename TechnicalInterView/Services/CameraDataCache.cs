using System;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using TechnicalInterView.Cameras.Models;
using TechnicalInterView.VideoAnalytics.Models;
using VideoAnalytics.Services;

namespace TechnicalInterView.Services;

public class CameraDataCache : ICameraDataCache
{
    private readonly ICameraDataService _cameraDataService;
    private  Dictionary<string, CameraConfig>? _cameraCache ;
    
    public CameraDataCache(ICameraDataService cameraDataService)
    {
        _cameraDataService = cameraDataService;
    }
    public async Task<CameraModel> GetCameraById(string cameraId)
    {
        if (_cameraCache == null)
        {
            _cameraCache = new Dictionary<string, CameraConfig>();
            var cameras = await GetCamerasAsync();
            foreach (var camera in cameras)
            {
                _cameraCache[camera.CameraId] = camera;
            }
        }
        _cameraCache.TryGetValue(cameraId, out var cameraConfig);
        var cameraModel = new CameraModel();
        if (cameraConfig is not null)
        {
            cameraModel.CameraId = cameraConfig.CameraId;
            cameraModel.Name = cameraConfig.Name;
            cameraModel.Status = cameraConfig.Status;
            cameraModel.UseCase = cameraConfig.UseCase;
            cameraModel.TenantId = cameraConfig.TenantId;
        }
        return cameraModel;
    }
    public async Task<List<CameraConfig>> GetCamerasAsync()
    {
        if ( _cameraCache is null)
        {
            _cameraCache = new Dictionary<string, CameraConfig>();
        }
        var cameras =  await _cameraDataService.GetCamerasAsync();
        foreach ( var camera in cameras)
        {
            _cameraCache[camera.CameraId] = camera;
        }
        return cameras;
    }

    public async Task<List<string>> GetTenantIdsAsync()
    {
        return await _cameraDataService.GetTenantIdsAsync();
    }

    public async Task UpdateCameraStatusAsync(string cameraId, string status)
    {

        await _cameraDataService.UpdateCameraStatusAsync(cameraId, status);
    }

    public async Task UpdateUseCaseAsync(string tenantId, string cameraId, string useCase)
    {
        await _cameraDataService.UpdateUseCaseAsync(tenantId, cameraId, useCase);
    }
}
