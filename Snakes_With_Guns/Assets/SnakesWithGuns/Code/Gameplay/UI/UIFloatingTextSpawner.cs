using SnakesWithGuns.Gameplay.Messages;
using SnakesWithGuns.Infrastructure.PubSub;
using SnakesWithGuns.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.Pool;

namespace SnakesWithGuns.Gameplay.UI
{
    public class UIFloatingTextSpawner : MonoBehaviour
    {
        [SerializeField] private TMP_Text _textPrefab;
        [SerializeField] private Vector3 _positionOffset;

        private ObjectPool<TMP_Text> _pool;
        private IChannel<SpawnFloatingTextMessage> _channel;

        private void Awake()
        {
            _pool = new ComponentPool<TMP_Text>(_textPrefab);
            _channel = Channels.GetChannel<SpawnFloatingTextMessage>();
            _channel.Register(OnMessage);
        }

        private void OnDestroy()
        {
            _channel.Unregister(OnMessage);
        }

        private void OnMessage(SpawnFloatingTextMessage message)
        {
            TMP_Text text = _pool.Get();
            text.transform.position = message.Position + _positionOffset;
        }
    }
}