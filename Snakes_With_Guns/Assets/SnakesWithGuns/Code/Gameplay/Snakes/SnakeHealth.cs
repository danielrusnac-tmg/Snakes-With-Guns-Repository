using SnakesWithGuns.Gameplay.Messages;
using SnakesWithGuns.Gameplay.Weapons;
using SnakesWithGuns.Infrastructure.PubSub;
using SnakesWithGuns.Utilities.CameraShake;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SnakesWithGuns.Gameplay.Snakes
{
    public class SnakeHealth : MonoBehaviour
    {
        [SerializeField] private float _damageCooldown = 0.3f;
        [SerializeField] private Tail _tail;
        [SerializeField] private Actor _actor;

        [Header("Explosion")]
        [SerializeField] private float _explosionRadius = 7;
        [SerializeField] private int _explosionDamage = 50;
        [SerializeField] private ParticleSystem _damageEffect;

        private float _lastHeadKillTime;
        private IChannel<ScreenShakeMessage> _screenShakeChannel;
        private IChannel<GameOverMessage> _gameOverChannel;

        private bool CanLoseHead => Time.time - _lastHeadKillTime > _damageCooldown;

        private void Awake()
        {
            _screenShakeChannel = Channels.GetChannel<ScreenShakeMessage>();
            _gameOverChannel = Channels.GetChannel<GameOverMessage>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Enemy") && CanLoseHead)
                KillHeadSegment();
        }

        private void KillHeadSegment()
        {
            if (_tail.RemoveSegment())
            {
                _damageEffect.Play();
                DamageUtility.DealDamageInRadius(transform.position, _explosionRadius, _explosionDamage,
                    _actor.SourceID);
                _screenShakeChannel.Publish(new ScreenShakeMessage(CameraShakeType.Strong));

                if (_tail.Segments.Count == 0)
                    Invoke(nameof(OnDie), 1.5f);
            }

            _lastHeadKillTime = Time.time;
        }

        private void OnDie()
        {
            _gameOverChannel.Publish(new GameOverMessage());
        }

        private void OnDrawGizmosSelected()
        {
#if UNITY_EDITOR
            Handles.DrawWireDisc(transform.position, Vector3.up, _explosionRadius);
#endif
        }
    }
}