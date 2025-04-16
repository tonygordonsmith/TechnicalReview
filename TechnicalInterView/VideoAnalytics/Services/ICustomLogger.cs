namespace VideoAnalytics.Services
{
    public interface ICustomLogger
    {
        Task LogAsync(string message, string tenantId);
        Task LogErrorAsync(string message, string tenantId, Exception ex);
    }
}