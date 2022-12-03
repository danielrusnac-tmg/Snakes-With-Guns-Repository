﻿using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SnakesWithGuns.Prototype.Snakes
{
    public class Tail : MonoBehaviour
    {
        [Min(0), SerializeField] private int _pointPerSegment = 3;
        [Min(0f), SerializeField] private float _segmentsDistance = 1.1f;
        [SerializeField] private float _moveSpeed = 15f;
        [SerializeField] private float _turnSpeed = 20f;

        private Transform _transform;
        private Transform _root;
        private List<TailPoint> _points = new();
        private List<Segment> _segments = new();

        private float PointsDistance => _segmentsDistance / _pointPerSegment;

        private void Awake()
        {
            _transform = transform;
            _root = transform.parent;
        }

        private void FixedUpdate()
        {
            if (_points.Count == 0)
                return;

            MoveSegments();
        }

        public void AddSegment(Segment instance)
        {
            Vector3 position = _segments.Count == 0 ? _transform.position : _segments.Last().Position;
            Vector3 forward = _segments.Count == 0 ? _transform.forward : _segments.Last().Rotation * Vector3.forward;
            Quaternion rotation = Quaternion.LookRotation(forward, Vector3.up);

            instance.Position = position;
            instance.Rotation = rotation;
            
            _segments.Add(instance);

            AddSegmentPoints(new TailPoint(instance.Position, instance.Rotation));
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
            segment.Position =
                Vector3.Lerp(segment.Position, position, _moveSpeed * Time.fixedDeltaTime);
            segment.Rotation =
                Quaternion.Lerp(segment.Rotation, rotation, _turnSpeed * Time.fixedDeltaTime);
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

            public TailPoint(Vector3 position, Quaternion rotation)
            {
                Position = position;
                Rotation = rotation;
            }
        }
    }
}