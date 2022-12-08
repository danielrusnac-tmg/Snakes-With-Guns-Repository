using System;
using TMPro;
using UnityEngine;

namespace SnakesWithGuns.Gameplay.UI
{
    public class UITimer : MonoBehaviour
    {
        [SerializeField] private Session _session;
        [SerializeField] private TMP_Text _timeText;

        private void Update()
        {
            TimeSpan timeSpan = TimeSpan.FromSeconds(_session.GameplayTime);
            _timeText.SetText($"{timeSpan.Minutes:00}:{timeSpan.Seconds:00}");
        }
    }
}