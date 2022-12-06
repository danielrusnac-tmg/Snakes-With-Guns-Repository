using System;
using SnakesWithGuns.Gameplay.Messages;
using SnakesWithGuns.Gameplay.Weapons;
using SnakesWithGuns.Infrastructure.PubSub;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SnakesWithGuns.Gameplay.Objects
{
    [RequireComponent(typeof(Health))]
    public class Dummy : MonoBehaviour, ITarget
    {
        public event Action<ITarget> Died;

        [SerializeField] private int _energy = 1;
        [SerializeField] private Vector2 _speedRange = new(2f, 3f);
        [SerializeField] private Health _health;
        [SerializeField] private Rigidbody _rigidbody;

        private Transform _transform;
        private IChannel<SpawnEnergyMessage> _spawnEnergyChannel;
        private Transform _target;
        private float _speed;
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
        }

        private void FixedUpdate()
        {
            Vector3 direction = (_target.position - _transform.position).normalized;
            _rigidbody.velocity = direction * _speed;
        }

        private void OnDestroy()
        {
            _health.Died -= OnDied;
        }

        public void Initialize(Transform target)
        {
            _isDead = false;
            _target = target;
            _speed = Random.Range(_speedRange.x, _speedRange.y);
            _health.ResetHealth();
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