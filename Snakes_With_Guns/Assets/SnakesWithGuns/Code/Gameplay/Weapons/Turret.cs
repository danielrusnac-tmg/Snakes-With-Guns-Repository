using UnityEngine;

namespace SnakesWithGuns.Gameplay.Weapons
{
    public class Turret : MonoBehaviour
    {
        public WeaponDefinition WeaponDefinition;

        [SerializeField] private float _turnDamp = 0.4f;
        [SerializeField] private float _aimTolerance = 0.2f;
        [SerializeField] private Transform _rotationPivot;
        [SerializeField] private Aimer _aimer;

        private Vector3 _targetDirection;
        private Vector3 _currentDirection;
        private Vector3 _turnVelocity;
        private Transform _transform;
        private IWeapon _weapon;

        private void Awake()
        {
            _transform = transform;
            _weapon = GetComponent<IWeapon>();
            Initialize(WeaponDefinition);
        }

        private void OnEnable()
        {
            _weapon.IsFiring = true;
            _currentDirection = _rotationPivot.forward;
        }

        private void OnDisable()
        {
            _weapon.IsFiring = false;
        }

        private void Update()
        {
            if (_aimer.HasTarget)
            {
                CalculateTargetDirection();
                _weapon.IsFiring = IsAimingAtTarget();
            }
            else
            {
                _targetDirection = _transform.forward;
                _weapon.IsFiring = false;
            }

            RotateTowardsTargetDirection();
        }

        public void Initialize(WeaponDefinition weaponDefinition)
        {
            _aimer.Radius = weaponDefinition.TurretRadius;
            _weapon.Initialize(weaponDefinition);
        }

        private bool IsAimingAtTarget()
        {
            float dot = Vector3.Dot(_rotationPivot.forward, _targetDirection);
            return _aimTolerance > Mathf.InverseLerp(1f, -1f, dot);
        }

        private void CalculateTargetDirection()
        {
            _targetDirection = _aimer.Target - _rotationPivot.position;
            _targetDirection.y = 0f;
            _targetDirection.Normalize();
        }

        private void RotateTowardsTargetDirection()
        {
            _currentDirection = Vector3
                .SmoothDamp(_currentDirection, _targetDirection, ref _turnVelocity, _turnDamp).normalized;

            _rotationPivot.forward = _currentDirection;
        }

        private void OnDrawGizmosSelected()
        {
            if (_aimer == null || !_aimer.HasTarget)
                return;

            Gizmos.color = IsAimingAtTarget() ? Color.red : Color.blue;
            Gizmos.DrawLine(_rotationPivot.position, _aimer.Target);
        }
    }
}