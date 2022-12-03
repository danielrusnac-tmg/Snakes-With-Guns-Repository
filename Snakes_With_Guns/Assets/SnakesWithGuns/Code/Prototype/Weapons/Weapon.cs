using System.Collections;
using System.Collections.Generic;
using SnakesWithGuns.Prototype.Infrastructure;
using UnityEngine;

namespace SnakesWithGuns.Prototype.Weapons
{
    public class Weapon : MonoBehaviour
    {
        private static Dictionary<Projectile, Pool<Projectile>> s_projectilePools = new();

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
            Projectile projectile = GetProjectileFromPool();
            projectile.transform.position = _muzzlePoint.position;
            projectile.transform.rotation = _muzzlePoint.rotation * _weaponDefinition.GetRotationOffset();
            projectile.gameObject.SetActive(true);
            projectile.ApplyForce(_weaponDefinition.GetForce(), _weaponDefinition.GetDrag());
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

        private Projectile GetProjectileFromPool()
        {
            if (!s_projectilePools.ContainsKey(_weaponDefinition.Projectile))
                s_projectilePools.Add(_weaponDefinition.Projectile, new Pool<Projectile>(() =>
                {
                    Projectile instance = Instantiate(_weaponDefinition.Projectile);
                    instance.Died += ReturnProjectileToPool;
                    instance.ImpactEffectPrefab = _weaponDefinition.ImpactEffectPrefab;
                    return instance;
                }, 50));

            return s_projectilePools[_weaponDefinition.Projectile].GetInstance();
        }

        private void ReturnProjectileToPool(Projectile projectile)
        {
            projectile.gameObject.SetActive(false);
            s_projectilePools[_weaponDefinition.Projectile].ReturnInstance(projectile);
        }
    }
}