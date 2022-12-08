using SnakesWithGuns.Gameplay.Snakes;
using UnityEngine;

namespace SnakesWithGuns.Gameplay.AI
{
    public class BotInputProvider : MonoBehaviour, ISnakeInputProvider
    {
        [SerializeField] private float _positionRadius = 25;
        [SerializeField] private float _targetMinDistance = 1;
        [SerializeField] private Transform _target;

        private Transform _transform;

        public Vector3 Direction => CalculateDirection();

        private void Awake()
        {
            _transform = transform;
            _target = new GameObject($"target_{name}").transform;
            _target.hideFlags = HideFlags.HideInHierarchy;
        }

        private void Start()
        {
            InvokeRepeating(nameof(MoveTargetToRandomPosition), 0, Random.Range(2, 10));
            InvokeRepeating(nameof(CheckTargetDistance), 1, Random.Range(1f, 2f));
        }

        private Vector3 CalculateDirection()
        {
            return (_target.position - _transform.position).normalized;
        }

        private void CheckTargetDistance()
        {
            if (Vector3.Distance(_transform.position, transform.position) < _targetMinDistance)
                MoveTargetToRandomPosition();
        }

        private void MoveTargetToRandomPosition()
        {
            _target.position = new Vector3(
                Random.Range(-_positionRadius, _positionRadius),
                0f,
                Random.Range(-_positionRadius, _positionRadius));
        }
    }
}