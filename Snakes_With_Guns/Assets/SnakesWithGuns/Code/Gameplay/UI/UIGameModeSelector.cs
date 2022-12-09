using SnakesWithGuns.Gameplay.Settings;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SnakesWithGuns.Gameplay.UI
{
    public class UIGameModeSelector : MonoBehaviour
    {
        [SerializeField] private GlobalSettings _globalSettings;
        [SerializeField] private UIGameModeButton _modeButtonPrefab;
        [SerializeField] private RectTransform _content;

        private void Awake()
        {
            foreach (GameplaySettings gameMode in _globalSettings.GameModes)
            {
                UIGameModeButton button = Instantiate(_modeButtonPrefab, _content);
                button.Display(gameMode);
                button.Selected += OnModeSelected;
            }
        }

        private void OnModeSelected(GameplaySettings modeSettings)
        {
            GlobalSettings.SelectedGameMode = modeSettings;
            SceneManager.LoadScene(1);
        }
    }
}