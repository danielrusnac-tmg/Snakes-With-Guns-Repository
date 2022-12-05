using UnityEngine;

namespace SnakesWithGuns.Prototype.Utilities.Audio
{
    public interface IAudioService
    {
        void PlaySfx(AudioClip clip, float volume);
    }
}