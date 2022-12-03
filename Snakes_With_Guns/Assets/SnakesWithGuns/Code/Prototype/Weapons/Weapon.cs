using System.Collections;
using UnityEngine;

namespace SnakesWithGuns.Prototype.Weapons
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private WeaponDefinition _weaponDefinition;
        [SerializeField] private Transform _muzzlePoint;

        private ParticleSystem _muzzle;
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

        private void Awake()
        {
            _muzzle = Instantiate(_weaponDefinition.MuzzleEffectPrefab, _muzzlePoint);
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
            _muzzle.Play();
            Projectile projectile = Instantiate(
                _weaponDefinition.Projectile, 
                _muzzlePoint.position, 
                _muzzlePoint.rotation * _weaponDefinition.GetRotationOffset());
            projectile.ApplyForce(_weaponDefinition.GetForce(), _weaponDefinition.GetDrag());
            projectile.ImpactEffectPrefab = _weaponDefinition.ImpactEffectPrefab;
        }

        private IEnumerator FireRoutine()
        {
            yield return null;
            
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