using System.Collections;
using SnakesWithGuns.Gameplay.Snakes;
using UnityEngine;

namespace SnakesWithGuns.Gameplay
{
    public class DefaultModuleInstaller : MonoBehaviour
    {
        [SerializeField] private Tail _tail;
        [SerializeField] private SegmentModule[] _modules;

        private IEnumerator Start()
        {
            foreach (SegmentModule module in _modules)
            {
                _tail.AddModule(module);
                yield return null;
            }
        }
    }
}