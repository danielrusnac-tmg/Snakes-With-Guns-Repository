using SnakesWithGuns.Gameplay.Snakes;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SnakesWithGuns.Gameplay.UI
{
    public class Gameplay : MonoBehaviour
    {
        [SerializeField] private Snake _player;
        [SerializeField] private UITailEditor _uiTailEditorPrefab;

        private bool _isEditorOpened;
        private UITailEditor _uiTailEditorInstance;

        private void Update()
        {
            if (Keyboard.current.tabKey.wasPressedThisFrame)
                ToggleTailEditor();
        }

        private void ShowTailEditor()
        {
            _uiTailEditorInstance = Instantiate(_uiTailEditorPrefab);
            _uiTailEditorInstance.Display(_player.GetComponent<Tail>());
        }

        private void HideTailEditor()
        {
            Destroy(_uiTailEditorInstance.gameObject);
        }

        private void ToggleTailEditor()
        {
            _isEditorOpened = !_isEditorOpened;

            if (_isEditorOpened)
                ShowTailEditor();
            else
                HideTailEditor();
        }
    }
}