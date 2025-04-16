using TechnicalInterView.VideoAnalytics.Models;


namespace VideoAnalytics.Services
{
    public interface ICameraDataService
    {
        Task<List<CameraConfig>> GetCamerasAsync();
        Task<List<string>> GetTenantIdsAsync();
        Task UpdateCameraStatusAsync(string cameraId, string status);
        Task UpdateUseCaseAsync(string tenantId, string cameraId, string useCase);
    }
}
