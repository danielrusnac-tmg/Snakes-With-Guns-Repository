using System;
using UnityEngine;

namespace SnakesWithGuns.Prototype.Weapons
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private ParticleSystem _particleSystem;

        public event Action<Projectile> Died;
        public event Action<ContactPoint> Collided;

        private void Update()
        {
            if (_rigidbody.velocity.sqrMagnitude < 1f)
                SelfDestroy();
        }

        private void OnCollisionEnter(Collision collision)
        {
            Collided?.Invoke(collision.GetContact(0));
            SelfDestroy();
        }

        public void ApplyForce(float force, float drag)
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            _rigidbody.AddForce(transform.forward * force, ForceMode.Impulse);
            _rigidbody.drag = drag;
        }

        private void SelfDestroy()
        {
            _particleSystem.Stop(true);
            Died?.Invoke(this);
        }
    }
}