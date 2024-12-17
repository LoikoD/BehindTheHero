using CodeBase.StaticData;
using System;
using UnityEngine;

namespace CodeBase.ThrowableObjects.Components.Movement
{
    public class ObjectMover : MonoBehaviour, IObjectMover
    {
        [SerializeField] private ObjectMoverStaticData _data;

        private float _moveSpeed;
        private AnimationCurve _moveCurve;

        private Vector3 _targetPosition;
        private Vector3 _startPosition;
        private float _moveTime;

        public event Action Moved;

        private void Awake()
        {
            _moveSpeed = _data.Speed;
            _moveCurve = _data.Curve;
        }

        public void Move()
        {
            if (_moveTime < 1)
            {
                _moveTime = Mathf.MoveTowards(_moveTime, 1, _moveSpeed * Time.deltaTime);
                transform.position = Vector3.Lerp(_startPosition, _targetPosition, _moveCurve.Evaluate(_moveTime));
            }
            else
            {
                Moved.Invoke();
            }
        }

        public void StartMoving(Vector3 targetPoint)
        {
            _targetPosition = targetPoint;
            _startPosition = transform.position;
            _moveTime = 0;
        }
    }
}
