using UnityEngine;

namespace SnakesWithGuns.Utilities
{
    public class TargetFramerateSetter : MonoBehaviour
    {
        [SerializeField] private int _targetFps = 60;

        private void Start()
        {
            Application.targetFrameRate = _targetFps;
        }
    }
}