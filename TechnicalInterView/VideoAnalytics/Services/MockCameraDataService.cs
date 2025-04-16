using System.Text.Json;
using TechnicalInterView.VideoAnalytics.Models;

namespace VideoAnalytics.Services
{
    public class MockCameraDataService : ICameraDataService
    {
        private readonly List<CameraConfig> _cameras;
        private readonly ICustomLogger _logger;
        private readonly Random _random = new Random();

        public MockCameraDataService(IWebHostEnvironment environment, ICustomLogger logger)
        {
            _logger = logger;
            var path = Path.Combine(environment.WebRootPath, "data", "cameras.json");
            if (File.Exists(path))
            {
                try
                {
                    var json = File.ReadAllText(path);
                    _cameras = JsonSerializer.Deserialize<List<CameraConfig>>(json) ?? new List<CameraConfig>();
                    Console.WriteLine($"MockCameraDataService: Loaded {_cameras.Count} cameras");
                }
                catch (Exception ex)
                {
                    _cameras = new List<CameraConfig>();
                    Console.WriteLine($"MockCameraDataService: Error loading JSON: {ex.Message}");
                    _logger.LogErrorAsync($"Error loading cameras.json: {ex.Message}", null, ex).GetAwaiter().GetResult();
                }
            }
            else
            {
                _cameras = new List<CameraConfig>();
                Console.WriteLine("MockCameraDataService: JSON file not found");
                _logger.LogErrorAsync("cameras.json not found", null, null).GetAwaiter().GetResult();
            }
        }

        public async Task<List<CameraConfig>> GetCamerasAsync()
        {
            await Task.Delay(100); // Simulate async
            return _cameras;
        }

        public async Task<List<string>> GetTenantIdsAsync()
        {
            await Task.Delay(100);
            return _cameras.Select(c => c.TenantId).Distinct().ToList();
        }

        public async Task UpdateCameraStatusAsync(string cameraId, string status)
        {
            var camera = _cameras.FirstOrDefault(c => c.CameraId == cameraId);
            if (camera != null)
            {
                camera.Status = status;
                Console.WriteLine($"MockCameraDataService: Updated {cameraId} to {status}");
                await _logger.LogAsync($"Updated status for camera {cameraId} to {status}", null);
            }
            await Task.CompletedTask;
        }

        public async Task UpdateUseCaseAsync(string tenantId, string cameraId, string useCase)
        {
            await Task.Delay(500); // Simulate network delay
            if (_random.Next(0, 10) == 0)
            {
                await _logger.LogErrorAsync($"Failed to update use case for camera {cameraId}, tenant {tenantId}.", tenantId, null);
                throw new Exception("Failed to update use case.");
            }

            var camera = _cameras.FirstOrDefault(c => c.CameraId == cameraId && c.TenantId == tenantId);
            if (camera != null)
            {
                camera.UseCase = useCase;
                Console.WriteLine($"MockCameraDataService: Updated {cameraId} use case to {useCase}");
                await _logger.LogAsync($"Successfully updated use case for camera {cameraId} to {useCase}, tenant {tenantId}.", tenantId);
            }
            else
            {
                await _logger.LogErrorAsync($"Camera {cameraId} not found for tenant {tenantId}.", tenantId, null);
                throw new Exception("Camera not found.");
            }
        }
    }
}