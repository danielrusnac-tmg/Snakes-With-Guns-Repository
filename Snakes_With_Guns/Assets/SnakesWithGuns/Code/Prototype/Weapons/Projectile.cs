using System;
using UnityEngine;

namespace SnakesWithGuns.Prototype.Weapons
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private bool _waitForParticleAnimation;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private ParticleSystem _particleSystem;
        
        [Header("Settings")]
        [SerializeField] private bool _destroyOnSlowDown = true;
        [SerializeField] private float _minVelocity = 1f;
        [SerializeField] private bool _alignToVelocity;

        public event Action<Projectile> Died;
        public event Action<ContactPoint> Collided;

        private void Awake()
        {
            ParticleSystem.MainModule main = _particleSystem.main;
            main.stopAction = ParticleSystemStopAction.Callback;
        }

        private void Update()
        {
            if (_destroyOnSlowDown && _rigidbody.velocity.sqrMagnitude < _minVelocity)
                SelfDestroy();
        }

        private void FixedUpdate()
        {
            if (_alignToVelocity && _rigidbody.velocity.sqrMagnitude > float.Epsilon)
                _rigidbody.MoveRotation(Quaternion.LookRotation(_rigidbody.velocity.normalized));
        }

        private void OnCollisionEnter(Collision collision)
        {
            Collided?.Invoke(collision.GetContact(0));
            SelfDestroy();
        }

        public void ApplyForce(float force, float drag)
        {
            _particleSystem.Play();
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            _rigidbody.AddForce(transform.forward * force, ForceMode.Impulse);
            _rigidbody.drag = drag;
        }

        private void OnParticleSystemStopped()
        {
            Died?.Invoke(this);
        }
        
        private void SelfDestroy()
        {
            if (_waitForParticleAnimation)
            {
                _particleSystem.Stop(true);
            }
            else
            {
                Died?.Invoke(this);
            }
        }
    }
}