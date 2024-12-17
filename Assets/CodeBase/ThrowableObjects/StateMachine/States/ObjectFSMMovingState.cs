using CodeBase.ThrowableObjects.Components.Movement;
using CodeBase.ThrowableObjects.StateMachine.Base;

namespace CodeBase.ThrowableObjects.StateMachine.States
{
    public class ObjectFSMMovingState : BaseObjectFSMState
    {
        private readonly IObjectMover _objectMover;

        public ObjectFSMMovingState(IThrowableCore context, IObjectMover objectMover) : base(context)
        {
            _objectMover = objectMover;
        }

        public override void Enter()
        {
            _objectMover.StartMoving(_context.TargetPoint);
            _objectMover.Moved += OnMoved;
        }

        public override void Update()
        {
            _objectMover.Move();
        }

        public override void Exit()
        {
            _objectMover.Moved -= OnMoved;
        }

        public void OnMoved()
        {
            _context.StateMachine.SetState<ObjectFSMIdleState>();
        }
    }
}
