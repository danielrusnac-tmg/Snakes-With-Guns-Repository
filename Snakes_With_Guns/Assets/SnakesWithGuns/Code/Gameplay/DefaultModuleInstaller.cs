using System;
using SnakesWithGuns.Gameplay.Snakes;
using UnityEngine;

namespace SnakesWithGuns.Gameplay
{
    public class DefaultModuleInstaller : MonoBehaviour
    {
        [SerializeField] private Tail _tail;
        [SerializeField] private SegmentModule[] _defaultWeapons = Array.Empty<SegmentModule>();

        private void Start()
        {
            if (_defaultWeapons.Length == 0)
                return;

            for (int i = 0; i < _defaultWeapons.Length; i++)
            {
                if (_tail.Segments.Count < i + 1)
                    _tail.AddSegment();

                Install(_defaultWeapons[i], i);
            }
        }

        private void Install(SegmentModule weapon, int segmentIndex)
        {
            if (!IsValidIndex(segmentIndex) || weapon == null)
                return;

            _tail.Segments[segmentIndex].InstallModule(weapon);
        }

        private bool IsValidIndex(int targetSegment)
        {
            return targetSegment >= 0 && targetSegment < _tail.Segments.Count;
        }
    }
}