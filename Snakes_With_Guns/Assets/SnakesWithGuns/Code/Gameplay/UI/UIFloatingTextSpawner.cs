using System.Collections.Generic;
using SnakesWithGuns.Gameplay.Messages;
using SnakesWithGuns.Infrastructure;
using SnakesWithGuns.Infrastructure.PubSub;
using SnakesWithGuns.Utilities;
using UnityEngine;
using UnityEngine.Pool;

namespace SnakesWithGuns.Gameplay.UI
{
    public class UIFloatingTextSpawner : MonoBehaviour, ISceneService
    {
        [SerializeField] private FloatingText _textPrefab;

        private ObjectPool<FloatingText> _pool;
        private IChannel<SpawnFloatingTextMessage> _channel;
        private Dictionary<int, FloatingText> _floatingTextById;
        private Queue<int> _floatingTextToRemove;

        public void Initialize()
        {
            _pool = new ComponentPool<FloatingText>(_textPrefab);
            _floatingTextById = new Dictionary<int, FloatingText>();
            _floatingTextToRemove = new Queue<int>();
            _channel = Channels.GetChannel<SpawnFloatingTextMessage>();
            _channel.Register(OnMessage);
        }

        public void Activate() { }

        public void Tick(float deltaTime)
        {
            CheckForTimeOuts();
            RemoveTimeOutText();
        }

        public void Deactivate() { }

        public void Cleanup()
        {
            _channel.Unregister(OnMessage);
        }

        private void RemoveTimeOutText()
        {
            while (_floatingTextToRemove.Count > 0)
            {
                int id = _floatingTextToRemove.Dequeue();
                _floatingTextById[id].Despawn(OnDespawn);
                _floatingTextById.Remove(id);
            }
        }

        private void CheckForTimeOuts()
        {
            foreach (var valuePair in _floatingTextById)
            {
                if (valuePair.Value.IsTimeout)
                    _floatingTextToRemove.Enqueue(valuePair.Key);
            }
        }

        private void OnMessage(SpawnFloatingTextMessage message)
        {
            if (!_floatingTextById.ContainsKey(message.InstanceID))
            {
                FloatingText floatingText = _pool.Get();
                floatingText.Display(message);
                _floatingTextById.Add(message.InstanceID, floatingText);
            }
            else
            {
                _floatingTextById[message.InstanceID].Display(message);
            }
        }

        private void OnDespawn(FloatingText floatingText)
        {
            _pool.Release(floatingText);
        }
    }
}