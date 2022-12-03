using UnityEngine;

namespace SnakesWithGuns.Prototype.Weapons
{
    public class Projectile : MonoBehaviour
    {
        public ParticleSystem ImpactEffectPrefab;

        [SerializeField] private Rigidbody _rigidbody;

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
            _rigidbody.AddForce(transform.forward * force, ForceMode.Impulse);
            _rigidbody.drag = drag;
        }

        private void SpawnImpactEffect(Collision collision)
        {
            ContactPoint point = collision.contacts[0];
            Instantiate(ImpactEffectPrefab, point.point, Quaternion.LookRotation(point.normal));
        }

        private void SelfDestroy()
        {
            Destroy(gameObject);
        }
    }
}