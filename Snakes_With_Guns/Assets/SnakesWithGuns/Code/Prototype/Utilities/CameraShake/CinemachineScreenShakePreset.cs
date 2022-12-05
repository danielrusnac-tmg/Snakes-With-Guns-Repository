using Cinemachine;
using UnityEngine;

namespace SnakesWithGuns.Prototype.Utilities.CameraShake
{
    public class CinemachineScreenShakePreset : MonoBehaviour
    {
        [SerializeField] private CameraShakeType _shakeType;
        [SerializeField] private CinemachineImpulseSource _impulseSource;

        public CameraShakeType ShakeType => _shakeType;

        public void GenerateImpulse()
        {
            _impulseSource.GenerateImpulse();            
        }
    }
}