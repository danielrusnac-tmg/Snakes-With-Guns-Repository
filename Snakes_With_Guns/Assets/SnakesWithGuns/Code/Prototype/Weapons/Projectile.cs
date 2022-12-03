using System;
using UnityEngine;

namespace SnakesWithGuns.Prototype.Weapons
{
    public class Projectile : MonoBehaviour
    {
        public ParticleSystem ImpactEffectPrefab;

        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private ParticleSystem _particleSystem;

        public event Action<Projectile> Died;

        private void Update()
        {
            if (_rigidbody.velocity.sqrMagnitude < 1f)
                SelfDestroy();
        }

        private void OnCollisionEnter(Collision collision)
        {
            SpawnImpactEffect(collision);
            SelfDestroy();
        }
        
        public void ApplyForce(float force, float drag)
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            _rigidbody.AddForce(transform.forward * force, ForceMode.Impulse);
            _rigidbody.drag = drag;
        }

        private void SpawnImpactEffect(Collision collision)
        {
            ContactPoint point = collision.contacts[0];
            ParticleSystem impactEffect = Instantiate(ImpactEffectPrefab, point.point, Quaternion.LookRotation(point.normal));
            Destroy(impactEffect.gameObject, 3f);
        }

        private void SelfDestroy()
        {
            _particleSystem.Clear(true);
            Died?.Invoke(this);
        }
    }
}