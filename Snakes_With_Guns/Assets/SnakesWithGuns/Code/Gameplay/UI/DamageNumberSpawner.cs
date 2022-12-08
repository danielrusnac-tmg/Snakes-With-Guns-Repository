using System;
using SnakesWithGuns.Gameplay.Messages;
using SnakesWithGuns.Gameplay.Objects;
using SnakesWithGuns.Infrastructure.PubSub;
using UnityEngine;

namespace SnakesWithGuns.Gameplay.UI
{
    public class DamageNumberSpawner : MonoBehaviour
    {
        [SerializeField] private Color _color = Color.red;
        [SerializeField] private Health _health;
        
        private Transform _transform;
        private IChannel<SpawnFloatingTextMessage> _worldTextChannel;

        private void Awake()
        {
            _transform = transform;
            _health.Changed += OnChanged;
            _worldTextChannel = Channels.GetChannel<SpawnFloatingTextMessage>();
        }

        private void OnDestroy()
        {
            _health.Changed -= OnChanged;
        }

        private void OnChanged(Health.ChangeData data)
        {
            _worldTextChannel.Publish(new SpawnFloatingTextMessage
            {
                Value = data.Delta,
                Position = _transform.position + Vector3.up,
                Color = _color,
                InstanceID = GetInstanceID()
            });
        }
    }
}