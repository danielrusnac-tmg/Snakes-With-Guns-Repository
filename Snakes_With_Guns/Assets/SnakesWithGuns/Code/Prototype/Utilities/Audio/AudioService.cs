using System.Collections;
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

        public void PlaySfx(AudioClip clip, float volume)
        {
            AudioSource source = _pool.Get();
            source.PlayOneShot(clip, volume);
            StartCoroutine(ReturnSourceToPoolRoutine(source, clip.length));
        }

        private IEnumerator ReturnSourceToPoolRoutine(AudioSource source, float delay)
        {
            yield return new WaitForSeconds(delay);
            _pool.Release(source);
        }
    }
}