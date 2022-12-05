using SnakesWithGuns.Prototype.Infrastructure.PubSub;
using SnakesWithGuns.Prototype.Messages;
using SnakesWithGuns.Prototype.Utilities.Audio;
using SnakesWithGuns.Prototype.Utilities.CameraShake;
using SnakesWithGuns.Prototype.Utilities.Vibrations;
using UnityEngine;

namespace SnakesWithGuns.Prototype
{
    public class Application : MonoBehaviour
    {
        public static Application Instance { get; private set; }

        [SerializeField] private CinemachineScreenShaker _screenShaker;
        [SerializeField] private AudioService _audioService;

        private IVibration _vibration;

        public IChannel<CameraShakeType> ScreenShakeChannel { get; private set; }
        public IChannel<VibrationType> VibrationChannel { get; private set; }
        public IChannel<PlaySfxMessage> SfxChannel { get; private set; }

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
            ScreenShakeChannel = new Channel<CameraShakeType>();
            VibrationChannel = new Channel<VibrationType>();
            SfxChannel = new Channel<PlaySfxMessage>();
        }

        private void RegisterChannels()
        {
            ScreenShakeChannel.Register(_screenShaker.Shake);
            // VibrationChannel.Register(_vibration.Play);
            // SfxChannel.Register(OnPlaySfx);
        }

        private void UnregisterChannels()
        {
            ScreenShakeChannel.Unregister(_screenShaker.Shake);
            // VibrationChannel.Unregister(_vibration.Play);
            // SfxChannel.Unregister(OnPlaySfx);
        }

        private void OnPlaySfx(PlaySfxMessage message) => _audioService.PlaySfx(message.Clip, message.Volume);
    }
}