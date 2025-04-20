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
                CameraUseCaseEnum.FireDetection => "FireDetection",
                CameraUseCaseEnum.OverCrowding => "OverCrowding",
                CameraUseCaseEnum.PeopleCounting => "PeopleCounting",
                CameraUseCaseEnum.UnusualBehaviour => "UnusualBehaviour",
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
                "peoplecounting" or "people counting" => CameraUseCaseEnum.PeopleCounting,
                "unusualbehaviour" or "unusual behaviour" or "unusualbehavior" => CameraUseCaseEnum.UnusualBehaviour,
                "unknown" or null => CameraUseCaseEnum.Unknown,
                _ => throw new ArgumentException($"Cannot parse '{useCaseString}' to CameraUseCaseEnum")
            };
        }
    }
}