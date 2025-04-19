using System;
using TechnicalInterView.Cameras.Enum;

namespace TechnicalInterView.Cameras.Extensions
{
    public static class CameraUseCaseEnumExtensions
    {
        public static string ToString(this CameraUseCaseEnum useCase)
        {
            return useCase switch
            {
                CameraUseCaseEnum.FireDetection => "Fire Detection",
                CameraUseCaseEnum.OverCrowding => "Over Crowding",
                CameraUseCaseEnum.PeopleCounting => "People Counting",
                CameraUseCaseEnum.UnusualBehaviour => "Unusual Behaviour",
                CameraUseCaseEnum.Unknown => "Unknown",
                _ => throw new ArgumentException($"Invalid camera use case: {useCase}")
            };
        }

        public static CameraUseCaseEnum FromString(string useCaseString)
        {
            return useCaseString?.Trim().ToLowerInvariant() switch
            {
                "firedetection" => CameraUseCaseEnum.FireDetection,
                "overcrowding" or "over crowding" => CameraUseCaseEnum.OverCrowding,
                "peoplecounting" => CameraUseCaseEnum.PeopleCounting,
                "unusualbehavior" => CameraUseCaseEnum.UnusualBehaviour,
                "unknown" or null => CameraUseCaseEnum.Unknown,
                _ => throw new ArgumentException($"Cannot parse '{useCaseString}' to CameraUseCaseEnum")
            };
        }
    }
}