using SnakesWithGuns.Prototype.Infrastructure.PubSub;
using SnakesWithGuns.Prototype.Utilities.CameraShake;
using SnakesWithGuns.Prototype.Utilities.Vibrations;
using UnityEngine;

namespace SnakesWithGuns.Prototype
{
    public class Application : MonoBehaviour
    {
        public static Application Instance { get; private set; }

        [SerializeField] private CinemachineScreenShaker _screenShaker;

        private IVibration _vibration;

        public IChannel<CameraShakeType> ScreenShakeChannel { get; private set; }
        public IChannel<VibrationType> VibrationChannel { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            InitializeServices();
            CreateChannels();
            RegisterChannels();

            Instance = this;
        }

        private void Start()
        {
            UnityEngine.Application.targetFrameRate = 60;
        }

        private void OnDestroy()
        {
            UnregisterChannels();
        }

        private void InitializeServices()
        {
            _vibration = new NiceVibrationCustom();
        }

        private void CreateChannels()
        {
            ScreenShakeChannel = new Channel<CameraShakeType>();
            VibrationChannel = new Channel<VibrationType>();
        }

        private void RegisterChannels()
        {
            ScreenShakeChannel.Register(_screenShaker.Shake);
            VibrationChannel.Register(_vibration.Play);
        }

        private void UnregisterChannels()
        {
            ScreenShakeChannel.Unregister(_screenShaker.Shake);
            VibrationChannel.Unregister(_vibration.Play);
        }
    }
}