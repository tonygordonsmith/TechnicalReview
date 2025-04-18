using System;
using TechnicalInterView.Cameras.Models;
using TechnicalInterView.VideoAnalytics.Models;
using VideoAnalytics.Services;

namespace TechnicalInterView.Services;

public interface ICameraDataCache : ICameraDataService
{
    public Task<CameraModel> GetCameraById(string cameraId);

}
