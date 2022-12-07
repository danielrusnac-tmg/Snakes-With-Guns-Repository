using System;
using UnityEngine;

namespace SnakesWithGuns.Gameplay.Weapons
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private bool _waitForParticleAnimation;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Collider _collider;
        [SerializeField] private ParticleSystem _particleSystem;
        
        [Header("Settings")]
        [SerializeField] private bool _destroyOnSlowDown = true;
        [SerializeField] private float _minVelocity = 1f;
        [SerializeField] private bool _alignToVelocity;

        public event Action<Projectile> Died;
        public event Action<Collision> Collided;

        private bool _hasCollided;
        private bool _isDead;
        
        private void Awake()
        {
            ParticleSystem.MainModule main = _particleSystem.main;
            main.stopAction = ParticleSystemStopAction.Callback;
        }

        private void FixedUpdate()
        {
            if (_alignToVelocity && _rigidbody.velocity.sqrMagnitude > float.Epsilon)
                _rigidbody.MoveRotation(Quaternion.LookRotation(_rigidbody.velocity.normalized));
            
            if (_destroyOnSlowDown && _rigidbody.velocity.sqrMagnitude < _minVelocity)
                SelfDestroy();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (_hasCollided)
                return;

            _hasCollided = true;
            Collided?.Invoke(collision);
            SelfDestroy();
        }

        public void ApplyForce(float force, float drag)
        {
            _hasCollided = false;
            _isDead = false;
            
            _collider.enabled = true;
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
            _collider.enabled = false;
            
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