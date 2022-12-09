using System;
using SnakesWithGuns.Gameplay.Settings;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SnakesWithGuns.Gameplay.UI
{
    public class UIGameModeButton : UIBehaviour
    {
        public event Action<GameplaySettings> Selected; 

        [SerializeField] private TMP_Text _gameModeName;

        private GameplaySettings _gameplaySettings;

        public void Display(GameplaySettings gameplaySettings)
        {
            _gameplaySettings = gameplaySettings;
            _gameModeName.SetText(gameplaySettings.ModeName);
        }

        public void Select()
        {
            Selected?.Invoke(_gameplaySettings);
        }
    }
}