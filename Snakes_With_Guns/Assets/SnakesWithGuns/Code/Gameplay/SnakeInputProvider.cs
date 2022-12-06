using SnakesWithGuns.Gameplay.Snakes;
using SnakesWithGuns.Prototype.Input;
using UnityEngine;

namespace SnakesWithGuns.Gameplay
{
    public class SnakeInputProvider : MonoBehaviour, ISnakeInputProvider
    {
        [SerializeField] private float _rotationOffset = 45f;

        private Quaternion _rotationOffsetQuaternion;
        private InputActions _inputActions;

        public Vector3 Direction => GetWorldInputDirection();

        private void Awake()
        {
            _rotationOffsetQuaternion = Quaternion.AngleAxis(_rotationOffset, Vector3.up);
            _inputActions = new InputActions();
        }

        private void OnEnable()
        {
            _inputActions.Gameplay.Enable();
        }

        private void OnDisable()
        {
            _inputActions.Gameplay.Disable();
        }

        private void OnDestroy()
        {
            _inputActions.Dispose();
        }

        private Vector3 GetWorldInputDirection()
        {
            Vector2 input = _inputActions.Gameplay.Move.ReadValue<Vector2>();
            Vector3 worldInput = Vector3.ClampMagnitude(new Vector3(input.x, 0f, input.y), 1f);
            worldInput = _rotationOffsetQuaternion * worldInput;

            return worldInput;
        }
    }
}