using System;
using UnityEngine;

namespace CodeBase.CameraLogic
{
    public class CameraMover : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private AnimationCurve _curve;

        private const float MoveSpeed = 0.3f;

        private Vector3 _targetPosition;
        private Vector3 _startPosition;
        private float _moveTime;
        private bool _isMoving = false;

        public event Action Moved;

        public void FixedUpdate()
        {
            if (_isMoving)
            {
                if (_moveTime < 1)
                {
                    _moveTime = Mathf.MoveTowards(_moveTime, 1, MoveSpeed * Time.deltaTime);
                    transform.position = Vector3.Lerp(_startPosition, _targetPosition, _curve.Evaluate(_moveTime));
                }
                else
                {
                    _isMoving = false;
                    Moved?.Invoke();
                }
            }
        }

        public void StartMoving()
        {
            _targetPosition = new Vector3(_target.position.x, _target.position.y, transform.position.z);
            _startPosition = transform.position;
            _isMoving = true;
            _moveTime = 0;
        }
    }
}
