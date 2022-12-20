using System;
using System.Collections;
using UnityEngine;

namespace SnakesWithGuns.Gameplay.Weapons
{
    public class TestWeaponSpawner : MonoBehaviour
    {
        [SerializeField] private int _weaponCount = 3;
        [SerializeField] private float _offset = 0.5f;
        [SerializeField] private float _delay = 0.01f;
        [SerializeField] private Weapon _weaponPrefab;
        [SerializeField] private WeaponDefinition[] _definitions;

        private IEnumerator Start()
        {
            float totalDistance = _weaponCount * _offset;
            float halfDistance = totalDistance / 2;

            for (int i = 0; i < _weaponCount; i++)
            {
                float t = (float) i / _weaponCount;
                float localX = Mathf.Lerp(-halfDistance, halfDistance, t);
                Weapon weapon = Instantiate(_weaponPrefab, transform.TransformPoint(new Vector3(localX, 0f, 0f)), transform.rotation, transform);
                weapon.Initialize(_definitions[(int) Mathf.Repeat(i, _definitions.Length)]);
                weapon.IsFiring = true;

                yield return new WaitForSeconds(_delay);
            }
        }

        private void OnDrawGizmos()
        {
            Vector3 offset = new Vector3(_weaponCount * _offset / 2, 0f, 0f);
            Gizmos.DrawLine(transform.position - offset, transform.position + offset);
        }
    }
}