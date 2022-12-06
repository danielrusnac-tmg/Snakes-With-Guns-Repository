using UnityEngine;

namespace SnakesWithGuns.Gameplay.Snakes
{
    public class Segment : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Transform _modulePoint;

        public Transform ModulePoint => _modulePoint;

        public Vector3 Position
        {
            get => _rigidbody.position;
            set => _rigidbody.MovePosition(value);
        }

        public Quaternion Rotation
        {
            get => _rigidbody.rotation;
            set => _rigidbody.MoveRotation(value);
        }
    }
}