using CodeBase.ThrowableObjects.StateMachine.Base;
using UnityEngine;

namespace CodeBase.ThrowableObjects.StateMachine.States
{
    public class ObjectFSMIdleState : BaseObjectFSMState
    {
        private float _idleTime;

        public ObjectFSMIdleState(IThrowableCore context) : base(context) { }

        public override void Enter()
        {
            _idleTime = 0f;
        }

        public override void Update()
        {
            _idleTime += Time.deltaTime;
            if (_idleTime > _context.StaticData.TimeToDisappear)
            {
                _context.StateMachine.SetState<ObjectFSMDisappearingState>();
            }
        }
    }
}
