using SnakesWithGuns.Gameplay;
using SnakesWithGuns.Gameplay.Messages;
using SnakesWithGuns.Infrastructure.PubSub;
using SnakesWithGuns.Utilities.CameraShake;
using UnityEngine;

namespace SnakesWithGuns
{
    public class Application : MonoBehaviour
    {
        [SerializeField] private CinemachineScreenShaker _screenShaker;

        private PauseService _pauseService;
        private IChannel<ScreenShakeMessage> _screenShakeChannel;
        private IChannel<PauseMessage> _pauseChannel;

        private void Awake()
        {
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
            _pauseService = new PauseService();
        }

        private void CreateChannels()
        {
            _screenShakeChannel = Channels.GetChannel<ScreenShakeMessage>();
            _pauseChannel = Channels.GetChannel<PauseMessage>();
        }

        private void RegisterChannels()
        {
            _screenShakeChannel.Register(OnShakeMessage);
            _pauseChannel.Register(OnPauseMessage);
        }

        private void UnregisterChannels()
        {
            _screenShakeChannel.Unregister(OnShakeMessage);
            _pauseChannel.Unregister(OnPauseMessage);
        }

        private void OnPauseMessage(PauseMessage message) => _pauseService.IsPaused = message.IsPaused;

        private void OnShakeMessage(ScreenShakeMessage message) => _screenShaker.Shake(message.ShakeType);
    }
}