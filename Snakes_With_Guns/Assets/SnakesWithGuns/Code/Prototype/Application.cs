using SnakesWithGuns.Prototype.Infrastructure.PubSub;
using SnakesWithGuns.Prototype.Utilities.CameraShake;
using UnityEngine;

namespace SnakesWithGuns.Prototype
{
    public class Application : MonoBehaviour
    {
        [SerializeField] private CinemachineScreenShaker _screenShaker;

        public IChannel<CameraShakeType> ScreenShakeChannel { get; private set; }
        
        public static Application Instance { get; private set; } 

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            ScreenShakeChannel = new Channel<CameraShakeType>();
            ScreenShakeChannel.Register(OnScreenShakeEvent);
        }

        private void OnDestroy()
        {
            ScreenShakeChannel.Unregister(OnScreenShakeEvent);
        }

        private void Start()
        {
            UnityEngine.Application.targetFrameRate = 60;
        }

        private void OnScreenShakeEvent(CameraShakeType shakeType)
        {
            _screenShaker.Shake(shakeType);
        }
    }
}