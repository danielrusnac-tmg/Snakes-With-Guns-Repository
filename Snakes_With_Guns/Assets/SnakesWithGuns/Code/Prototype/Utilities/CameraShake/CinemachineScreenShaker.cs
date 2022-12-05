using System;
using System.Collections.Generic;
using UnityEngine;

namespace SnakesWithGuns.Prototype.Utilities.CameraShake
{
    public class CinemachineScreenShaker : MonoBehaviour, IScreenShaker
    {
        [SerializeField] private CinemachineScreenShakePreset[] _presets = Array.Empty<CinemachineScreenShakePreset>();

        private Dictionary<CameraShakeType, CinemachineScreenShakePreset> _presetByType = new();

        private void Awake()
        {
            _presetByType = new Dictionary<CameraShakeType, CinemachineScreenShakePreset>();

            foreach (CinemachineScreenShakePreset preset in _presets)
            {
                if (!_presetByType.ContainsKey(preset.ShakeType))
                    _presetByType.Add(preset.ShakeType, preset);
            }
        }

        public void Shake(CameraShakeType type)
        {
            if (_presetByType.ContainsKey(type))
                _presetByType[type].GenerateImpulse();
        }
    }
}