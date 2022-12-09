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
        [SerializeField] private float _levelDuration = 900f;
        [SerializeField] private AnimationCurve _dummyChanceCurve = AnimationCurve.Constant(0f, 1f, 1f);

        public float CalculateSpawnRate(float gameplayTime)
        {
            return _dummyChanceCurve.Evaluate(NormalizedGameplayTime(gameplayTime));
        }

        public Dummy GetPrefab(float gameplayTime)
        {
            return _prefabs[Random.Range(0, _prefabs.Length)];
        }

        private float NormalizedGameplayTime(float gameplayTime)
        {
            return Mathf.Clamp01(gameplayTime / _levelDuration);
        }
    }
}