using SnakesWithGuns.Gameplay.Snakes;
using UnityEngine;

namespace SnakesWithGuns.Gameplay.AI
{
    public class BotInputProvider : MonoBehaviour, ISnakeInputProvider
    {
        [SerializeField] private Transform _target;

        private Transform _transform;

        public Vector3 Direction => CalculateDirection();

        private void Awake()
        {
            _transform = transform;
            _target = new GameObject($"target_{name}").transform;
        }

        private Vector3 CalculateDirection()
        {
            return (_target.position - _transform.position).normalized;
        }
    }
}