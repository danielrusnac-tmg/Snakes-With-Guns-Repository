using System;
using SnakesWithGuns.Gameplay.Snakes;
using TMPro;
using UnityEngine;

namespace SnakesWithGuns.Gameplay.UI
{
    public class UIModule : MonoBehaviour
    {
        public event Action<SegmentModule> GetPressed;

        [SerializeField] private TMP_Text _segmentName;

        private SegmentModule _module;

        public void Display(SegmentModule module)
        {
            _module = module;
            _segmentName.SetText(module.name);
        }

        public void Get()
        {
            GetPressed?.Invoke(_module);
        }
    }
}