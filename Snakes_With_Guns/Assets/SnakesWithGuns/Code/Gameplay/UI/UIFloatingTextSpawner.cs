using DG.Tweening;
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
        [SerializeField] private float _variation = 1f;
        [SerializeField] private float _movement = 3f;
        [SerializeField] private float _duration = 3f;
        [SerializeField] private Ease _ease = Ease.OutBack;

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
            Transform t = text.transform;
            text.color = message.Color;
            text.SetText(message.Message);
            t.position = message.Position + Random.insideUnitSphere * _variation;
            t.DOMove(t.position + t.up * _movement, _duration).SetEase(_ease).OnComplete(() => _pool.Release(text));
        }
    }
}