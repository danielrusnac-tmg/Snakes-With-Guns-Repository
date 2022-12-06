using System;
using SnakesWithGuns.Gameplay.Messages;
using SnakesWithGuns.Gameplay.Weapons;
using SnakesWithGuns.Infrastructure.PubSub;
using UnityEngine;

namespace SnakesWithGuns.Gameplay.Objects
{
    [RequireComponent(typeof(Health))]
    public class Dummy : MonoBehaviour, ITarget
    {
        public event Action<ITarget> Died;

        [SerializeField] private int _energy = 1;
        [SerializeField] private Health _health;

        private Transform _transform;
        private IChannel<SpawnEnergyMessage> _spawnEnergyChannel;

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

        private void OnDestroy()
        {
            _health.Died -= OnDied;
        }

        private void OnDied()
        {
            _spawnEnergyChannel.Publish(new SpawnEnergyMessage()
            {
                Amount = _energy,
                Position = Position
            });
            
            Died?.Invoke(this);
            Destroy(gameObject);
        }
    }
}