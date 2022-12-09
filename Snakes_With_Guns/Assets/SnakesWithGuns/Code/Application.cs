using DG.Tweening;
using SnakesWithGuns.Gameplay;
using SnakesWithGuns.Gameplay.Messages;
using SnakesWithGuns.Infrastructure;
using SnakesWithGuns.Infrastructure.PubSub;
using SnakesWithGuns.Utilities.CameraShake;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SnakesWithGuns
{
    public class Application : SceneController
    {
        [SerializeField] private CinemachineScreenShaker _screenShaker;

        private PauseService _pauseService;
        private IChannel<ScreenShakeMessage> _screenShakeChannel;
        private IChannel<PauseMessage> _pauseChannel;

        public void Restart()
        {
            _pauseService.IsPaused = false;
            DOTween.KillAll();
            SceneManager.LoadScene(0);
        }

        protected override void OnInitialize()
        {
            InitializeServices();
            CreateChannels();
            RegisterChannels();
        }

        protected override void OnCleanup()
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