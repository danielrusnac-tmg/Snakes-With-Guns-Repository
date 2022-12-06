using SnakesWithGuns.Gameplay.Messages;
using SnakesWithGuns.Gameplay.Snakes;
using SnakesWithGuns.Infrastructure.PubSub;
using UnityEngine;

namespace SnakesWithGuns.Gameplay.UI
{
    public class UITailEditor : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        private Tail _tail;
        private IChannel<LevelUpMessage> _levelUpChannel;
        private IChannel<PauseMessage> _pauseMessage;

        private void Awake()
        {
            _levelUpChannel = Channels.GetChannel<LevelUpMessage>();
            _pauseMessage = Channels.GetChannel<PauseMessage>();
            _levelUpChannel.Register(OnLevelUpMessage);
        }

        private void OnDestroy()
        {
            _levelUpChannel.Unregister(OnLevelUpMessage);
        }

        public void Display(Tail tail)
        {
            _tail = tail;
            _canvas.enabled = true;
            _pauseMessage.Publish(new PauseMessage(true));
        }

        public void Hide()
        {
            _canvas.enabled = false;
            _pauseMessage.Publish(new PauseMessage(false));
        }

        public void AddSegment()
        {
            _tail.AddSegment();
        }

        public void RemoveSegment()
        {
            _tail.RemoveSegment();
        }

        private void OnLevelUpMessage(LevelUpMessage message)
        {
            Display(message.Tail);
        }
    }
}