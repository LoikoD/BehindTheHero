using System.Collections;
using CodeBase.StaticData;
using CodeBase.ThrowableObjects.Objects.EquipableObject.Weapon;
using CodeBase.ThrowableObjects.Pool;
using UnityEngine;

namespace CodeBase.ThrowableObjects
{
    public class ThrowableObject : MonoBehaviour, IEquippableObject
    {
        [SerializeField] private ThrowableObjectStaticData _staticData;

        [SerializeField] private ThrowableObjectState _state;
        [SerializeField] private bool _isOnKnight;

        private Vector3 _targetDirection;
        private Vector3 _targetPoint;

        private DisappearableObject _disappear;

        private float _idleTime;

        public ThrowableObjectState State => _state;

        public bool CanBePickedUp => _state == ThrowableObjectState.Idle || _state == ThrowableObjectState.Disappearing;

        private void Awake()
        {
            _disappear = GetComponent<DisappearableObject>();

        }

        private void OnEnable()
        {
            if (_isOnKnight)
            {
                _state = ThrowableObjectState.Weapon;
                return;
            }
            
            _state = ThrowableObjectState.Idle;
            _disappear = GetComponent<DisappearableObject>();
        }

        private void Update()
        {
            if (_state == ThrowableObjectState.Idle)
            {
                _idleTime += Time.deltaTime;
                if (_idleTime > _staticData.TimeToDisappear)
                {
                    _state = ThrowableObjectState.Disappearing;
                    _disappear.StartDisappear();
                }
            }
        }


        private void FixedUpdate()
        {
            if (_state == ThrowableObjectState.Moving)
            {
                if (Vector3.Distance(transform.position, _targetPoint) > _staticData.DistanceEpsilon)
                {
                    Move(_targetPoint);
                }
                else
                {
                    _state = ThrowableObjectState.Idle;
                    _idleTime = 0;
                }
            }
        }

        public void InitThrow(Vector2 targetPoint)
        {
            _targetDirection = (Vector3)targetPoint - transform.position;
            _targetDirection.Normalize();

            _targetPoint = transform.position + _targetDirection * _staticData.MaxDistance;

            _state = ThrowableObjectState.Moving;
        }

        public void PickedUp()
        {
            if (_state == ThrowableObjectState.Disappearing)
            {
                _disappear.StopDisappear();
            }

            _state = ThrowableObjectState.PickedUp;
        }

        public void Equip(Vector3 position)
        {
            _state = ThrowableObjectState.Disabled;
            ThrowableObjectPool.ReturnObjectToPool(gameObject);
        }

        private void Move(Vector3 position)
        {
            Vector3 newPos = Vector3.Lerp(transform.position, position, _staticData.Speed * Time.deltaTime);
            transform.position = newPos;
        }
    }
}
