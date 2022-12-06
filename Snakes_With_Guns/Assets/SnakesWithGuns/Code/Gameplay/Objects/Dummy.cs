using System;
using SnakesWithGuns.Gameplay.Weapons;
using UnityEngine;

namespace SnakesWithGuns.Gameplay.Objects
{
    [RequireComponent(typeof(Health))]
    public class Dummy : MonoBehaviour, ITarget
    {
        public event Action<ITarget> Died;
        
        [SerializeField] private Health _health;

        private Transform _transform;

        public Vector3 Position => _transform.position;

        private void Reset()
        {
            _health = GetComponent<Health>();
        }

        private void Awake()
        {
            _transform = transform;
            _health.Died += OnDied;
        }

        private void OnDestroy()
        {
            _health.Died -= OnDied;
        }

        private void OnDied()
        {
            Died?.Invoke(this);
            Destroy(gameObject);
        }
    }
}