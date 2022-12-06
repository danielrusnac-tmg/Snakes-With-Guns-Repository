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

        [SerializeField] private Transform _muzzlePoint;

        private WeaponDefinition _weaponDefinition;
        private ParticleSystem _muzzle;
        private bool _isFiring;
        private Coroutine _fireCoroutine;
        private IChannel<ScreenShakeMessage> _screenShakeChannel;

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

        public void Initialize(WeaponDefinition weaponDefinition)
        {
            _screenShakeChannel = Channels.GetChannel<ScreenShakeMessage>();
            _weaponDefinition = weaponDefinition;
            _muzzle = Instantiate(_weaponDefinition.MuzzleEffectPrefab, _muzzlePoint);

            if (!s_projectilePools.ContainsKey(_weaponDefinition.Projectile))
                s_projectilePools.Add(_weaponDefinition.Projectile, CreateProjectilePool());

            if (!s_impactEffectPools.ContainsKey(_weaponDefinition.ImpactEffectPrefab))
                s_impactEffectPools.Add(_weaponDefinition.ImpactEffectPrefab, CreateImpactEffectPool());
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
            SpawnProjectile(_muzzlePoint.position, _muzzlePoint.rotation * _weaponDefinition.GetRotationOffset());
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

        private void SpawnProjectile(Vector3 position, Quaternion rotation)
        {
            Projectile projectile = s_projectilePools[_weaponDefinition.Projectile].Get();
            projectile.transform.position = position;
            projectile.transform.rotation = rotation;
            projectile.ApplyForce(_weaponDefinition.GetForce(), _weaponDefinition.GetDrag());
        }

        private void OnProjectileCollided(ContactPoint point)
        {
            ParticleSystem effect = s_impactEffectPools[_weaponDefinition.ImpactEffectPrefab].Get();
            effect.transform.position = point.point;

            if (_weaponDefinition.AlignImpactToSurface)
                effect.transform.forward = point.normal;

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