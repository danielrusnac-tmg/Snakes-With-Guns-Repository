using System;
using SnakesWithGuns.Gameplay.Objects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace SnakesWithGuns.Gameplay.Settings
{
    [Serializable]
    public class DummySpawnSettings
    {
        [SerializeField] private Dummy[] _prefabs;
        [SerializeField] private float _spawnRate = 2f;

        public float CalculateSpawnRate(float gameplayTime)
        {
            return _spawnRate;
        }

        public Dummy GetPrefab(float gameplayTime)
        {
            return _prefabs[Random.Range(0, _prefabs.Length)];
        }
    }
}