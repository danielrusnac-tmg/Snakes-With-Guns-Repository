using Lofelt.NiceVibrations;

namespace SnakesWithGuns.Prototype.Utilities.Vibrations
{
    public class NiceVibrationWrapper : IVibration
    {
        public void Play(VibrationType vibration)
        {
            HapticPatterns.PlayPreset(GetPreset(vibration));
        }

        private HapticPatterns.PresetType GetPreset(VibrationType vibration)
        {
            return vibration switch
            {
                VibrationType.Weak => HapticPatterns.PresetType.LightImpact,
                VibrationType.Medium => HapticPatterns.PresetType.MediumImpact,
                VibrationType.Strong => HapticPatterns.PresetType.HeavyImpact,
                _ => HapticPatterns.PresetType.None
            };
        }
    }
}