using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class ResolutionDropdownMenu : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private Dropdown resolutionDropdown;
        [SerializeField] private Toggle fullscreenToggle;
        
        [Header("Configuration")]
        [SerializeField] private ResolutionConfig resolutionConfig;
        [SerializeField] private bool applyImmediately = true;
        [SerializeField] private bool saveToPlayerPrefs = true;
        
        [Header("PlayerPrefs Keys")]
        [SerializeField] private string resolutionWidthKey = "ScreenWidth";
        [SerializeField] private string resolutionHeightKey = "ScreenHeight";
        [SerializeField] private string fullscreenKey = "Fullscreen";

        private List<ResolutionData> availableResolutions = new List<ResolutionData>();
        private int currentResolutionIndex = 0;

        private void Start()
        {
            InitializeDropdown();
            LoadSavedSettings();
            SetupEventListeners();
        }

        private void InitializeDropdown()
        {
            availableResolutions.Clear();
            
            // Add resolutions from config
            if (resolutionConfig != null)
            {
                availableResolutions.AddRange(resolutionConfig.availableResolutions);
            }
            
            // Add system resolutions if enabled
            if (resolutionConfig == null || resolutionConfig.includeSystemResolutions)
            {
                AddSystemResolutions();
            }
            
            // Remove duplicates and sort
            availableResolutions = availableResolutions
                .GroupBy(r => new { r.width, r.height })
                .Select(g => g.First())
                .OrderByDescending(r => r.width * r.height)
                .ToList();
            
            PopulateDropdown();
        }

        private void AddSystemResolutions()
        {
            Resolution[] systemResolutions = Screen.resolutions;
            foreach (Resolution res in systemResolutions)
            {
                // Only add if not already in the list
                if (!availableResolutions.Any(r => r.width == res.width && r.height == res.height))
                {
                    availableResolutions.Add(new ResolutionData(res.width, res.height));
                }
            }
        }

        private void PopulateDropdown()
        {
            if (resolutionDropdown == null) return;

            resolutionDropdown.ClearOptions();
            
            List<string> options = availableResolutions.Select(r => r.displayName).ToList();
            resolutionDropdown.AddOptions(options);
            
            // Set current resolution
            FindCurrentResolutionIndex();
            resolutionDropdown.value = currentResolutionIndex;
            resolutionDropdown.RefreshShownValue();
        }

        private void FindCurrentResolutionIndex()
        {
            int currentWidth = Screen.width;
            int currentHeight = Screen.height;
            
            for (int i = 0; i < availableResolutions.Count; i++)
            {
                if (availableResolutions[i].width == currentWidth && 
                    availableResolutions[i].height == currentHeight)
                {
                    currentResolutionIndex = i;
                    return;
                }
            }
            
            // If current resolution not found, default to first option
            currentResolutionIndex = 0;
        }

        private void SetupEventListeners()
        {
            if (resolutionDropdown != null)
            {
                resolutionDropdown.onValueChanged.AddListener(OnResolutionChanged);
            }
            
            if (fullscreenToggle != null)
            {
                fullscreenToggle.onValueChanged.AddListener(OnFullscreenChanged);
            }
        }

        private void LoadSavedSettings()
        {
            if (!saveToPlayerPrefs) return;
            
            // Load fullscreen setting
            if (fullscreenToggle != null)
            {
                bool isFullscreen = PlayerPrefs.GetInt(fullscreenKey, 
                    resolutionConfig?.fullscreenByDefault == true ? 1 : 0) == 1;
                fullscreenToggle.isOn = isFullscreen;
            }
            
            // Load resolution setting
            if (PlayerPrefs.HasKey(resolutionWidthKey) && PlayerPrefs.HasKey(resolutionHeightKey))
            {
                int savedWidth = PlayerPrefs.GetInt(resolutionWidthKey);
                int savedHeight = PlayerPrefs.GetInt(resolutionHeightKey);
                
                for (int i = 0; i < availableResolutions.Count; i++)
                {
                    if (availableResolutions[i].width == savedWidth && 
                        availableResolutions[i].height == savedHeight)
                    {
                        currentResolutionIndex = i;
                        if (resolutionDropdown != null)
                        {
                            resolutionDropdown.value = i;
                            resolutionDropdown.RefreshShownValue();
                        }
                        break;
                    }
                }
            }
        }

        private void OnResolutionChanged(int resolutionIndex)
        {
            if (resolutionIndex < 0 || resolutionIndex >= availableResolutions.Count) return;
            
            currentResolutionIndex = resolutionIndex;
            
            if (applyImmediately)
            {
                ApplyResolution();
            }
        }

        private void OnFullscreenChanged(bool isFullscreen)
        {
            if (applyImmediately)
            {
                Screen.fullScreen = isFullscreen;
                
                if (saveToPlayerPrefs)
                {
                    PlayerPrefs.SetInt(fullscreenKey, isFullscreen ? 1 : 0);
                    PlayerPrefs.Save();
                }
            }
        }

        public void ApplyResolution()
        {
            if (currentResolutionIndex < 0 || currentResolutionIndex >= availableResolutions.Count) return;
            
            ResolutionData selectedResolution = availableResolutions[currentResolutionIndex];
            bool isFullscreen = fullscreenToggle != null ? fullscreenToggle.isOn : Screen.fullScreen;
            
            Screen.SetResolution(selectedResolution.width, selectedResolution.height, isFullscreen);
            
            if (saveToPlayerPrefs)
            {
                PlayerPrefs.SetInt(resolutionWidthKey, selectedResolution.width);
                PlayerPrefs.SetInt(resolutionHeightKey, selectedResolution.height);
                PlayerPrefs.SetInt(fullscreenKey, isFullscreen ? 1 : 0);
                PlayerPrefs.Save();
            }
            
            Debug.Log($"Resolution changed to: {selectedResolution.displayName}, Fullscreen: {isFullscreen}");
        }

        public void ResetToDefault()
        {
            if (resolutionConfig != null && resolutionConfig.availableResolutions.Length > 0)
            {
                ResolutionData defaultRes = resolutionConfig.availableResolutions[0];
                
                for (int i = 0; i < availableResolutions.Count; i++)
                {
                    if (availableResolutions[i].width == defaultRes.width && 
                        availableResolutions[i].height == defaultRes.height)
                    {
                        resolutionDropdown.value = i;
                        currentResolutionIndex = i;
                        break;
                    }
                }
                
                if (fullscreenToggle != null)
                {
                    fullscreenToggle.isOn = resolutionConfig.fullscreenByDefault;
                }
                
                ApplyResolution();
            }
        }

        // Public methods for external access
        public void SetResolution(int width, int height, bool fullscreen = true)
        {
            for (int i = 0; i < availableResolutions.Count; i++)
            {
                if (availableResolutions[i].width == width && availableResolutions[i].height == height)
                {
                    resolutionDropdown.value = i;
                    currentResolutionIndex = i;
                    
                    if (fullscreenToggle != null)
                        fullscreenToggle.isOn = fullscreen;
                    
                    ApplyResolution();
                    return;
                }
            }
        }

        public ResolutionData GetCurrentResolution()
        {
            if (currentResolutionIndex >= 0 && currentResolutionIndex < availableResolutions.Count)
                return availableResolutions[currentResolutionIndex];
            
            return null;
        }

        private void OnDestroy()
        {
            if (resolutionDropdown != null)
                resolutionDropdown.onValueChanged.RemoveListener(OnResolutionChanged);
            
            if (fullscreenToggle != null)
                fullscreenToggle.onValueChanged.RemoveListener(OnFullscreenChanged);
        }
    }
}