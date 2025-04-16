using Microsoft.AspNetCore.SignalR;
using VideoAnalytics.Hubs;

namespace VideoAnalytics.Services
{
    public class MockStatusService : IHostedService, IDisposable
    {
        private readonly IHubContext<StatusHub> _hubContext;
        private readonly IServiceProvider _serviceProvider;
        private Timer? _timer;
        private static readonly Random _random = new Random();

        public MockStatusService(IHubContext<StatusHub> hubContext, IServiceProvider serviceProvider)
        {
            _hubContext = hubContext;
            _serviceProvider = serviceProvider;
            Console.WriteLine("MockStatusService: Initialized");
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("MockStatusService: Starting timer");
            _timer = new Timer(UpdateStatuses, null, TimeSpan.Zero, TimeSpan.FromSeconds(2)); // Changed to 2 seconds
            return Task.CompletedTask;
        }

        private async void UpdateStatuses(object? state)
        {
            Console.WriteLine("MockStatusService: UpdateStatuses called");
            try
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var cameraDataService = scope.ServiceProvider.GetRequiredService<ICameraDataService>();
                    var cameras = await cameraDataService.GetCamerasAsync();
                    Console.WriteLine($"MockStatusService: Loaded {cameras.Count} cameras");

                    foreach (var camera in cameras)
                    {
                        if (_random.NextDouble() < 0.8) // Changed to 80%
                        {
                            var newStatus = _random.Next(0, 2) == 0 ? "Online" : "Offline";
                            await cameraDataService.UpdateCameraStatusAsync(camera.CameraId, newStatus);
                            await _hubContext.Clients.All.SendAsync("ReceiveStatusUpdate", camera.CameraId, newStatus);
                            Console.WriteLine($"MockStatusService: Updated {camera.CameraId} to {newStatus}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"MockStatusService: Error in UpdateStatuses: {ex.Message}");
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("MockStatusService: Stopping timer");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Console.WriteLine("MockStatusService: Disposing");
            _timer?.Dispose();
        }
    }
}