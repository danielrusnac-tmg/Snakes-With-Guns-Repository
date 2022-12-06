using SnakesWithGuns.Gameplay.Messages;
using SnakesWithGuns.Infrastructure.PubSub;
using UnityEngine;

namespace SnakesWithGuns.Gameplay
{
    public class PauseService : MonoBehaviour
    {
        private bool _isPaused;
        private IChannel<PauseMessage> _pauseChannel;

        private bool IsPaused
        {
            get => _isPaused;
            set
            {
                if (_isPaused == value)
                    return;

                _isPaused = value;
                Time.timeScale = value ? 0f : 1f;
            }
        }

        private void Awake()
        {
            _pauseChannel = Channels.GetChannel<PauseMessage>();
            _pauseChannel.Register(OnPauseMessage);
        }

        private void OnDestroy()
        {
            _pauseChannel.Unregister(OnPauseMessage);
        }

        private void OnPauseMessage(PauseMessage message)
        {
            IsPaused = message.IsPaused;
        }
    }
}