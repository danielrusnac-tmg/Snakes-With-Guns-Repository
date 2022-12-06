using Lofelt.NiceVibrations;

namespace SnakesWithGuns.Utilities.Vibrations
{
    public class NiceVibrationCustom : IVibration
    {
        public void Play(VibrationType vibration)
        {
            HapticPatterns.PlayEmphasis(0.1f, 0f);
        }
    }
}