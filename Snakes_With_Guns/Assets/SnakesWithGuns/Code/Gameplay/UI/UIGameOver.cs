using SnakesWithGuns.Gameplay.Messages;
using SnakesWithGuns.Infrastructure.PubSub;
using UnityEngine;

namespace SnakesWithGuns.Gameplay.UI
{
    public class UIGameOver : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        private IChannel<GameOverMessage> _gameOverChannel;
        private IChannel<PauseMessage> _pauseChannel;

        private void Awake()
        {
            _gameOverChannel = Channels.GetChannel<GameOverMessage>();
            _pauseChannel = Channels.GetChannel<PauseMessage>();
            _gameOverChannel.Register(OnGameOverMessage);
        }

        private void OnDestroy()
        {
            _gameOverChannel.Unregister(OnGameOverMessage);
        }

        private void OnGameOverMessage(GameOverMessage message)
        {
            _pauseChannel.Publish(new PauseMessage(true));
            _canvas.enabled = true;
        }
    }
}