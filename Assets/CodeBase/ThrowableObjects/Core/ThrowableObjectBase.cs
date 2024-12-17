using CodeBase.StaticData;
using CodeBase.ThrowableObjects.Components.Disappearing;
using CodeBase.ThrowableObjects.Components.Movement;
using CodeBase.ThrowableObjects.Pool;
using CodeBase.ThrowableObjects.StateMachine;
using CodeBase.ThrowableObjects.StateMachine.States;
using UnityEngine;

namespace CodeBase.ThrowableObjects.Core
{
    public abstract class ThrowableObjectBase : MonoBehaviour, IThrowableCore
    {
        [SerializeField] private protected ThrowableObjectStaticData _staticData;

        private protected ObjectStateMachine _stateMachine;
        private protected IDisappearable _disappearComponent;
        private protected IObjectMover _moverComponent;

        public ObjectStateMachine StateMachine => _stateMachine;
        public ThrowableObjectStaticData StaticData => _staticData;
        public Vector3 TargetPoint { get; private protected set; }

        protected virtual void Awake()
        {
            InitializeComponents();
            InitializeStateMachine();

        }

        private void OnEnable()
        {
            if (_stateMachine?.CurrentState is ObjectFSMDisabledState)
                _stateMachine.SetState<ObjectFSMIdleState>();
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

        public void ReturnToPool()
        {
            ThrowableObjectPool.ReturnObjectToPool(gameObject);
        }
    }
}