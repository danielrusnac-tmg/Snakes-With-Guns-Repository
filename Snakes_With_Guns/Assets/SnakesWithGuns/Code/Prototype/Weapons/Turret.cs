using UnityEngine;

namespace SnakesWithGuns.Prototype.Weapons
{
    public class Turret : MonoBehaviour
    {
        [SerializeField] private float _turnDamp = 0.4f;
        [SerializeField] private float _aimTolerance = 0.2f;
        [SerializeField] private Weapon _weapon;
        [SerializeField] private Transform _rotationPivot;
        [SerializeField] private Aimer _aimer;

        private Vector3 _targetDirection;
        private Vector3 _turnVelocity;

        private void OnEnable()
        {
            _weapon.IsFiring = true;
            _rotationPivot.localEulerAngles = new Vector3(0, Random.Range(0, 360), 0);
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
                _weapon.IsFiring = false;
            }

            RotateTowardsTargetDirection();
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
            _rotationPivot.forward = Vector3
                .SmoothDamp(_rotationPivot.forward, _targetDirection, ref _turnVelocity, _turnDamp).normalized;
        }

        private void OnDrawGizmosSelected()
        {
            if (_aimer == null || !_aimer.HasTarget)
                return;

            Gizmos.color = IsAimingAtTarget() ? Color.red : Color.blue;
            Gizmos.DrawLine(_weapon.transform.position, _aimer.Target);
        }
    }
}