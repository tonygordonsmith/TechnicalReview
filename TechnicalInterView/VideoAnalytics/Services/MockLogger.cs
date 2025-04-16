namespace VideoAnalytics.Services
{
    public class MockLogger : ICustomLogger
    {
        public async Task LogAsync(string message, string tenantId)
        {
            Console.WriteLine($"[LOG] Tenant: {tenantId}, Message: {message}");
            await Task.CompletedTask;
        }
        public async Task LogErrorAsync(string message, string tenantId, Exception ex)
        {
            Console.WriteLine($"[ERROR] Tenant: {tenantId}, Message: {message}, Exception: {(ex != null ? ex.Message : "None")}");
            await Task.CompletedTask;
        }
    }
}