using System;
using DG.Tweening;
using SnakesWithGuns.Gameplay.Messages;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SnakesWithGuns.Gameplay.UI
{
    public class FloatingText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private float _variation = 1f;
        [SerializeField] private float _movement = 3f;
        [SerializeField] private float _duration = 3f;
        [SerializeField] private float _letGoDelay = 0.4f;
        [SerializeField] private Ease _ease = Ease.OutBack;

        private bool _isFirstDisplay;
        private float _displayTime;
        private int _lastValue;
        private Transform _transform;
        private Action<FloatingText> _despawnAction;
        private Tween _scaleTween;

        public bool IsTimeout => Time.time - _displayTime >= _letGoDelay;

        private void Awake()
        {
            _transform = transform;
            _isFirstDisplay = true;
        }

        public void Display(SpawnFloatingTextMessage message)
        {
            _lastValue += message.Value;

            _text.SetText(_lastValue > 0 ? $"+{_lastValue}" : _lastValue.ToString());

            _displayTime = Time.time;

            if (_isFirstDisplay)
            {
                _text.color = message.Color;
                _transform.position = message.Position + Random.insideUnitSphere * _variation;
                _transform.localScale = Vector3.zero;
                _scaleTween = transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
            }

            _isFirstDisplay = false;
        }
        
        public void Despawn(Action<FloatingText> despawnAction)
        {
            _scaleTween?.Complete();
            _isFirstDisplay = true;
            _despawnAction = despawnAction;
            _lastValue = 0;
            _transform.DOMove(_transform.position + _transform.up * _movement, _duration).SetEase(_ease).OnComplete(() => _despawnAction(this));
        }
    }
}