using UnityEngine;

namespace SnakesWithGuns.Gameplay.Weapons
{
    public static class DamageUtility
    {
        private static readonly Collider[] DAMAGE_COLLIDERS = new Collider[40];

        public static void DealDamageInRadius(Vector3 point, float radius, int damage, int sourceID)
        {
            int targets = Physics.OverlapSphereNonAlloc(
                point,
                radius,
                DAMAGE_COLLIDERS);

            if (targets == 0)
                return;

            for (int i = 0; i < targets; i++)
            {
                if (DAMAGE_COLLIDERS[i].TryGetComponent(out IDamageable damageable) && damageable.SourceID != sourceID)
                    damageable.DealDamage(damage);
            }
        }
    }
}