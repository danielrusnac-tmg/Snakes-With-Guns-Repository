using SnakesWithGuns.Gameplay.Snakes;
using SnakesWithGuns.Gameplay.Weapons;
using UnityEngine;

namespace SnakesWithGuns.Gameplay
{
    public class SegmentInstaller : MonoBehaviour
    {
        [SerializeField] private Tail _tail;
        [SerializeField] private WeaponDefinition _weaponDefinition;
        [SerializeField] private int _targetSegment;

        [ContextMenu(nameof(Add))]
        private void Add()
        {
            _tail.AddSegment();
        }

        [ContextMenu(nameof(Remove))]
        private void Remove()
        {
            _tail.RemoveSegment();
        }

        [ContextMenu(nameof(Install))]
        private void Install()
        {
            Turret turret = Instantiate(_weaponDefinition.TurretPrefab, _tail.Segments[_targetSegment].ModulePoint);
            turret.Initialize(_weaponDefinition);
        }

        [ContextMenu(nameof(Uninstall))]
        private void Uninstall()
        {
            
        }
    }
}