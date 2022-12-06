using Lofelt.NiceVibrations;
using UnityEngine;

namespace SnakesWithGuns.Utilities.Vibrations
{
    public class NiceVibrationWithPresets : IVibration
    {
        private float _lastPlayTime;
        private float _cooldown;

        private bool CanPlay => (Time.time - _lastPlayTime) > _cooldown;
        
        public void Play(VibrationType vibration)
        {
            if (!CanPlay)
                return;

            HapticPatterns.PresetType preset = GetPreset(vibration);
            HapticPatterns.PlayPreset(preset);

            _cooldown = HapticPatterns.GetPresetDuration(preset) * 2f;
            _lastPlayTime = Time.time;
        }

        private HapticPatterns.PresetType GetPreset(VibrationType vibration)
        {
            return vibration switch
            {
                VibrationType.Weak => HapticPatterns.PresetType.Selection,
                VibrationType.Medium => HapticPatterns.PresetType.LightImpact,
                VibrationType.Strong => HapticPatterns.PresetType.HeavyImpact,
                _ => HapticPatterns.PresetType.None
            };
        }
    }
}