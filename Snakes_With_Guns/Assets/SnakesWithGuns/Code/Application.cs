using SnakesWithGuns.Infrastructure.PubSub;
using SnakesWithGuns.Utilities.CameraShake;
using SnakesWithGuns.Utilities.Vibrations;
using UnityEngine;

namespace SnakesWithGuns
{
    public class Application : MonoBehaviour
    {
        public static Application Instance { get; private set; }

        [SerializeField] private CinemachineScreenShaker _screenShaker;

        private IVibration _vibration;
        private IChannel<ScreenShakeMessage> _screenShakeChannel;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            InitializeServices();
            CreateChannels();
            RegisterChannels();
        }

        private void OnDestroy()
        {
            UnregisterChannels();
        }

        private void InitializeServices()
        {
            UnityEngine.Application.targetFrameRate = 60;
            _vibration = new NiceVibrationWithPresets();
        }

        private void CreateChannels()
        {
            _screenShakeChannel = Channels.GetChannel<ScreenShakeMessage>();
        }

        private void RegisterChannels()
        {
            _screenShakeChannel.Register(_screenShaker.Shake);
        }

        private void UnregisterChannels()
        {
            _screenShakeChannel.Unregister(_screenShaker.Shake);
        }
    }
}