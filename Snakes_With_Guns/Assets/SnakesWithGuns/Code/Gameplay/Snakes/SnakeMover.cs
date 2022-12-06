using UnityEngine;

namespace SnakesWithGuns.Gameplay.Snakes
{
    public class SnakeMover : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed = 5;
        [SerializeField] private float _turnSpeed = 20;
        [SerializeField] private Rigidbody _rigidbody;

        private Vector3 _wantedDirection;
        private Transform _transform;

        public float MovementSpeed
        {
            get => _movementSpeed;
            set => _movementSpeed = value;
        }
        
        public float TurnSpeed
        {
            get => _turnSpeed;
            set => _turnSpeed = value;
        }
        
        public bool CanMove
        {
            get => !_rigidbody.isKinematic;
            set => _rigidbody.isKinematic = !value;
        }
        
        public Vector3 Direction
        {
            get => _transform.forward;
            set
            {
                if (value.sqrMagnitude > 0f)
                    _wantedDirection = value;
            }
        }

        private void Awake()
        {
            _transform = transform;
        }

        private void FixedUpdate()
        {
            if (!CanMove)
                return;

            _rigidbody.velocity = transform.forward * MovementSpeed;

            if (_wantedDirection.sqrMagnitude > float.Epsilon)
            {
                Quaternion wantedRotation = Quaternion.LookRotation(_wantedDirection, Vector3.up);
                Quaternion newRotation =
                    Quaternion.Lerp(_rigidbody.rotation, wantedRotation, _turnSpeed * Time.fixedDeltaTime);
                _rigidbody.MoveRotation(newRotation);
            }

            _rigidbody.angularVelocity = Vector3.zero;
        }
    }
}