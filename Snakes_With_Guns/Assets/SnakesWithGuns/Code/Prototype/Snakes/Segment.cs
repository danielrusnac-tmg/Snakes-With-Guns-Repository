using UnityEngine;

namespace SnakesWithGuns.Prototype.Snakes
{
    public class Segment : MonoBehaviour
    {
        private Transform _transform;

        public Transform Transform => _transform;

        private void Awake()
        {
            _transform = transform;
        }
    }
}