using UnityEngine;
using UnityEngine.Assertions;

namespace SnakesWithGuns.Prototype.Snakes
{
    public class Snake : MonoBehaviour
    {
        [SerializeField] private SnakeMover _mover;

        private ISnakeInputProvider _inputProvider;

        private void Awake()
        {
            _inputProvider = GetComponent<ISnakeInputProvider>();
            Assert.IsNotNull(_inputProvider);
        }

        private void Update()
        {
            _mover.Direction = _inputProvider.Direction;
        }
    }
}