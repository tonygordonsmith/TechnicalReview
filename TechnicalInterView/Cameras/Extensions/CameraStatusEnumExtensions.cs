using System;
using TechnicalInterView.Cameras.Enum;

namespace TechnicalInterView.Cameras.Extensions
{
    public static class CameraStatusEnumExtensions
    {
        public static string ToString(this CameraStatusEnum status)
        {
            return status switch
            {
                CameraStatusEnum.Online => "Online",
                CameraStatusEnum.Offline => "Offline",
                CameraStatusEnum.Unkown => "Unknown",
                _ => throw new ArgumentException($"Invalid camera status: {status}")
            };
        }

        public static CameraStatusEnum FromString(string statusString)
        {
            return statusString?.Trim().ToLowerInvariant() switch
            {
                "online" => CameraStatusEnum.Online,
                "offline" => CameraStatusEnum.Offline,
                "unknown" or null => CameraStatusEnum.Unkown,
                _ => throw new ArgumentException($"Cannot parse '{statusString}' to CameraStatusEnum")
            };
        }
    }
}