using System;
using UnityEditor.SearchService;
using UnityEngine;

namespace SnakesWithGuns.Gameplay.Weapons
{
    public struct HitData
    {
        public Vector3 Point;
        public Vector3 Normal;
        public IDamageable Damageable;
    }
    
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private bool _waitForParticleAnimation;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private ParticleSystem _particleSystem;
        [SerializeField] private LayerMask _hitMask;

        [Header("Settings")]
        [SerializeField] private bool _destroyOnSlowDown = true;
        [SerializeField] private float _minVelocity = 1f;
        [SerializeField] private bool _alignToVelocity;

        public event Action<Projectile> Died;
        public event Action<HitData> Collided;

        private bool _hasCollided;
        private bool _isDead;
        private Ray _ray;
        private RaycastHit[] _hits = new RaycastHit[1];
        
        public int SourceID { get; set; }
        
        private void Awake()
        {
            ParticleSystem.MainModule main = _particleSystem.main;
            main.stopAction = ParticleSystemStopAction.Callback;
        }

        private void FixedUpdate()
        {
            float velocity = _rigidbody.velocity.magnitude;
            Vector3 direction = _rigidbody.velocity.normalized;
            
            DetectHit(direction, velocity);
            AlignToVelocity(velocity, direction);
            DestroyOnSlowdown(velocity);
        }

        private void DetectHit(Vector3 direction, float velocity)
        {
            _ray.origin = _rigidbody.position;
            _ray.direction = direction;

            if (Physics.RaycastNonAlloc(_ray, _hits, velocity * Time.fixedDeltaTime, _hitMask) > 0)
            {
                if (_hasCollided)
                    return;

                if (!_hits[0].collider.TryGetComponent(out IDamageable damageable) || damageable.SourceID == SourceID)
                    return;
                
                _hasCollided = true;
                Collided?.Invoke(new HitData
                {
                    Point = _hits[0].point,
                    Normal = _hits[0].normal,
                    Damageable = damageable,
                });
                SelfDestroy();
            }
        }

        private void DestroyOnSlowdown(float velocitySqrMagnitude)
        {
            if (_destroyOnSlowDown && velocitySqrMagnitude < _minVelocity)
                SelfDestroy();
        }

        private void AlignToVelocity(float velocitySqrMagnitude, Vector3 direction)
        {
            if (_alignToVelocity && velocitySqrMagnitude > float.Epsilon)
                _rigidbody.MoveRotation(Quaternion.LookRotation(direction));
        }

        public void ApplyForce(float force, float drag)
        {
            _hasCollided = false;
            _isDead = false;
            
            _particleSystem.Play();
            _rigidbody.velocity = transform.forward * force;
            _rigidbody.angularVelocity = Vector3.zero;
            _rigidbody.drag = drag;
        }

        private void OnParticleSystemStopped()
        {
            if (_waitForParticleAnimation)
                OnDied();
        }

        private void SelfDestroy()
        {
            if (_waitForParticleAnimation)
            {
                _particleSystem.Stop(true);
            }
            else
            {
               OnDied();
            }
        }

        private void OnDied()
        {
            if (_isDead)
                return;

            _isDead = true;
            Died?.Invoke(this);
        }
    }
}