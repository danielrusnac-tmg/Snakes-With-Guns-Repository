using System;
using System.Linq;
using UnityEngine;

namespace SnakesWithGuns.Gameplay.Settings
{
    [CreateAssetMenu(menuName = "Snakes With Guns/Settings/Global", fileName = "settings_global")]
    public class GlobalSettings : ScriptableObject
    {
        public static GameplaySettings SelectedGameMode;
        
        [SerializeField] private GameplaySettings[] _gameModes = Array.Empty<GameplaySettings>();

        public GameplaySettings[] GameModes => _gameModes;

        private void OnEnable()
        {
            SelectedGameMode = _gameModes.FirstOrDefault();
        }
    }
}