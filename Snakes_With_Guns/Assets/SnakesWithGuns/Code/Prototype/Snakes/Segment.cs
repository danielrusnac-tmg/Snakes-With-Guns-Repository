using UnityEngine;

namespace SnakesWithGuns.Prototype.Snakes
{
    public class Segment : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        
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