using UnityEngine;

namespace SnakesWithGuns.Prototype.Weapons
{
    public class Dummy : MonoBehaviour, IDamageable
    {
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        public Vector3 Position => _transform.position;
    }
}