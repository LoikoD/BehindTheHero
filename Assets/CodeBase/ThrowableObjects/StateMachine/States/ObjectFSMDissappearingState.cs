using CodeBase.ThrowableObjects.Components.Disappearing;
using CodeBase.ThrowableObjects.StateMachine.Base;

namespace CodeBase.ThrowableObjects.StateMachine.States
{
    public class ObjectFSMDisappearingState : BaseObjectFSMState
    {
        private readonly IDisappearable _disappearable;

        public ObjectFSMDisappearingState(IThrowableCore context, IDisappearable disappearable) : base(context)
        {
            _disappearable = disappearable;
        }

        public override void Enter()
        {
            _disappearable.StartDisappear();
            _disappearable.Disappeared += OnDisappeared;
        }

        public override void Exit()
        {
            _disappearable.StopDisappear();
            _disappearable.Disappeared -= OnDisappeared;
        }

        private void OnDisappeared()
        {
            _context.StateMachine.SetState<ObjectFSMDisabledState>();
        }
    }
}
