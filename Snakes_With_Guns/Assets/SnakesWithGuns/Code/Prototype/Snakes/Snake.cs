using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace SnakesWithGuns.Prototype.Snakes
{
    public class Snake : MonoBehaviour
    {
        [SerializeField] private SnakeMover _mover;
        [SerializeField] private Tail _tail;

        private ISnakeInputProvider _inputProvider;

        public IReadOnlyList<Segment> Segments => _tail.Segments;

        private void Awake()
        {
            _inputProvider = GetComponent<ISnakeInputProvider>();
            Assert.IsNotNull(_inputProvider);
        }

        private void Update()
        {
            _mover.Direction = _inputProvider.Direction;
        }

        public void InstallModule(int at, ISegmentModule module)
        {
            Segments[at].InstallModule(module);
        }

        public void UninstallModule(int at)
        {
            Segments[at].UninstallModule();
        }
    }
}