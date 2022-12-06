using UnityEngine;

namespace SnakesWithGuns.Utilities.Audio
{
    public struct PlaySfxMessage
    {
        public AudioClip Clip;
        public float Volume;

        public PlaySfxMessage(AudioClip clip, float volume = 1f)
        {
            Clip = clip;
            Volume = volume;
        }
    }
}