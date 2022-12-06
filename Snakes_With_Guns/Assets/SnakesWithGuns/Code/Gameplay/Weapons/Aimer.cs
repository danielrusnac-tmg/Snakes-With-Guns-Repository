using System.Collections.Generic;
using UnityEngine;

namespace SnakesWithGuns.Gameplay.Weapons
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SphereCollider))]
    public class Aimer : MonoBehaviour
    {
        [SerializeField] private SphereCollider _sphereCollider;

        private Transform _transform;
        private List<ITarget> _targets = new();

        public float Radius
        {
            get => _sphereCollider.radius;
            set => _sphereCollider.radius = value;
        }

        public Vector3 Target => GetClosestTarget();

        public bool HasTarget { get; private set; }

        private void Reset()
        {
            _sphereCollider = GetComponent<SphereCollider>();
        }

        private void Awake()
        {
            _transform = transform;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.TryGetComponent(out ITarget damageable))
                return;

            AddTarget(damageable);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out ITarget damageable))
                return;

            RemoveTarget(damageable);
        }

        private void AddTarget(ITarget target)
        {
            _targets.Add(target);
            HasTarget = true;
            target.Died += RemoveTarget;
        }

        private void RemoveTarget(ITarget target)
        {
            _targets.Remove(target);
            HasTarget = _targets.Count > 0;
            target.Died -= RemoveTarget;
        }

        private Vector3 GetClosestTarget()
        {
            if (!HasTarget)
                return Vector3.zero;

            float minDistance = float.MaxValue;
            ITarget closestTarget = null;

            foreach (ITarget target in _targets)
            {
                float distance = Vector3.Distance(target.Position, _transform.position);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    closestTarget = target;
                }
            }

            return closestTarget.Position;
        }
    }
}