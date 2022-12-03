using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

namespace SnakesWithGuns.Prototype.Input
{
    public class Joystick : OnScreenControl
    {
        [Header("Settings")]
        [Range(0f, 1f), SerializeField] private float _radius = 1f;
        [Range(0f, 1f), SerializeField] private float _activeAlpha = 1f;
        [Range(0f, 1f), SerializeField] private float _inactiveAlpha = 0.3f;
        [SerializeField, InputControl(layout = "Vector2")] private string _controlPath;

        [Header("Components")]
        [SerializeField] private RectTransform _defaultJoystickPosition;
        [SerializeField] private RectTransform _handle;
        [SerializeField] private RectTransform _constrain;
        [SerializeField] private CanvasGroup _canvasGroup;

        private Vector2 _startDragPosition;
        private bool _isActive;

        private float ConstrainRadius => _constrain.rect.width * 0.5f * _radius;

        protected override string controlPathInternal
        {
            get => _controlPath;
            set => _controlPath = value;
        }

        private void Awake()
        {
            _canvasGroup.alpha = _inactiveAlpha;
        }

        private void Start()
        {
            ResetAnchors();
            MoveToDefaultPosition();
        }

        private void Update()
        {
            if (EventSystem.current.currentInputModule.input.GetMouseButtonDown(0))
            {
                MoveToActivePosition(EventSystem.current.currentInputModule.input.mousePosition);
            }

            if (EventSystem.current.currentInputModule.input.GetMouseButton(0))
            {
                if (_startDragPosition != EventSystem.current.currentInputModule.input.mousePosition && !_isActive)
                    _isActive = true;

                if (_isActive)
                    OnDrag(EventSystem.current.currentInputModule.input.mousePosition);
            }

            if (EventSystem.current.currentInputModule.input.GetMouseButtonUp(0))
            {
                _isActive = false;
                OnPointerUp();
            }
        }

        private void OnDrag(Vector2 position)
        {
            _constrain.gameObject.SetActive(true);

            Vector2 direction = Vector2.ClampMagnitude(
                position - _startDragPosition,
                ConstrainRadius);

            _handle.localPosition = direction;
            SendValueToControl(direction / ConstrainRadius);
        }

        private void OnPointerUp()
        {
            _constrain.gameObject.SetActive(false);

            MoveToDefaultPosition();
            SendValueToControl(Vector2.zero);
        }

        private void ResetAnchors()
        {
            _constrain.anchorMin = new Vector2(0.5f, 0.5f);
            _constrain.anchorMax = new Vector2(0.5f, 0.5f);
        }

        private void MoveToActivePosition(Vector2 position)
        {
            _constrain.position = position;
            _startDragPosition = position;
            _canvasGroup.alpha = _activeAlpha;
        }

        private void MoveToDefaultPosition()
        {
            _canvasGroup.alpha = _inactiveAlpha;
            _constrain.position = _defaultJoystickPosition.position;
            _handle.localPosition = Vector2.zero;
        }
    }
}