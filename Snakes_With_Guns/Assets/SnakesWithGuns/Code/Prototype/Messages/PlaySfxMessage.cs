using UnityEngine;

namespace SnakesWithGuns.Prototype.Messages
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