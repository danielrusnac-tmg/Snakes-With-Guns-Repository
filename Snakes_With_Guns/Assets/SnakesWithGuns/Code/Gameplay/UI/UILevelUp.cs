using System.Linq;
using SnakesWithGuns.Gameplay.Messages;
using SnakesWithGuns.Gameplay.Snakes;
using SnakesWithGuns.Infrastructure.PubSub;
using UnityEngine;

namespace SnakesWithGuns.Gameplay.UI
{
    public class UILevelUp : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private UIModule[] _modules;
        [SerializeField] private SegmentModule[] _segmentModules;

        private Tail _tail;
        private IChannel<LevelUpMessage> _levelUpChannel;
        private IChannel<PauseMessage> _pauseMessage;

        private void Awake()
        {
            _levelUpChannel = Channels.GetChannel<LevelUpMessage>();
            _pauseMessage = Channels.GetChannel<PauseMessage>();
            _levelUpChannel.Register(OnLevelUpMessage);
            
            foreach (UIModule module in _modules)
                module.GetPressed += OnModuleGet;
        }

        private void OnDestroy()
        {
            _levelUpChannel.Unregister(OnLevelUpMessage);
            
            foreach (UIModule module in _modules)
                module.GetPressed -= OnModuleGet;
        }

        public void Display(Tail tail)
        {
            _tail = tail;
            _canvas.enabled = true;
            
            foreach (UIModule module in _modules)
                module.Display(_segmentModules[Random.Range(0, _segmentModules.Length)]);
            
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

        private void OnModuleGet(SegmentModule module)
        {
            _tail.AddSegment();
            _tail.Segments.Last().InstallModule(module);
            Hide();
        }
    }
}