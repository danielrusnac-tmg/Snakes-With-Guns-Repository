using UnityEngine;

namespace SnakesWithGuns.Prototype.Snakes
{
    public class Segment : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Transform _spawnPoint;
        
        private ISegmentModule _module;
        private GameObject _moduleObject;

        public ISegmentModule Module => _module;
        public bool HasModule => _module != null;

        public Vector3 Position
        {
            get => _rigidbody.position;
            set => _rigidbody.MovePosition(value);
        }

        public Quaternion Rotation
        {
            get => _rigidbody.rotation;
            set => _rigidbody.MoveRotation(value);
        }

        public void InstallModule(ISegmentModule module)
        {
            if (HasModule)
                UninstallModule();
            
            _module = module;
            _moduleObject = Instantiate(module.ModulePrefab, _spawnPoint);
        }

        public bool UninstallModule()
        {
            if (!HasModule)
                return false;
            
            _module = null;
            Destroy(_moduleObject);
            return true;
        }
    }
}