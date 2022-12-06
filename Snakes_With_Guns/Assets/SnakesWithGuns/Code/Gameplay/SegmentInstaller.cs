using System;
using SnakesWithGuns.Gameplay.Snakes;
using UnityEngine;

namespace SnakesWithGuns.Gameplay
{
    public class SegmentInstaller : MonoBehaviour
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

        [ContextMenu(nameof(Add))]
        public void Add()
        {
            _tail.AddSegment();
        }

        [ContextMenu(nameof(Remove))]
        public void Remove()
        {
            _tail.RemoveSegment();
        }

        public void Install(SegmentModule weapon, int segmentIndex)
        {
            if (!IsValidIndex(segmentIndex) || weapon == null)
                return;

            Install(weapon, _tail.Segments[segmentIndex]);
        }

        [ContextMenu(nameof(Install))]
        public void Install(SegmentModule weapon, Segment segment)
        {
            segment.InstallModule(weapon);
        }

        [ContextMenu(nameof(Uninstall))]
        public void Uninstall(int segmentIndex) { }

        private bool IsValidIndex(int targetSegment)
        {
            return targetSegment >= 0 && targetSegment < _tail.Segments.Count;
        }
    }
}