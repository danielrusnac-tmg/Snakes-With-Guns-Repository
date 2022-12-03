using UnityEngine;

namespace SnakesWithGuns.Prototype.Snakes
{
    public class SnakeMover : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed = 5;
        [SerializeField] private float _turnSpeed = 20;
        [SerializeField] private CharacterController _characterController;

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

        private void Update()
        {
            if (_wantedDirection.sqrMagnitude > 0f)
                _transform.forward = Vector3.Slerp(_transform.forward, _wantedDirection, _turnSpeed * Time.deltaTime);
            
            _characterController.Move(Direction * (_movementSpeed * Time.deltaTime));
        }
    }
}