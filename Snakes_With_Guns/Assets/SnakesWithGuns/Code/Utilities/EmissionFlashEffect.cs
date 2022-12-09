using System.Collections;
using UnityEngine;

namespace SnakesWithGuns.Utilities
{
    public class EmissionFlashEffect : MonoBehaviour
    {
        private static readonly int EMISSION_COLOR = Shader.PropertyToID("_EmissionColor");

        [SerializeField] private Renderer _renderer;
        [SerializeField] private Color _color = Color.white;
        [SerializeField] private float _duration = 0.5f;
        [SerializeField] private AnimationCurve _ease = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);

        private GameObject _gameObject;
        private MaterialPropertyBlock _propertyBlock;
        private Coroutine _flashCoroutine;

        private void Awake()
        {
            _gameObject = gameObject;
            _propertyBlock = new MaterialPropertyBlock();
        }

        [ContextMenu(nameof(Flash))]
        public void Flash()
        {
            if (!_gameObject.activeInHierarchy)
                return;

            if (_flashCoroutine != null)
                StopCoroutine(_flashCoroutine);

            _flashCoroutine = StartCoroutine(FlashRoutine());
        }

        [ContextMenu(nameof(ResetFlash))]
        public void ResetFlash()
        {
            _propertyBlock.SetColor(EMISSION_COLOR, Color.black);
            _renderer.SetPropertyBlock(_propertyBlock);
        }

        private IEnumerator FlashRoutine()
        {
            float t = 0f;

            while (t < 1f)
            {
                t += Time.deltaTime / _duration;
                SetColor(t);
                yield return null;
            }

            SetColor(1f);
        }

        private void SetColor(float t)
        {
            _propertyBlock.SetColor(EMISSION_COLOR, Color.Lerp(_color, Color.black, _ease.Evaluate(t)));
            _renderer.SetPropertyBlock(_propertyBlock);
        }
    }
}