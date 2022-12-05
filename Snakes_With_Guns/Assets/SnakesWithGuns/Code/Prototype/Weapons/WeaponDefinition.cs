﻿using SnakesWithGuns.Prototype.Utilities.CameraShake;
using SnakesWithGuns.Prototype.Utilities.Vibrations;
using UnityEngine;

namespace SnakesWithGuns.Prototype.Weapons
{
    [CreateAssetMenu(menuName = "Snakes With Guns/Weapon Definition", fileName = "weapon_")]
    public class WeaponDefinition : ScriptableObject
    {
        [Header("Stats")]
        [SerializeField] private float _fireRate = 0.2f;
        [SerializeField] private float _reloadDuration = 1f;
        [SerializeField] private int _magazineSize = 30;

        [Header("Projectile")]
        [SerializeField] private float _force = 40;
        [SerializeField] private float _drag = 3;
        [SerializeField] private float _rotationOffset = 1f;
        [Range(0f, 1f)]
        [SerializeField] private float _randomness = 0.2f;
        [SerializeField] private bool _alignImpactToSurface = true;

        [Header("Effects")]
        [SerializeField] private CameraShakeType _impactShake;
        [SerializeField] private VibrationType _impactVibration;
        [SerializeField] private Projectile _projectile;
        [SerializeField] private ParticleSystem _muzzleEffectPrefab;
        [SerializeField] private ParticleSystem _impactEffectPrefab;
        
        [Header("Audio")]
        [SerializeField] private AudioClip _fire;
        [SerializeField] private AudioClip _impact;

        [Header("Turret")]
        [SerializeField] private float _radius = 10f;
        [SerializeField] private Turret _turretPrefab;

        public float FireRate => _fireRate;
        public float ReloadDuration => _reloadDuration;
        public int MagazineSize => _magazineSize;
        public bool AlignImpactToSurface => _alignImpactToSurface;
        public CameraShakeType ImpactShake => _impactShake;
        public VibrationType ImpactVibration => _impactVibration;
        public Projectile Projectile => _projectile;
        public ParticleSystem MuzzleEffectPrefab => _muzzleEffectPrefab;
        public ParticleSystem ImpactEffectPrefab => _impactEffectPrefab;
        public AudioClip Fire => _fire;
        public AudioClip Impact => _impact;
        public float Radius => _radius;
        public Turret TurretPrefab => _turretPrefab;

        public float GetForce()
        {
            float offset = _randomness * _force;
            return _force + Random.Range(-offset, offset);
        }

        public float GetDrag()
        {
            return _drag;
        }

        public Quaternion GetRotationOffset()
        {
            return Quaternion.AngleAxis(Random.Range(-_rotationOffset, _rotationOffset), Vector3.up);
        }
    }
}