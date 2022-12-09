using UnityEngine;

namespace SnakesWithGuns.Gameplay.Settings
{
    [CreateAssetMenu(menuName = "Snakes With Guns/Settings/Gameplay", fileName = "settings_gameplay_")]
    public class GameplaySettings : ScriptableObject
    {
        [SerializeField] private string _modeName;
        [SerializeField] private DummySpawnSettings _dummySpawn;
        [SerializeField] private int _opponentCount;

        public DummySpawnSettings DummySpawn => _dummySpawn;
        public string ModeName => _modeName;
        public int OpponentCount => _opponentCount;
    }
}