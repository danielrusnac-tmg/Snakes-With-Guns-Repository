using UnityEngine;
using UnityEngine.Assertions;

namespace SnakesWithGuns.Gameplay.Snakes
{
    public class Snake : Actor
    {
        [SerializeField] private SnakeStats _stats = new();
        [SerializeField] private SnakeMover _mover;
        [SerializeField] private Tail _tail;

        private ISnakeInputProvider _inputProvider;

        public SnakeStats Stats => _stats;
        public Tail Tail => _tail;

        private void Awake()
        {
            _inputProvider = GetComponent<ISnakeInputProvider>();
            Assert.IsNotNull(_inputProvider);
        }

        private void Update()
        {
            _mover.Direction = _inputProvider.Direction;
        }

        [ContextMenu(nameof(LevelUp))]
        private void LevelUp()
        {
            _stats.Level.Value++;
        }
    }
}