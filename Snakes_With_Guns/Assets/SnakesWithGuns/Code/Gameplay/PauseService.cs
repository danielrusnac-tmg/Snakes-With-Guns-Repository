using UnityEngine;

namespace SnakesWithGuns.Gameplay
{
    public class PauseService
    {
        private bool _isPaused;

        public bool IsPaused
        {
            get => _isPaused;
            set
            {
                if (_isPaused == value)
                    return;

                _isPaused = value;
                Time.timeScale = value ? 0f : 1f;
            }
        }
    }
}