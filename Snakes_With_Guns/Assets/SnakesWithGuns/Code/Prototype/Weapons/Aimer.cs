using System.Collections.Generic;
using UnityEngine;

namespace SnakesWithGuns.Prototype.Weapons
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(SphereCollider))]
    public class Aimer : MonoBehaviour
    {
        [SerializeField] private SphereCollider _sphereCollider;

        private Transform _transform;
        private List<IDamageable> _targets = new();

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
            if (!other.TryGetComponent(out IDamageable damageable))
                return;

            _targets.Add(damageable);
            HasTarget = true;
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out IDamageable damageable))
                return;

            _targets.Remove(damageable);
            HasTarget = _targets.Count > 0;
        }

        private Vector3 GetClosestTarget()
        {
            if (!HasTarget)
                return Vector3.zero;

            float minDistance = float.MaxValue;
            IDamageable closestTarget = null;

            foreach (IDamageable target in _targets)
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