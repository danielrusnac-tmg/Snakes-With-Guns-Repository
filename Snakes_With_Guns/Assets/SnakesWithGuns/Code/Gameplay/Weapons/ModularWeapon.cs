using System.Linq;
using UnityEngine;

namespace SnakesWithGuns.Gameplay.Weapons
{
    public class ModularWeapon : MonoBehaviour, IWeapon
    {
        [SerializeField] private GameObject[] _weaponObjects;

        private IWeapon[] _weapons;
        private bool _isFiring;

        public bool IsFiring
        {
            get => _isFiring;
            set
            {
                if (_isFiring == value)
                    return;
                
                _isFiring = value;
                ToggleIsFiring(value);
            }
        }

        public void Initialize(WeaponDefinition weaponDefinition)
        {
            _weapons = _weaponObjects.Select(o => o.GetComponent<IWeapon>()).ToArray();
            
            foreach (IWeapon weapon in _weapons)
                weapon.Initialize(weaponDefinition);
        }

        private void ToggleIsFiring(bool value)
        {
            foreach (IWeapon weapon in _weapons)
                weapon.IsFiring = value;
        }
    }
}