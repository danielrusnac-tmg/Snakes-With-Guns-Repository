using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;

namespace SnakesWithGuns.Prototype.Snakes
{
    public class Snake : MonoBehaviour
    {
        [SerializeField] private float _radius = 0.5f;
        [SerializeField] private SnakeMover _mover;
        [SerializeField] private Tail _tail;
        [SerializeField] private Segment[] _segmentPrefabs;

        private ISnakeInputProvider _inputProvider;

        public float Radius => _radius;

        private void Awake()
        {
            _inputProvider = GetComponent<ISnakeInputProvider>();
            Assert.IsNotNull(_inputProvider);
        }

        private IEnumerator Start()
        {
            for (int i = 0; i < _segmentPrefabs.Length; i++)
            {
                _tail.AddSegment(Instantiate(_segmentPrefabs[i]));
                yield return new WaitForSeconds(0.1f);
            }
        }

        private void Update()
        {
            _mover.Direction = _inputProvider.Direction;
        }

        private void OnDrawGizmosSelected()
        {
#if UNITY_EDITOR
            Handles.DrawWireDisc(transform.position, Vector3.up, Radius);
#endif
        }
    }
}