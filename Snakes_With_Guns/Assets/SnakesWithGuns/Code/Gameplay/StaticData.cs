using SnakesWithGuns.Gameplay.Snakes;
using UnityEngine;

namespace SnakesWithGuns.Gameplay
{
    public class StaticData : MonoBehaviour
    {
        [SerializeField] private SegmentModule[] _modules;

        public SegmentModule[] Modules => _modules;

        public SegmentModule GetRandomModule()
        {
            return _modules[Random.Range(0, _modules.Length)];
        }
    }
}