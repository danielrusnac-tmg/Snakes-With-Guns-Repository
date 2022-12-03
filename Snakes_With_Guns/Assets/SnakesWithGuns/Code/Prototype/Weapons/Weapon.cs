using System.Collections;
using UnityEngine;

namespace SnakesWithGuns.Prototype.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponDefinition _weaponDefinition;
        [SerializeField] private Transform _muzzlePoint;

        private bool _isFiring;
        private Coroutine _fireCoroutine;

        public bool IsFiring
        {
            get => _isFiring;
            set
            {
                if (_isFiring == value)
                    return;

                _isFiring = value;

                if (value)
                {
                    StartFiring();
                }
                else
                {
                    StopFiring();
                }
            }
        }

        private void StartFiring()
        {
            _fireCoroutine = StartCoroutine(FireRoutine());
        }

        private void StopFiring()
        {
            StopCoroutine(_fireCoroutine);
        }

        private void Fire()
        {
            Instantiate(_weaponDefinition.Projectile, _muzzlePoint.position, _muzzlePoint.rotation);
        }

        private IEnumerator FireRoutine()
        {
            while (true)
            {
                for (int i = 0; i < _weaponDefinition.MagazineSize; i++)
                {
                    Fire();
                    yield return new WaitForSeconds(_weaponDefinition.FireRate);
                }

                yield return new WaitForSeconds(_weaponDefinition.ReloadDuration);
            }
        }
    }
}