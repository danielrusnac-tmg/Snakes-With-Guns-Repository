using SnakesWithGuns.Infrastructure.PubSub;
using SnakesWithGuns.Utilities.CameraShake;
using UnityEngine;

namespace SnakesWithGuns.Gameplay.Snakes
{
    public class SnakeHealth : MonoBehaviour
    {
        [SerializeField] private float _damageCooldown = 0.3f;
        [SerializeField] private Tail _tail;
        [SerializeField] private ParticleSystem _damageEffect;

        private float _lastHeadKillTime;
        private IChannel<ScreenShakeMessage> _screenShakeChannel;

        private bool CanLoseHead => Time.time - _lastHeadKillTime > _damageCooldown;

        private void Awake()
        {
            _screenShakeChannel = Channels.GetChannel<ScreenShakeMessage>();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Enemy") && CanLoseHead)
                KillHeadSegment();
        }

        private void KillHeadSegment()
        {
            if (_tail.RemoveSegment())
            {
                _damageEffect.Play();
                _screenShakeChannel.Publish(new ScreenShakeMessage(CameraShakeType.Strong));
            }

            _lastHeadKillTime = Time.time;
        }
    }
}