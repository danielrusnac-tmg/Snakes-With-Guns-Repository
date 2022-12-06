using System;
using SnakesWithGuns.Gameplay.Messages;
using SnakesWithGuns.Gameplay.Weapons;
using SnakesWithGuns.Infrastructure.PubSub;
using SnakesWithGuns.Utilities;
using UnityEngine;

namespace SnakesWithGuns.Gameplay.Objects
{
    [RequireComponent(typeof(Health))]
    public class Dummy : MonoBehaviour, ITarget
    {
        public event Action<ITarget> Died;

        [SerializeField] private int _energy = 1;
        [SerializeField] private float _speed;
        [SerializeField] private Health _health;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private EmissionFlashEffect _flashEffect;

        private Transform _transform;
        private IChannel<SpawnEnergyMessage> _spawnEnergyChannel;
        private Transform _target;
        private bool _isDead;

        public Vector3 Position => _transform.position;

        private void Reset()
        {
            _health = GetComponent<Health>();
        }

        private void Awake()
        {
            _spawnEnergyChannel = Channels.GetChannel<SpawnEnergyMessage>();
            _transform = transform;
            _health.Died += OnDied;
            _health.Changed += OnHealthChanged;
        }

        private void OnDestroy()
        {
            _health.Died -= OnDied;
            _health.Changed -= OnHealthChanged;
        }

        private void FixedUpdate()
        {
            Vector3 direction = (_target.position - _transform.position).normalized;
            _rigidbody.velocity = direction * _speed;
        }

        public void Initialize(Transform target)
        {
            _isDead = false;
            _target = target;
            _flashEffect.ResetFlash();
            _health.ResetHealth();
        }

        private void OnHealthChanged(Health.ChangeData data)
        {
            if (data.Delta < 0)
                _flashEffect.Flash();
        }

        private void OnDied()
        {
            if (_isDead)
                return;

            _isDead = true;

            _spawnEnergyChannel.Publish(new SpawnEnergyMessage()
            {
                Amount = _energy,
                Position = Position
            });

            Died?.Invoke(this);
        }
    }
}