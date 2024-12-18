using CodeBase.StaticData;
using CodeBase.ThrowableObjects.Components.Disappearing;
using CodeBase.ThrowableObjects.Components.Movement;
using CodeBase.ThrowableObjects.StateMachine;
using CodeBase.ThrowableObjects.StateMachine.States;
using System;
using UnityEngine;

namespace CodeBase.ThrowableObjects.Core
{
    public abstract class ThrowableObjectBase : MonoBehaviour, IThrowableCore
    {
        [SerializeField] private protected ThrowableObjectStaticData _staticData;

        private protected ObjectStateMachine _stateMachine;
        private protected IDisappearable _disappearComponent;
        private protected IObjectMover _moverComponent;
        private protected Transform _objectsHolder;

        public ObjectStateMachine StateMachine => _stateMachine;
        public ThrowableObjectStaticData StaticData => _staticData;
        public Vector3 TargetPoint { get; private protected set; }

        public event Action<ThrowableObjectBase> Disabled;

        public void Initialize(Transform objectsHolder)
        {
            InitializeComponents();
            InitializeStateMachine();

            _objectsHolder = objectsHolder;
            transform.SetParent(_objectsHolder, true);
        }

        private void OnEnable()
        {
            if (_stateMachine?.CurrentState is ObjectFSMDisabledState)
            {
                _stateMachine.SetState<ObjectFSMIdleState>();
            }
        }

        protected virtual void Update()
        {
            _stateMachine?.Update();
        }

        private void InitializeComponents()
        {
            _disappearComponent = GetComponent<IDisappearable>();
            _moverComponent = GetComponent<IObjectMover>();
        }

        protected virtual void InitializeStateMachine()
        {
            _stateMachine = new ObjectStateMachine(this, _moverComponent, _disappearComponent);
            _stateMachine.SetState<ObjectFSMIdleState>();
        }

        public void Disable()
        {
            Disabled.Invoke(this);
        }
    }
}