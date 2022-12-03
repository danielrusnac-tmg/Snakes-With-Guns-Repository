using UnityEngine;

namespace SnakesWithGuns.Prototype.Weapons
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        private void Update()
        {
            if (_rigidbody.velocity.sqrMagnitude < 0.1f)
                SelfDestroy();
        }

        public void ApplyForce(float force, float drag)
        {
            _rigidbody.AddForce(transform.forward * force, ForceMode.Impulse);
            _rigidbody.drag = drag;
        }

        private void SelfDestroy()
        {
            Destroy(gameObject);
        }
    }
}