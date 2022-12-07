using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;

namespace SnakesWithGuns.Gameplay.Input
{
    public class Joystick : OnScreenControl, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        [Header("Settings")]
        [SerializeField] private bool _interactable = true;
        [Range(0f, 1f), SerializeField] private float _radius = 0.5f;
        [SerializeField, InputControl(layout = "Vector2")] private string _controlPath;

        [Header("Components")]
        [SerializeField] private RectTransform _handle;
        [SerializeField] private RectTransform _constrain;
        [SerializeField] private CanvasGroup _canvasGroup;

        private Vector2 _startDragPosition;

        public bool Interactable
        {
            get => _interactable;
            set
            {
                if (_interactable == value)
                    return;

                _interactable = value;
                _canvasGroup.interactable = value;
                
                if (!value)
                    Hide();
            }
        }
        
        private bool IsShown
        {
            get => _canvasGroup.alpha > float.Epsilon;
            set => _canvasGroup.alpha = value ? 1f : 0f;
        }
        
        private float ConstrainRadius => (_constrain.rect.width - _handle.rect.width) * _radius;

        protected override string controlPathInternal
        {
            get => _controlPath;
            set => _controlPath = value;
        }
        
        private void Start()
        {
            ResetAnchors();
            Hide();
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            if (!Interactable)
                return;
            
            Show(eventData.position);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!Interactable || !IsShown)
                return;
            
            Vector2 direction = Vector2.ClampMagnitude(
                eventData.position - _startDragPosition,
                ConstrainRadius);

            _handle.localPosition = direction;
            SendValueToControl(direction / ConstrainRadius);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (!Interactable)
                return;
            
            Hide();
            SendValueToControl(Vector2.zero);
        }

        private void ResetAnchors()
        {
            _constrain.anchorMin = new Vector2(0.5f, 0.5f);
            _constrain.anchorMax = new Vector2(0.5f, 0.5f);
        }

        private void Show(Vector2 position)
        {
            _constrain.position = position;
            _startDragPosition = position;
            _handle.localPosition = Vector3.zero;
            IsShown = true;
        }

        private void Hide()
        {
            IsShown = false;
        }
    }
}