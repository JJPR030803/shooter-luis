using System;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public class ResolutionData
    {
        public int width;
        public int height;
        public string displayName;

        public ResolutionData(int width, int height)
        {
            this.width = width;
            this.height = height;
            this.displayName = $"{width} x {height}";
        }

        public ResolutionData(int width, int height, string customDisplayName)
        {
            this.width = width;
            this.height = height;
            this.displayName = customDisplayName;
        }
    }

    [CreateAssetMenu(fileName = "ResolutionConfig", menuName = "UI/Resolution Config")]
    public class ResolutionConfig : ScriptableObject
    {
        [Header("Available Resolutions")]
        public ResolutionData[] availableResolutions = {
            new ResolutionData(1920, 1080, "Full HD (1920x1080)"),
            new ResolutionData(2560, 1440, "2K (2560x1440)"),
            new ResolutionData(3840, 2160, "4K (3840x2160)"),
            new ResolutionData(1366, 768, "HD (1366x768)"),
            new ResolutionData(1280, 720, "HD Ready (1280x720)"),
            new ResolutionData(1600, 900, "HD+ (1600x900)")
        };

        [Header("Settings")]
        public bool includeSystemResolutions = true;
        public bool fullscreenByDefault = true;
    }
}