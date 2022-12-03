using UnityEngine;

namespace SnakesWithGuns.Prototype.Weapons
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private float _turnDamp = 0.4f;
        [SerializeField] private Weapon _weapon;
        [SerializeField] private Transform _rotationPivot;
        [SerializeField] private Transform _target;

        private Vector3 _targetDirection;
        private Vector3 _turnVelocity;

        private void OnEnable()
        {
            _weapon.IsFiring = true;
        }

        private void OnDisable()
        {
            _weapon.IsFiring = false;
        }

        private void Update()
        {
            CalculateTargetDirection();
            RotateTowardsTargetDirection();
        }

        private void CalculateTargetDirection()
        {
            if (_target == null)
                return;

            _targetDirection = _rotationPivot.position - _target.position;
            _targetDirection.y = 0f;
            _targetDirection.Normalize();
        }

        private void RotateTowardsTargetDirection()
        {
            _rotationPivot.forward = Vector3
                .SmoothDamp(_rotationPivot.forward, _targetDirection, ref _turnVelocity, _turnDamp).normalized;
        }
    }
}