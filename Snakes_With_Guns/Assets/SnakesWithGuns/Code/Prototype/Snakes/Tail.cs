using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SnakesWithGuns.Prototype.Snakes
{
    public class Tail : MonoBehaviour
    {
        [Min(0)]
        [SerializeField] private int _pointPerSegment = 3;
        [Min(0f)]
        [SerializeField] private float _segmentsDistance = 1.1f;
        [SerializeField] private float _moveSpeed = 15f;
        [SerializeField] private float _turnSpeed = 20f;
        [SerializeField] private Segment _segmentPrefab;
        [SerializeField] private List<Segment> _segments = new();

        private Transform _transform;
        private Transform _root;
        private List<TailPoint> _points = new();

        private float PointsDistance => _segmentsDistance / _pointPerSegment;

        private void Awake()
        {
            _transform = transform;
            _root = transform.parent;
        }

        private void Update()
        {
            if (_points.Count == 0)
                return;

            MoveSegments();
        }

        [ContextMenu(nameof(AddSegment))]
        public void AddSegment()
        {
            Transform previousSegmentTransform = _segments.Count == 0 ? _transform : _segments.Last().Transform;
            Vector3 position = previousSegmentTransform.position;
            Quaternion rotation = Quaternion.LookRotation(previousSegmentTransform.forward, Vector3.up);

            Segment instance = Instantiate(_segmentPrefab, position, rotation, _root);
            _segments.Add(instance);

            AddSegmentPoints(new TailPoint(instance.Transform));
        }

        private void MoveSegments()
        {
            Vector3 direction = _transform.position - _points[0].Position;
            float distance = direction.magnitude;

            if (distance > PointsDistance)
            {
                _points.Insert(0, new TailPoint
                {
                    Position = _points[0].Position + direction.normalized * PointsDistance,
                    Rotation = _transform.rotation
                });

                _points.RemoveAt(_points.Count - 1);

                distance -= PointsDistance;
            }

            MoveSegment(_segments[0], _transform.position, _transform.rotation);

            if (_segments.Count < 2)
                return;

            float value = distance / PointsDistance;

            for (int i = 1; i < _segments.Count; i++)
            {
                int pointA = i * _pointPerSegment;
                int pointB = pointA - 1;
                
                MoveSegment(_segments[i],
                    Vector3.Lerp(_points[pointA].Position, _points[pointB].Position, value),
                    Quaternion.Lerp(_points[pointA].Rotation, _points[pointB].Rotation, value));
            }
        }

        private void MoveSegment(Segment segment, Vector3 position, Quaternion rotation)
        {
            segment.Transform.position =
                Vector3.Lerp(segment.Transform.position, position, _moveSpeed * Time.deltaTime);
            segment.Transform.rotation =
                Quaternion.Lerp(segment.Transform.rotation, rotation, _turnSpeed * Time.deltaTime);
        }

        [ContextMenu(nameof(RemoveSegment))]
        public void RemoveSegment()
        {
            if (_segments.Count <= 1)
                return;

            _segments.RemoveAt(_segments.Count - 1);
        }

        private void AddSegmentPoints(TailPoint point)
        {
            for (int i = 0; i < _pointPerSegment; i++)
                _points.Add(point);
        }

        private struct TailPoint
        {
            public Vector3 Position;
            public Quaternion Rotation;

            public TailPoint(Transform transform)
            {
                Position = transform.position;
                Rotation = transform.rotation;
            }
        }
    }
}