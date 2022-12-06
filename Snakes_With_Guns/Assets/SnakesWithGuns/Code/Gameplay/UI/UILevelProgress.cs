using SnakesWithGuns.Gameplay.Messages;
using SnakesWithGuns.Infrastructure.PubSub;
using UnityEngine;
using UnityEngine.UI;

namespace SnakesWithGuns.Gameplay.UI
{
    public class UILevelProgress : MonoBehaviour
    {
        [SerializeField] private Slider _progressSlider;
        
        private IChannel<LevelProgressMessage> _levelProgressChannel;

        private void Awake()
        {
            _levelProgressChannel = Channels.GetChannel<LevelProgressMessage>();
            _levelProgressChannel.Register(OnLevelProgressMessage);
        }

        private void OnDestroy()
        {
            _levelProgressChannel.Unregister(OnLevelProgressMessage);
        }

        private void OnLevelProgressMessage(LevelProgressMessage message)
        {
            _progressSlider.value = message.Progress;
        }
    }
}