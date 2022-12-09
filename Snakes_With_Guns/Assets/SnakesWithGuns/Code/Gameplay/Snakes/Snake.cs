using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace SnakesWithGuns.Gameplay.Snakes
{
    public class Snake : Actor
    {
        private const float LOSE_HEAD_COOLDOWN = 0.1f;
        
        [SerializeField] private SnakeStats _stats = new();
        [SerializeField] private SnakeMover _mover;
        [SerializeField] private Tail _tail;

        private float _lastHeadKillTime;
        private ISnakeInputProvider _inputProvider;

        public SnakeStats Stats => _stats;
        public Tail Tail => _tail;

        private bool CanLoseHead => Time.time - _lastHeadKillTime > LOSE_HEAD_COOLDOWN;

        private void Awake()
        {
            _inputProvider = GetComponent<ISnakeInputProvider>();
            Assert.IsNotNull(_inputProvider);
        }

        private void Update()
        {
            _mover.Direction = _inputProvider.Direction;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Enemy") && CanLoseHead)
                KillHeadSegment();
        }

        [ContextMenu(nameof(LevelUp))]
        private void LevelUp()
        {
            _stats.Level.Value++;
        }

        private void KillHeadSegment()
        {
            _tail.RemoveSegment();
            _lastHeadKillTime = Time.time;
        }
    }
}