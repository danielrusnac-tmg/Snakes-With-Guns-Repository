using UnityEngine;
using UnityEngine.Pool;

namespace SnakesWithGuns.Prototype.Utilities.Audio
{
    public class AudioService : MonoBehaviour, IAudioService
    {
        [SerializeField] private AudioSource _sfXPrefab;

        private ObjectPool<AudioSource> _pool;

        private void Awake()
        {
            _pool = new ObjectPool<AudioSource>(
                () => Instantiate(_sfXPrefab, transform),
                null, null, source => Destroy(source.gameObject), false, 50);
        }

        public void PlaySfx(AudioClip clip, float volume) { }
    }
}