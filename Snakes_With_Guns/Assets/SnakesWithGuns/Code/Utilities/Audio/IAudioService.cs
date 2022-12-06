using UnityEngine;

namespace SnakesWithGuns.Utilities.Audio
{
    public interface IAudioService
    {
        void PlaySfx(AudioClip clip, float volume);
    }
}