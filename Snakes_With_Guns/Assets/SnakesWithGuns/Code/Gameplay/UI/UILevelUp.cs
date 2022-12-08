using SnakesWithGuns.Gameplay.Messages;
using SnakesWithGuns.Gameplay.Snakes;
using SnakesWithGuns.Infrastructure.PubSub;
using UnityEngine;
using UnityEngine.Scripting;

namespace SnakesWithGuns.Gameplay.UI
{
    public class UILevelUp : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private UIModule[] _modules;
        [RequiredMember]
        [SerializeField] private StaticData _staticData;

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

            Reroll();

            _pauseMessage.Publish(new PauseMessage(true));
        }

        public void Hide()
        {
            _canvas.enabled = false;
            _pauseMessage.Publish(new PauseMessage(false));
        }

        public void Reroll()
        {
            foreach (UIModule module in _modules)
                module.Display(_staticData.GetRandomModule());
        }

        public void Skip()
        {
            Hide();
        }

        private void OnLevelUpMessage(LevelUpMessage message)
        {
            Display(message.Tail);
        }

        private void OnModuleGet(SegmentModule module)
        {
            _tail.AddModule(module);
            Hide();
        }
    }
}