using System;
using System.Collections;
using System.Collections.Generic;
using SnakesWithGuns.Infrastructure.PubSub;
using SnakesWithGuns.Utilities;
using SnakesWithGuns.Utilities.CameraShake;
using UnityEngine;
using UnityEngine.Pool;

namespace SnakesWithGuns.Gameplay.Weapons
{
    public class Weapon : MonoBehaviour, IWeapon
    {
        private static Dictionary<Projectile, ObjectPool<Projectile>> s_projectilePools = new();
        private static Dictionary<ParticleSystem, ObjectPool<ParticleSystem>> s_impactEffectPools = new();
        private static Collider[] s_damageColliders = new Collider[10];

        [SerializeField] private Transform _muzzlePoint;

        private WeaponDefinition _weaponDefinition;
        private ParticleSystem _muzzle;
        private IChannel<ScreenShakeMessage> _screenShakeChannel;

        public int SourceID { get; set; }
        [field: SerializeField] public bool IsFiring { get; set; }

        public void Initialize(WeaponDefinition weaponDefinition)
        {
            _screenShakeChannel = Channels.GetChannel<ScreenShakeMessage>();
            _weaponDefinition = weaponDefinition;
            _muzzle = Instantiate(_weaponDefinition.MuzzleEffectPrefab, _muzzlePoint);

            if (!s_projectilePools.ContainsKey(_weaponDefinition.Projectile))
                s_projectilePools.Add(_weaponDefinition.Projectile, CreateProjectilePool());

            if (!s_impactEffectPools.ContainsKey(_weaponDefinition.ImpactEffectPrefab))
                s_impactEffectPools.Add(_weaponDefinition.ImpactEffectPrefab, CreateImpactEffectPool());

            StartCoroutine(FireRoutine());
        }

        private void OnDestroy()
        {
            s_projectilePools.Clear();
            s_impactEffectPools.Clear();
        }

        private void Fire()
        {
            _muzzle.Play();

            for (int j = 0; j < _weaponDefinition.ProjectilePerShot; j++)
                SpawnProjectile(_muzzlePoint.position, _muzzlePoint.rotation * _weaponDefinition.GetRotationOffset());
        }

        private IEnumerator FireRoutine()
        {
            WaitWhile waitIsFiring = new WaitWhile(() => !IsFiring);
            WaitForSeconds waitRate = new WaitForSeconds(_weaponDefinition.FireRate);
            WaitForSeconds waitReload = new WaitForSeconds(_weaponDefinition.ReloadDuration);

            while (true)
            {
                yield return waitIsFiring;

                for (int i = 0; i < _weaponDefinition.MagazineSize; i++)
                {
                    Fire();

                    yield return waitRate;

                    if (!IsFiring)
                        yield return waitIsFiring;
                }

                yield return waitReload;
            }
        }

        private void SpawnProjectile(Vector3 position, Quaternion rotation)
        {
            Projectile projectile = s_projectilePools[_weaponDefinition.Projectile].Get();
            projectile.transform.position = position;
            projectile.transform.rotation = rotation;
            projectile.ApplyForce(_weaponDefinition.GetForce(), _weaponDefinition.GetDrag());
        }

        private void OnProjectileCollided(HitData hitData)
        {
            DealProjectileDamage(hitData);
            SpawnProjectileCollisionEffect(hitData);
        }

        private void DealProjectileDamage(HitData hitData)
        {
            switch (_weaponDefinition.DamageDealMode)
            {
                case DamageDealMode.OnContact:
                {
                    hitData.Damageable.DealDamage(_weaponDefinition.Damage);
                }
                    break;
                case DamageDealMode.InRadius:
                {
                    DamageUtility.DealDamageInRadius(
                        hitData.Point, 
                        _weaponDefinition.DamageRadius, 
                        _weaponDefinition.Damage,
                        SourceID);
                }
                    break;
            }
        }

        private void SpawnProjectileCollisionEffect(HitData hitData)
        {
            ParticleSystem effect = s_impactEffectPools[_weaponDefinition.ImpactEffectPrefab].Get();
            effect.transform.position = hitData.Point;

            if (_weaponDefinition.AlignImpactToSurface)
                effect.transform.forward = hitData.Normal;

            effect.Play();
            _screenShakeChannel.Publish(new ScreenShakeMessage(_weaponDefinition.ImpactShake));
        }

        private void OnProjectileDied(Projectile projectile)
        {
            s_projectilePools[_weaponDefinition.Projectile].Release(projectile);
        }

        private void OnReleaseImpact(ParticleSystem effect)
        {
            s_impactEffectPools[_weaponDefinition.ImpactEffectPrefab].Release(effect);
        }

        private ObjectPool<ParticleSystem> CreateImpactEffectPool()
        {
            return new ObjectPool<ParticleSystem>(
                () =>
                {
                    ParticleSystem effect = Instantiate(_weaponDefinition.ImpactEffectPrefab);
                    PooledParticle pooledParticle = effect.gameObject.AddComponent<PooledParticle>();
                    pooledParticle.Construct(effect, OnReleaseImpact);
                    return effect;
                },
                null, null, effect => Destroy(effect.gameObject), false, 100);
        }

        private ObjectPool<Projectile> CreateProjectilePool()
        {
            return new ObjectPool<Projectile>(
                () =>
                {
                    Projectile projectile = Instantiate(_weaponDefinition.Projectile);
                    projectile.Died += OnProjectileDied;
                    projectile.Collided += OnProjectileCollided;
                    return projectile;
                },
                projectile => projectile.gameObject.SetActive(true),
                projectile => projectile.gameObject.SetActive(false),
                projectile =>
                {
                    projectile.Died -= OnProjectileDied;
                    projectile.Collided -= OnProjectileCollided;
                    Destroy(projectile);
                }, false, 100);
        }
    }
}