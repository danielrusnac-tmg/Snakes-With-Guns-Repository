using SnakesWithGuns.Gameplay.Objects;
using UnityEngine;

namespace SnakesWithGuns.Gameplay.Snakes
{
    public class Segment : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Transform _modulePoint;
        [SerializeField] private Health _health;

        private SegmentModuleComponent _moduleInstance;

        public Actor ParentActor { get; set; }
        public SegmentModule ActiveModule { get; private set; }
        
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

        public void InstallModule(SegmentModule module)
        {
            if (ActiveModule != null)
                UninstallModule();

            _health.SourceID = ParentActor.SourceID;
            ActiveModule = module;
            _moduleInstance = Instantiate(module.ModulePrefab, _modulePoint);
            _moduleInstance.ParentActor = ParentActor;
            _moduleInstance.OnInstall();
        }

        public void SetVelocity(Vector3 velocity)
        {
            _rigidbody.velocity = velocity;
        }

        public void UninstallModule()
        {
            if (ActiveModule == null)
                return;

            ActiveModule = null;
            Destroy(_moduleInstance.gameObject);
        }
    }
}