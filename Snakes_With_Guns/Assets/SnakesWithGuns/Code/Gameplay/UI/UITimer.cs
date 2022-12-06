using System;
using TMPro;
using UnityEngine;

namespace SnakesWithGuns.Gameplay.UI
{
    public class UITimer : MonoBehaviour
    {
        [SerializeField] private TMP_Text _timeText;

        private float _time;

        private void Update()
        {
            _time += Time.deltaTime;
            TimeSpan timeSpan = TimeSpan.FromSeconds(_time);
            _timeText.SetText($"{timeSpan.Minutes:00}:{timeSpan.Seconds:00}");
        }
    }
}