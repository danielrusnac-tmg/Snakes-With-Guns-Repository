using UnityEngine;

namespace SnakesWithGuns.Prototype.Weapons
{
    [CreateAssetMenu(menuName = "Snakes With Guns/Weapon Definition", fileName = "weapon_")]
    public class WeaponDefinition : ScriptableObject
    {
        [SerializeField] private float _fireRate = 0.2f;
        [SerializeField] private float _reloadDuration = 1f;
        [SerializeField] private int _magazineSize = 30;
        [SerializeField] private Projectile _projectile;

        public float FireRate => _fireRate;
        public float ReloadDuration => _reloadDuration;
        public int MagazineSize => _magazineSize;
        public Projectile Projectile => _projectile;
    }
}